using UnityEngine;

public class AudioRayCasting : MonoBehaviour
{
    [Header("Raycast Settings")]
    public int rayCastAmount = 50;
    public LayerMask mask;

    [Header("Particles")]
    public ParticleSystem hitParticles;

    public void CastRadialRays()
    {
        CastRadialRays(transform.position);
    }

    public void CastRadialRays(Vector3 origin)
    {
        for (int i = 0; i < rayCastAmount; i++)
        {
            Vector3 dir = FibonacciSphere(i, rayCastAmount);

            RaycastHit[] hits = Physics.RaycastAll(origin, dir, 100f, mask);

            System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            float remainingIntens = 1f;

            foreach (RaycastHit hit in hits)
            {
                remainingIntens = ProcessHit(hit, remainingIntens);

                if (remainingIntens < 0.01f) break;
                else
                {
                    Vector3 reflectDir = Vector3.Reflect(dir, hit.normal);
                    Vector3 reflectOrigin = hit.point + hit.normal * 0.01f;
                    ProcessReflection(reflectOrigin, reflectDir, remainingIntens);
                }
            }
        }
    }

    void ProcessReflection(Vector3 origin, Vector3 direction, float intensity)
    {
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, 100f, mask);

        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            intensity = ProcessHit(hit, intensity);
            if (intensity < 0.01f) break;
        }
    }

    float ProcessHit(RaycastHit hit, float intensity)
    {
        MaterialDampening material = hit.collider.GetComponentInParent<MaterialDampening>();

        float dampening;
        if (material != null)
            dampening = material.dampeningCoefficient;
        else
            dampening = 0.7f;

        float distanceDampening = 1f / (1f + hit.distance * 0.1f);

        intensity *= (1 - dampening) * distanceDampening;

        if (intensity < 0.01f) return intensity;

        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

        emitParams.position = hit.point;
        emitParams.startLifetime = 1.5f;
        emitParams.startSize = Mathf.Lerp(0.05f, 0.3f, intensity);

        if (hit.collider.CompareTag("PuzzlePiece"))
            emitParams.startColor = Color.blue;
        else
            emitParams.startColor = Color.white;

        hitParticles.Emit(emitParams, 1);
        return intensity;
    }

    Vector3 FibonacciSphere(int index, int total)
    {
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float theta = 2 * Mathf.PI * index / goldenRatio;
        float phi = Mathf.Acos(1 - 2f * (index + 0.5f) / total);
        return new Vector3(
            Mathf.Sin(phi) * Mathf.Cos(theta),
            Mathf.Cos(phi),
            Mathf.Sin(phi) * Mathf.Sin(theta)
        );
    }
}
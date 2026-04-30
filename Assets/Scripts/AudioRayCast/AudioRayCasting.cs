using System.Collections.Generic;
using UnityEngine;

public class AudioRayCasting : MonoBehaviour
{
    [Header("Raycast Settings")]
    public int rayCastAmount = 50;
    public LayerMask mask;
    private GameObject hitObject;
    private Vector3 hitPoint;
    private float hitDistance;
    private float dampening;
  
    [Header("Particles")]
    public ParticleSystem hitParticles;

    public void CastRadialRays()
    {
        for (int i = 0; i < rayCastAmount; i++)
        {

            Vector3 dir = FibonacciSphere(i, rayCastAmount);
            RaycastHit[] hits = Physics.RaycastAll(transform.position, dir, 100f, mask);
            float remainingIntens = 1f;

            foreach (RaycastHit hit in hits)
            {
                hitObject = hit.collider.gameObject;
                hitPoint = hit.point;
                hitDistance = hit.distance;

                MaterialDampening material = hitObject.GetComponentInParent<MaterialDampening>();
                if (material != null)
                    dampening = material.dampeningCoefficient;
                else
                    dampening = 0.7f;

                remainingIntens *= (1 - dampening);

                if (remainingIntens < 0.01) break;

                ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

                emitParams.position = hitPoint;
                emitParams.startLifetime = 5f;
                emitParams.startSize = Mathf.Lerp(0.3f, 0.05f, remainingIntens);
                emitParams.startColor = Color.white;

                hitParticles.Emit(emitParams, 1);
            }     
        }
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

using Unity.VisualScripting;
using UnityEngine;

public class AudioRayCasting : MonoBehaviour
{
    [Header("Raycast Settings")]
    public int rayCastAmount = 50;
    public LayerMask mask;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CastRadialRays();
    }

    void CastRadialRays()
    {
        Debug.Log("Space was pressed");
        float angleStep = 360f / rayCastAmount;

        for (int i = 0; i < rayCastAmount; i++)
        {
            Vector3 dir = FibonacciSphere(i, rayCastAmount);
            Ray ray = new Ray(transform.position, dir);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
            {
                Debug.DrawRay(transform.position, dir * hit.distance, Color.green, 2f);
            }
        }
    }

    //Vector3 AngleToDirection(float angle)
    //{
    //    float radians = angle * Mathf.Deg2Rad;

    //    return new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
    //}

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

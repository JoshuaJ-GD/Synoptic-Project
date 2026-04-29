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

    [Header("Audio")]
    public List<AudioClip> audioClips;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CastRadialRays();
    }

    void CastRadialRays()
    {
        Debug.Log("Space was pressed");

        for (int i = 0; i < rayCastAmount; i++)
        {
            Vector3 dir = FibonacciSphere(i, rayCastAmount);
            RaycastHit[] hits = Physics.RaycastAll(transform.position, dir, 100f, mask);

            foreach (RaycastHit hit in hits)
            {
                hitObject = hit.collider.gameObject;
                hitPoint = hit.point;
                hitDistance = hit.distance;

                Debug.DrawLine(transform.position, hitPoint, Color.green, 5f);
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

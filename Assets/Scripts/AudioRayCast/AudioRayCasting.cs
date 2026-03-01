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
            float angle = i * angleStep;
            Vector3 dir = AngleToDirection(angle);

            Ray ray = new Ray(transform.position, dir);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
            {
                Debug.DrawRay(transform.position, dir * hit.distance, Color.green, 2f);
            }
        }
    }

    Vector3 AngleToDirection(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
    }
}

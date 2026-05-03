using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public AudioRayCasting rayCasting;
    public float footstepInterval = 0.5f;
    public int rayCount = 20;

    private float timer;
    private bool isChasing = false;
    
    public void StartChasing()
    {
        isChasing = true;
    }

    private void Update()
    {
        if (!isChasing) return;

        timer += Time.deltaTime;
        if (timer >= footstepInterval)
        {
            timer = 0f;
            rayCasting.CastRadialRays(transform.position, rayCount);
        }
    }
}

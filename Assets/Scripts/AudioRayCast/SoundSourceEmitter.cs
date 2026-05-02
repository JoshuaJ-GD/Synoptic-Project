using UnityEngine;

public class SoundSourceEmitter : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioRayCasting rayCaster;
    public float interval = 1.0f;
    private float timer;

    private void Update()
    {
        if (!audioSource.isPlaying) return;

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            rayCaster.CastRadialRays(transform.position);
        }
    }
}

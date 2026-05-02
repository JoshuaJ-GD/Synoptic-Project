using System.Collections;
using UnityEngine;

public class LivingRoomPuzzle : MonoBehaviour
{
    [Header("Radio")]
    public AudioSource radio;
    public AudioClip radioSound;
    public float detectRadius = 2f;

    private Transform player;
    private bool radioStatus = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        radio.clip = radioSound;
        radio.loop = true;
        radio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (radioStatus) return;

        while (!radioStatus)
        {
            Physics.Raycast(transform.position, (player.position - transform.position).normalized, out RaycastHit hit);
            if (hit.collider.gameObject.layer == 6)
                radio.volume = 0.1f;
            else if (hit.collider.gameObject.tag == "Player")
                radio.volume = 1f;
        }

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= detectRadius) StartCoroutine(RadioOff());
    }

    IEnumerator RadioOff()
    {
        radioStatus = true;

        radio.Stop();
        yield return null;
    }
}

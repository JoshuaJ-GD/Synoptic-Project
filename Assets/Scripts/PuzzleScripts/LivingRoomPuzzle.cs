using System.Collections;
using UnityEngine;

public class LivingRoomPuzzle : MonoBehaviour
{
    [Header("Radio")]
    public AudioSource radio;
    public AudioClip radioSound;
    public AudioClip radioTurnOff;
    public float detectRadius = 2f;
    public float maxDist = 15f;
    public float minDist = 4f;

    [Header("Dialogue")]
    public string radioOffKnotName = "radio_off";

    private Transform player;
    private bool radioStatus = false;
    private bool radioStarted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tag = "PuzzlePiece";

        radio.clip = radioSound;
        radio.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (radioStatus) return;

        float dist = Vector3.Distance(transform.position, player.position);
        float distVol = Mathf.Clamp01(1f - (dist - minDist) / (maxDist - minDist));
        float targetVol = distVol * 0.5f;

        Physics.Raycast(transform.position, (player.position - transform.position).normalized, out RaycastHit hit);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 6)
                targetVol *= 0.2f;
            else if (hit.collider.CompareTag("Player"))
                targetVol = distVol * 1f;
        }

        radio.volume = targetVol;

        if (dist <= detectRadius) StartCoroutine(RadioOff());
    }

    IEnumerator RadioOff()
    {
        radioStatus = true;

        radio.Stop();
        radio.clip = radioTurnOff;
        radio.loop = false;
        radio.time = 1f;
        radio.Play();

        tag = "Untagged";

        yield return new WaitForSeconds(1f);

        DialogueManager.Instance.StartDialogue(radioOffKnotName);
    }

    public void ActivateRadio()
    {
        if (radioStatus) return;
        radioStarted = true;
        radio.clip = radioSound;
        radio.loop = true;
        radio.Play();
    }
}

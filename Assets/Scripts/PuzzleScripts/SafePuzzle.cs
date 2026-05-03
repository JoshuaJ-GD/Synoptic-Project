using System.Collections;
using UnityEngine;

public class SafePuzzle : MonoBehaviour
{
    [Header("Code Settings")]
    public string correctCode = "369";
    public float interactdist = 1.5f;

    [Header("Dialogue")]
    public string notReadyKnot = "safe_not_ready";
    public string wrongCodeKnot = "safe_wrong";
    public string correctCodeKnot = "safe_correct";

    private Transform player;
    private bool safeSolved = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (safeSolved) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= interactdist && Input.GetKeyDown(KeyCode.E) && !DialogueManager.Instance.IsDisplaying())
            TryOpenSafe();
    }

    void TryOpenSafe()
    {
        if (SafePuzzle.cluesFound < 3)
        {
            DialogueManager.Instance.StartDialogue(notReadyKnot);
            return;
        }

        if (SafePuzzle.foundCode == correctCode)
            StartCoroutine(OpenSafe());
        else
            DialogueManager.Instance.StartDialogue(wrongCodeKnot);
    }

    IEnumerator OpenSafe()
    {
        safeSolved = true;

        DialogueManager.Instance.StartDialogue(correctCodeKnot);

        yield return null;
    }
}

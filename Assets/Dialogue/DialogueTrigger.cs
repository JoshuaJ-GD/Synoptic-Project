using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink Settings")]
    public string knotName;

    [Header("Settings")]
    public bool triggerOnce = true;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { Debug.Log("other tag"); return; }
        if (triggerOnce && hasTriggered) return;

        Debug.Log("starting dialogue");

        hasTriggered = true;
        DialogueManager.Instance.StartDialogue(knotName);
    }
}

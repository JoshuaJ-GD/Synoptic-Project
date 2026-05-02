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
        if (!other.CompareTag("Player")) return;
        if (triggerOnce && hasTriggered) return;

        hasTriggered = true;
        DialogueManager.Instance.StartDialogue(knotName);
    }
}

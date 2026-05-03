using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Settings")]
    public bool triggerOnce = true;
    public string dialogueKnot;

    [Header("References")]
    public LivingRoomPuzzle livingRoomPuzzle;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (triggerOnce && hasTriggered) return;

        hasTriggered = true;

        if (livingRoomPuzzle != null)
            livingRoomPuzzle.ActivateRadio();

        if (!string.IsNullOrEmpty(dialogueKnot))
            DialogueManager.Instance.StartDialogue(dialogueKnot);
    }
}

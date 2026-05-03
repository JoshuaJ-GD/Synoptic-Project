using UnityEngine;

public class SafeClue : MonoBehaviour
{
    public string dialogueKnot;
    public int codeDigit;
    public int codePosition;
    public float interactDist = 1.5f;

    private Transform player;
    private bool found = false;

    public static int cluesFound = 0;
    public static string[] codeSlots = new string[3];

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cluesFound = 0;
        codeSlots = new string[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (found) return;

        float dist = Vector3.Distance(transform.position, player.position);

        Debug.Log($"Distance: {dist} | InteractDist: {interactDist}");

        if (dist <= interactDist)
            UIPromptScript.instance.Show();  
        else
            UIPromptScript.instance.Hide();

        if (dist <= interactDist && Input.GetKeyDown(KeyCode.E) && !DialogueManager.Instance.IsDisplaying())
            FindClue();
    }

    void FindClue()
    {
        found = true;
        UIPromptScript.instance.Hide();
        cluesFound++;
        codeSlots[codePosition] = codeDigit.ToString();
        gameObject.tag = "Untagged";
        DialogueManager.Instance.StartDialogue(dialogueKnot);
    }

    public static string GetFoundCode()
    {
        return string.Join("", codeSlots);
    }
}

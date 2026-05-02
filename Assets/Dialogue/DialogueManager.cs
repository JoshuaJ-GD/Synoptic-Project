using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Ink Story")]
    public TextAsset inkJSON;

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI promptText;

    private Story story;
    private bool isDisplaying = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        story = new Story(inkJSON.text);
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void StartDialogue(string knotName)
    {
        if (isDisplaying) return;

        story.ChoosePathString(knotName);
        dialoguePanel.SetActive(true);
        StartCoroutine(RunDialogue());
    }

    IEnumerator RunDialogue()
    {
        isDisplaying = true;
        promptText.text = "";

        while (story.canContinue)
        {
            string text = story.Continue();

            promptText.text = "[E] Continue";

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

            yield return new WaitForSeconds(0.1f);
            promptText.text = "";
        }

        EndDialogue();
    }

    void EndDialogue()
    {
        isDisplaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        promptText.text = "";
    }

    public bool IsDisplaying()
    {
        return isDisplaying;
    }
}


using TMPro;
using UnityEngine;

public class UIPromptScript : MonoBehaviour
{
    public static UIPromptScript instance;
    public TextMeshProUGUI promptText;

    private void Awake()
    {
        instance = this;
        promptText.gameObject.SetActive(false);
    }

    public void Show()
    {
        promptText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        promptText.gameObject.SetActive(false);
    }
}

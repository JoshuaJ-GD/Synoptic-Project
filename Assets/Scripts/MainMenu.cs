using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public TextMeshProUGUI pressAnyButton;

    [Header("Settings")]
    public string scene = "Game";
    public float fade = 2f;

    private bool canStart = false;
    private float timer = 0f;

    private void Start()
    {
        titleText.alpha = 0f;
        subtitleText.alpha = 0f;
        pressAnyButton.alpha = 0f;

        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if (!canStart) return;

        if (Input.anyKeyDown)
            StartCoroutine(LoadGame());

        timer += Time.deltaTime;
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fade)
        {
            titleText.alpha = elapsed / fade;
            elapsed += Time.deltaTime;
            yield return null;
        }
        titleText.alpha = 1f;

        yield return new WaitForSeconds(0.3f);

        elapsed = 0f;
        while (elapsed < fade * 0.5f)
        {
            subtitleText.alpha = elapsed / (fade * 0.5f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        subtitleText.alpha = 1f;

        yield return new WaitForSeconds(0.5f);

        canStart = true;
        StartCoroutine(FlashPressAnyButton());
    }

    IEnumerator FlashPressAnyButton()
    {
        while (true)
        {
            float elapsed = 0f;
            while (elapsed < 0.5f)
            {
                pressAnyButton.alpha = elapsed / 0.5f;
                elapsed += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            elapsed = 0f;
            while (elapsed < 0.5f)
            {
                pressAnyButton.alpha = 1f - elapsed / 0.5f;
                elapsed += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator LoadGame()
    {
        canStart = false;
        StopCoroutine(FlashPressAnyButton());

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            float alpha = 1f - elapsed;
            titleText.alpha = alpha;
            subtitleText.alpha = alpha;
            pressAnyButton.alpha = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(scene);
    }
}

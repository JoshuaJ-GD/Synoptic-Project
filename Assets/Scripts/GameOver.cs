using System.Collections;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameOverText2;
    public TextMeshProUGUI gameOverText3;

    public string scene = "Game";
    public float fadeInTime = 2f;

    private void Start()
    {
        gameOverText.text = "Test";
        //gameOverText.alpha = 0f;
        gameOverText2.alpha = 0f;
        gameOverText3.alpha = 0f;

        bool won = PlayerPrefs.GetInt("Won", 0) == 1;

        if (won)
        {
            gameOverText.text = "YOU ESCAPED!";
            gameOverText2.text = "but what was that...?";
            gameOverText3.text = "Press space to quit.";
        }
        else
        {
            gameOverText.text = "GAME OVER";
            gameOverText2.text = "The darkness has consumed you..";
            gameOverText3.text = "Press space to quit.";
        }

        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Application.Quit();
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeInTime)
        {
            gameOverText.alpha = elapsed / fadeInTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameOverText.alpha = 1f;

        yield return new WaitForSeconds(0.5f);

        // Fade in subtitle
        elapsed = 0f;
        while (elapsed < fadeInTime * 0.5f)
        {
            gameOverText2.alpha =
                elapsed / (fadeInTime * 0.5f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameOverText2.alpha = 1f;

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FlashQuitText());
    }

    IEnumerator FlashQuitText()
    {
        while (true)
        {
            float elapsed = 0f;
            while (elapsed < 0.5f)
            {
                gameOverText3.alpha = elapsed / 0.5f;
                elapsed += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            elapsed = 0f;
            while (elapsed < 0.5f)
            {
                gameOverText3.alpha = 1f - elapsed / 0.5f;
                elapsed += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f); 
        }
    }
}

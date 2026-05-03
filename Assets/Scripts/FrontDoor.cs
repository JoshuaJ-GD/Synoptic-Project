using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrontDoor : MonoBehaviour
{
    public bool requireSafeSolved = true;
    public string escaped = "you_escaped";
    public string locked = "door_locked";
    public string endScene = "EndScreen";
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StartCoroutine(Escape());
    }

    IEnumerator Escape()
    {
        if (requireSafeSolved && !SafePuzzle.safeSolved)
        {
            DialogueManager.Instance.StartDialogue(locked);
            yield break;
        }

        yield return new WaitForSeconds(1f);

        PlayerPrefs.SetInt("Won", 1);
        SceneManager.LoadScene("GameEnd");
    }
}

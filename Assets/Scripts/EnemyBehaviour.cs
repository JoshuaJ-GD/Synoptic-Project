using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    private float chaseSpeed = 3;
    public float catchDistance = 1.5f;
    public string caughtDialogue = "caught";
    public GameObject door;

    private NavMeshAgent agent;
    private Transform player;
    private bool isCaught = false;
    private bool isChasing = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = chaseSpeed;
    }

    public void StartChasing()
    {
        isChasing = true;
        GetComponent<EnemyFootsteps>().StartChasing();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChasing || isCaught) return;

        door.tag = "PuzzlePiece";
        agent.SetDestination(player.position);
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= catchDistance)
        {
            StartCoroutine(CatchPlayer());
        }
            
    }

    IEnumerator CatchPlayer()
    {
        isCaught = true;
        isChasing = false;
        agent.isStopped = true;

        yield return new WaitForSeconds(1f);

        PlayerPrefs.SetInt("Won", 0);
        SceneManager.LoadScene("GameEnd");
    }
}

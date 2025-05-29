using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;

    public GameObject gameoverpanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameoverpanel != null)
        {
            gameoverpanel.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameoverpanel != null)
            {
                gameoverpanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}

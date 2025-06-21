using UnityEngine;

public class ShowGameOverOnPlayerTouch : MonoBehaviour
{
    public GameObject gameOverScreen;

    void Start()
    {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}

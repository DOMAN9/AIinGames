using TMPro;
using UnityEngine;
using UnityEngine.UI; // for optional UI

public class Collector : MonoBehaviour
{
    public int totalItems = 3;
    private int itemsCollected = 0;

    public GameObject winScreen;
    public GameObject interactPrompt; // Optional: UI Text like “Press F to Pick Up”
    public TextMeshProUGUI itemCounterText; // Optional: UI item count display

    private GameObject nearbyItem;

    void Start()
    {
        if (winScreen != null) winScreen.SetActive(false);
        if (interactPrompt != null) interactPrompt.SetActive(false);
        UpdateItemCounter();
    }

    void Update()
    {
        if (nearbyItem != null && Input.GetKeyDown(KeyCode.F))
        {
            CollectItem(nearbyItem);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            nearbyItem = other.gameObject;
            if (interactPrompt != null) interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectible") && other.gameObject == nearbyItem)
        {
            nearbyItem = null;
            if (interactPrompt != null) interactPrompt.SetActive(false);
        }
    }

    void CollectItem(GameObject item)
    {
        itemsCollected++;
        Destroy(item);
        nearbyItem = null;
        if (interactPrompt != null) interactPrompt.SetActive(false);
        UpdateItemCounter();

        if (itemsCollected >= totalItems)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("✅ You Win! All items collected.");
        if (winScreen != null) winScreen.SetActive(true);
        Time.timeScale = 0f; // freeze game
    }

    void UpdateItemCounter()
    {
        if (itemCounterText != null)
            itemCounterText.text = $"Items: {itemsCollected} / {totalItems}";
    }
}

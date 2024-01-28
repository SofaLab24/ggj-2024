using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class KebisController : MonoBehaviour
{

    // This function is called when the Collider other enters the trigger
    public TriangleMarozController playerController;
    public float timeRemaining = 60;
    public TextMeshProUGUI textTime;    
    void Start()
    {
        playerController = FindFirstObjectByType<TriangleMarozController>();
        textTime = GameObject.Find("TimeRemainingText")?.GetComponent<TextMeshProUGUI>();
    
    }

    void Update() {
        textTime.text = timeRemaining.ToString("F2");
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        if (timeRemaining <= 0) {
            playerController.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Call a function to handle winning the game
            WinGame();
        }
    }

    // Function to handle winning the game
    private void WinGame()
    {
        // Add your win logic here, such as displaying a win message,
        // returning to the main UI, or loading a new scene.
        
        // For example, you can load a new scene after a delay:
        Invoke("LoadMainUI", 2f);
    }

    // Function to load the main UI scene
    private void LoadMainUI()
    {
        playerController.Points += PlayerPrefs.GetFloat("money");
        playerController.Points = Mathf.Round(playerController.Points * 100f) / 100f;
        PlayerPrefs.SetFloat("money", playerController.Points);
        // TODO: Implement win logic
        Debug.Log("You win!");
        Debug.Log("Scored points: " + playerController.Points);
        SceneManager.LoadScene(0);
    }
}

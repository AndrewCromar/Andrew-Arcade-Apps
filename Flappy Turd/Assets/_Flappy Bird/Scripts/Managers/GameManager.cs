using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private Transform groundCollider;
    [Space]
    [SerializeField] private GameObject startCanvas;
    [Space]
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private TextMeshProUGUI gamePointsText;
    [Space]
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private TextMeshProUGUI endPointsText;

    [Header("Settings")]
    [SerializeField] private float speed = 1;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [Space]
    [SerializeField] private float pipeSpawnRate;

    [Header("Debug")]
    [SerializeField] private int score;
    [SerializeField] private float pipeCounter;
    [SerializeField] private bool gameStarted;

    private void Awake()
    {
        instance = this;

        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Update width and height variables
        height = mainCamera.orthographicSize * 2;
        width = mainCamera.orthographicSize * mainCamera.aspect * 2;

        groundCollider.position = new Vector2(GetHalfWidth() * -1 / 2, GetHalfHeight() * -1 - 0.5f);

        // Set points text
        gamePointsText.text = "\n" + score.ToString();

        // Spawn pipes
        if (gameStarted)
        {
            pipeCounter -= Time.deltaTime;
            if (pipeCounter <= 0)
            {
                pipeCounter = pipeSpawnRate;
                Instantiate(pipePrefab, transform.position, Quaternion.identity);
            }
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        startCanvas.GetComponent<Animator>().SetBool("leave", true);
        gameCanvas.GetComponent<Animator>().SetBool("enter", true);
    }

    public void PlayerDied()
    {
        speed = -5;
        endPointsText.text = "Score: " + score.ToString() + "\nFlap to continue...";
        endCanvas.GetComponent<Animator>().SetBool("enter", true);
        gameCanvas.GetComponent<Animator>().SetBool("exit", true);
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public float GetSpeed() { return speed; }
    public float GetWidth() { return width; }
    public float GetHalfWidth() { return width / 2; }
    public float GetHeight() { return height; }
    public float GetHalfHeight() { return height / 2; }
    public bool GetGameStarted() { return gameStarted; }

    public void AddScore(int _score) { score += _score; }
}

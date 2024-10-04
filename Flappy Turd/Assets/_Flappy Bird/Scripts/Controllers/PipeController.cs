using UnityEngine;

public class PipeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject headPrefab;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private BoxCollider2D pointCollider;

    [Header("Settings")]
    [SerializeField] private float xBuffer = 0.5f;
    [SerializeField] private float yBuffer = 5;
    [Space]
    [SerializeField] private int pipeHeight = 10;
    [SerializeField] private float gapSize = 2.5f;

    private void Start()
    {
        // Random y position
        float halfWidth = GameManager.instance.GetHalfWidth();
        float halfHeight = GameManager.instance.GetHalfHeight();
        transform.position = new Vector2(halfWidth + xBuffer, Random.Range(-halfHeight + gapSize, halfHeight - gapSize)); // Random.Range(halfHeight - yBuffer * -1, halfHeight - yBuffer));

        // Spawn graphics
        SpawnBottom();
        SpawnTop();

        pointCollider.size = new Vector2(pointCollider.size.x, 2 * GameManager.instance.GetHeight());
    }

    private void Update()
    {
        float xDeltaPosition = GameManager.instance.GetSpeed() * -1 * Time.deltaTime;

        transform.position = transform.position + new Vector3(xDeltaPosition, 0, 0);

        if (transform.position.x <= GameManager.instance.GetWidth() / 2 * -1 - xBuffer) Destroy(gameObject);
    }

    #region Spawning
    private void SpawnBottom()
    {
        Transform bottomHead = Instantiate(headPrefab, (Vector2)transform.position + new Vector2(0, -gapSize), Quaternion.identity, transform).transform;
        for (int i = 1; i <= pipeHeight; i++)
        {
            Instantiate(bodyPrefab, (Vector2)bottomHead.position + new Vector2(0, -i), Quaternion.identity, transform);
        }
    }

    private void SpawnTop()
    {
        Transform topHead = Instantiate(headPrefab, (Vector2)transform.position + new Vector2(0, gapSize), Quaternion.Euler(0, 0, 180), transform).transform;
        topHead.localScale = new Vector2(-1, 1);
        for (int i = 1; i <= pipeHeight; i++)
        {
            Instantiate(bodyPrefab, (Vector2)topHead.position + new Vector2(0, i), Quaternion.Euler(0, 0, 180), transform).transform.localScale = new Vector2(-1, 1);
        }
    }
    #endregion
}
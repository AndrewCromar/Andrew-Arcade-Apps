using UnityEngine;

public class ObjectSuicide : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float timeTillDeath;

    private void Update()
    {
        timeTillDeath -= Time.deltaTime;
        if (timeTillDeath <= 0) Destroy(gameObject);
    }
}

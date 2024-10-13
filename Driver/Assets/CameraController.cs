using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public static CameraController instance;

    [Header("References")]
    [SerializeField] private Transform target;

    [Header("Settings")]
    [SerializeField] private Vector3 offset;
    [Space]
    [SerializeField] private float positionSmoothing;

    private void Awake() { instance = this; }

    private void Update()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, new Vector3(0, target.position.y, 0) + offset, positionSmoothing * Time.deltaTime);
    }

    public void SetTarget(Transform _target) { target = _target; }
}
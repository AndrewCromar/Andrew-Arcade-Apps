using ONYX;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject flapSound;
    [SerializeField] private GameObject pointSound;
    [SerializeField] private GameObject deathSound;

    [Header("Settings")]
    [SerializeField] private float flapStrength = 7.5f;
    [SerializeField] private float dieVelocity = 12.5f;
    [SerializeField] private float gravity = -19.62f;
    [SerializeField] private float maxAngle = 45;
    [SerializeField] private float zPosition = -10;
    [SerializeField] private string pointTag = "point";
    [SerializeField] private float deadRotationSpeed = 1;

    [Header("Debug")]
    [SerializeField] private float yVelocity;
    [SerializeField] private bool dead;

    private void Update()
    {
        if (GameManager.instance.GetGameStarted())
        {
            Position();
            Rotation();
        }

        float halfHeight = GameManager.instance.GetHalfHeight();
        float offset = 0.25f;
        transform.position = new Vector3(GameManager.instance.GetHalfWidth() * -1 / 2, Mathf.Clamp(transform.position.y, -halfHeight - offset, halfHeight - offset), zPosition);
    }

    #region Movement
    private void Position()
    {
        yVelocity += gravity * Time.deltaTime;

        float yDeltaPosition = yVelocity * Time.deltaTime;
        transform.position = transform.position + new Vector3(0, yDeltaPosition, 0);
    }

    private void Rotation()
    {
        if (!dead)
        {
            float rotation = yVelocity * (maxAngle / flapStrength);
            rotation = Mathf.Clamp(rotation, -maxAngle, maxAngle);

            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        else
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0, 0, deadRotationSpeed);
        }
    }

    public void FlapInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GameManager.instance.StartGame();
            if (!dead)
            {
                yVelocity = flapStrength;
                Instantiate(flapSound);
            }
            else
            {
                GameManager.instance.Continue();
            }
        }
    }
    #endregion

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!dead)
        {
            if (col.CompareTag(pointTag))
            {
                GameManager.instance.AddScore(1);
                Instantiate(pointSound);
            }
            else
            {
                dead = true;
                yVelocity = dieVelocity;
                Instantiate(deathSound);
                GameManager.instance.PlayerDied();
            }
        }
    }

    public void OnDriverButton(InputAction.CallbackContext ctx) { if (ctx.performed) AndrewArcadeTools.ReturnToDriver(); }
}
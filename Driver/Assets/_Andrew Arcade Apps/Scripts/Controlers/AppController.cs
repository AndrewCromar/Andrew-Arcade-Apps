using UnityEngine;

public class AppController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Debug")]
    [SerializeField] private AppProfile appProfile;

    public void SetupApp(AppProfile _appProfile)
    {
        appProfile = _appProfile;

        spriteRenderer.sprite = Resources.Load<Sprite>("Apps/" + appProfile.icon);

        if (spriteRenderer.sprite == null)
        {
            Debug.LogWarning("Failed to load sprite: " + "Apps/" + appProfile.icon);
        }
    }
}

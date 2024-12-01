using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Text titleText;
    [SerializeField] private Text developerText;

    [Header("Debug")]
    [SerializeField] private ONYX.AppProfile appProfile;

    public void SetupApp(ONYX.AppProfile _appProfile)
    {
        appProfile = _appProfile;

        spriteRenderer.sprite = Resources.Load<Sprite>("Apps/" + appProfile.icon);
        if (spriteRenderer.sprite == null) Debug.LogWarning("Failed to load sprite: " + "Apps/" + appProfile.icon);

        titleText.text = appProfile.title;
        developerText.text = appProfile.developer;
    }
}

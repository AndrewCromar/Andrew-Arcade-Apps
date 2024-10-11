using UnityEngine;
using TMPro;

public class CartridgeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Cartridge cartridge;
    [Space]
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text developer;

    private void Setup()
    {
        icon.sprite = cartridge.appIcon;
        title.text = cartridge.appName;
        developer.text = cartridge.appDeveloper;
    }

    public void SetCartridge(Cartridge _cartridge)
    {
        cartridge = _cartridge;
        Setup();
    }
}
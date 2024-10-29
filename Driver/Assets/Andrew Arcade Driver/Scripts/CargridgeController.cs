using UnityEngine;
using TMPro;

public class CartridgeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Cartridge cartridge;
    [SerializeField] private Renderer objectRenderer;
    [Space]
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text developer;

    [Header("Debug")]
    [SerializeField] private bool hovered;
    [SerializeField] private float yRotationOffset;

    private void Setup()
    {
        icon.sprite = cartridge.appIcon;
        title.text = cartridge.appName;
        developer.text = cartridge.appDeveloper;

        objectRenderer.material.color = cartridge.cartridgeColor;
        objectRenderer.material.mainTexture = cartridge.cartridgeTexture;
    }

    public void SetCartridge(Cartridge _cartridge)
    {
        cartridge = _cartridge;
        Setup();
    }

    public void Hover() { hovered = true; }
    public void DeHover() { hovered = false; }
    public bool GetHovered() { return hovered; }

    public void SetYRotationOffset(float _yRotationOffset) { yRotationOffset = _yRotationOffset; }
    public float GetYRotationOffset() { return yRotationOffset; }
}
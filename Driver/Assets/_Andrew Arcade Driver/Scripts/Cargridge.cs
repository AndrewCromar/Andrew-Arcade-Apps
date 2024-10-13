using UnityEngine;

[CreateAssetMenu(fileName = "Cartridge Data", menuName = "Cartridge")]
public class Cartridge : ScriptableObject
{
    public string appName;
    public string appDeveloper;
    public Sprite appIcon;
    public Color cartridgeColor;
    public Texture2D cartridgeTexture;
}
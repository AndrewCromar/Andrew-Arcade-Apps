using UnityEngine;

[CreateAssetMenu(fileName = "App Data", menuName = "App")]
public class AppScriptableObject : ScriptableObject
{
    public string appName;
    public string appDeveloper;
    public Sprite appIcon;
}
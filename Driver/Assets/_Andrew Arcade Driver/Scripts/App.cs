using UnityEngine;

[CreateAssetMenu(fileName = "App Data", menuName = "App")]
public class App : ScriptableObject
{
    public string appName;
    public string appDeveloper;
    public Sprite appIcon;
}
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

public class DriverController : MonoBehaviour
{
    [Header("References")]
    public AppScriptableObject flappyTurd;

    private void Start()
    {
        UnityEngine.Debug.Log(ToCamelcase(flappyTurd.appName));
    }

    public void StartApp()
    {
        Process.Start("C:/Users/scrom/AppData/Local/Microsoft/WindowsApps/mspaint.exe");
    }

    public string ToCamelcase(string _text)
    {
        string lower = _text.ToLower();
        List<string> wordList = new List<string>(lower.Split(' '));
        string firstWord = wordList[0];
        wordList.RemoveAt(0);
        string finalCamelcase = firstWord;
        foreach (string word in wordList)
        {
            finalCamelcase += char.ToUpper(word[0]) + word.Substring(1);
        }
        return finalCamelcase;
    }
}

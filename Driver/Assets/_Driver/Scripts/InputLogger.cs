using UnityEngine;
using UnityEngine.InputSystem;

public class InputLogger : MonoBehaviour
{
    void Start()
    {
        // Debug.developerConsoleVisible = true;
    }

    private void Update()
    {
        foreach (var device in InputSystem.devices)
        {
            foreach (var control in device.allControls)
            {
                var value = control.ReadValueAsObject();
                if (value != null && !value.Equals(default))
                {
                    Debug.Log($"Device: {device.displayName}, Control: {control.path}, Value: {value}");
                }
            }
        }
    }
}
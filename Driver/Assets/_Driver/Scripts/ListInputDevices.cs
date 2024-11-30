using UnityEngine;
using UnityEngine.InputSystem;

public class ListInputDevices : MonoBehaviour
{
    void Start()
    {
        DriverController.instance.Output("Listing all connected input devices...");

        foreach (var device in InputSystem.devices)
        {
            DriverController.instance.Output($" > Device: {device.displayName}, Type: {device.GetType()}");
        }

        DriverController.instance.Output("Done!");
    }
}
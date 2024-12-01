using UnityEngine;
using UnityEngine.InputSystem;

namespace ONYX
{
    public class Debug_ListInputDevices : MonoBehaviour
    {
        void Start()
        {
            Debug.Log("Listing all connected input devices...");

            foreach (var device in InputSystem.devices)
            {
                Debug.Log($" > Device: {device.displayName}, Type: {device.GetType()}");
            }

            Debug.Log("Done!");
        }
    }
}
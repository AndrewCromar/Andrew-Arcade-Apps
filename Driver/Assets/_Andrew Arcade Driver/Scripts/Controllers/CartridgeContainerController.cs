using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using System.IO;

public class CartridgeContainerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cartridgeContainer;
    [SerializeField] private GameObject cartridgePrefab;
    [Space]
    [SerializeField] private List<Cartridge> cartridges = new List<Cartridge>();

    [Header("Settings")]
    [SerializeField] private float small;
    [SerializeField] private float large;
    [Space]
    [SerializeField] private float positionSmoothing;
    [SerializeField] private float rotationSmoothing;
    [Space]
    [SerializeField] private float randomYRotationOffset;
    [Space]
    [SerializeField] private Vector3 upPosition;
    [SerializeField] private Vector3 downPosition;
    [Space]
    [SerializeField] private Vector3 upRotation;
    [SerializeField] private Vector3 downRotation;
    [Space]
    [SerializeField] private float sinAplitude;
    [SerializeField] private float sinFrequency;

    [Header("Debug")]
    [SerializeField] private int currentHovered = 0;
    [SerializeField] private List<GameObject> loadedCartridges = new List<GameObject>();

    [Header("Inputs")]
    [SerializeField] private bool upInputQueued;
    [SerializeField] private bool downInputQueued;
    [SerializeField] private bool selectInputQueued;

    private void Start()
    {
        GenerateCatridges();
        LoadCartriges();
    }

    private void GenerateCatridges()
    {
        string dataFolder = "Assets/Apps/Data";
        
        // Check if the folder exists
        if (AssetDatabase.IsValidFolder(dataFolder))
        {
            Debug.Log("Folder found: " + dataFolder);

            // Get all files in the folder
            string[] files = Directory.GetFiles(dataFolder);
            foreach (string file in files)
            {
                // Get the file name without the full path
                string fileName = Path.GetFileName(file);
                
                // Filter for .json files and exclude .meta files
                if (fileName.EndsWith(".json"))
                {
                    Debug.Log("JSON File: " + fileName); // Print only the file name

                    _cartridge = new Cartridge();

                    

                    cartridges.Append(_cartridge)
                }
            }
        }
        else
        {
            Debug.Log("Folder does not exist: " + dataFolder);
        }
    }

    private void Update()
    {
        if (upInputQueued)
        {
            currentHovered++;
            upInputQueued = false;
        }
        else if (downInputQueued)
        {
            currentHovered--;
            downInputQueued = false;
        }
        else if (selectInputQueued)
        {
            Debug.Log("Launch app.");
            selectInputQueued = false;
        }
        currentHovered = Mathf.Clamp(currentHovered, 0, loadedCartridges.Count - 1);

        foreach (GameObject cartridge in loadedCartridges)
        {
            CartridgeController cartridgeController = cartridge.GetComponent<CartridgeController>();
            if (cartridgeController.GetHovered())
            {
                cartridgeController.SetYRotationOffset(Random.Range(-randomYRotationOffset, randomYRotationOffset));
            }
            cartridgeController.DeHover();
        }
        loadedCartridges[currentHovered].GetComponent<CartridgeController>().Hover();

        CameraController.instance.SetTarget(loadedCartridges[currentHovered].transform);

        float yPos = 0;
        for (int i = 0; i < loadedCartridges.Count; i++)
        {
            CartridgeController cartridgeController = loadedCartridges[i].GetComponent<CartridgeController>();
            bool hovered = cartridgeController.GetHovered();
            bool nextHovered = loadedCartridges[Mathf.Clamp(i + 1, 0, loadedCartridges.Count - 1)].GetComponent<CartridgeController>().GetHovered();


            if (i == 0) if (hovered) yPos += large - +(small / 2); else yPos += small / 2;

            loadedCartridges[i].transform.position = Vector3.Lerp(loadedCartridges[i].transform.position, new Vector3(0, yPos, 0) + (hovered ? upPosition : downPosition), positionSmoothing * Time.deltaTime);
            loadedCartridges[i].transform.rotation = Quaternion.Lerp(loadedCartridges[i].transform.rotation, Quaternion.Euler(hovered ? upRotation + new Vector3(sinAplitude * Mathf.Sin(sinFrequency * Time.time), sinAplitude * Mathf.Cos(sinFrequency * Time.time), 0) : downRotation + new Vector3(0, cartridgeController.GetYRotationOffset(), 0)), rotationSmoothing * Time.deltaTime);

            if (hovered || nextHovered) yPos += large; else yPos += small;
        }
    }

    private void LoadCartriges()
    {
        foreach (Cartridge cartridge in cartridges)
        {
            loadedCartridges.Add(Instantiate(cartridgePrefab, cartridgeContainer));
            CartridgeController cartridgeController = loadedCartridges[loadedCartridges.Count - 1].GetComponent<CartridgeController>();
            cartridgeController.SetCartridge(cartridge);
            cartridgeController.SetYRotationOffset(Random.Range(-randomYRotationOffset, randomYRotationOffset));
        }
    }

    public void UpInput(InputAction.CallbackContext ctx) { if (ctx.performed) upInputQueued = true; }
    public void DownInput(InputAction.CallbackContext ctx) { if (ctx.performed) downInputQueued = true; }
    public void SelectInput(InputAction.CallbackContext ctx) { if (ctx.performed) selectInputQueued = true; }
}
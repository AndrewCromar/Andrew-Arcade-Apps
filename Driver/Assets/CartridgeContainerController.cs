using System.Collections.Generic;
using UnityEngine;

public class CartridgeContainerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cartridgeContainer;
    [SerializeField] private GameObject cartridgePrefab;
    [Space]
    [SerializeField] private List<Cartridge> cartridges = new List<Cartridge>();

    [Header ("Debug")]
    [SerializeField] private List<CartridgeController>() loadedCartridges = new List<CartridgeController>();

    private void Start()
    {
        LoadCartriges();
    }

    private void LoadCartriges()
    {
        foreach (Cartridge cartridge in cartridges)
        {
            Instantiate(cartridgePrefab, cartridgeContainer).GetComponent<CartridgeController>().SetCartridge(cartridge);
        }
    }
}
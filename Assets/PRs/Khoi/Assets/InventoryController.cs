using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryScreen;

    void Start()
    {
        // Inventory starts hidden
        inventoryScreen.SetActive(false);
    }

    void Update()
    {
        // Press E to open/close inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }

        // Press Escape to close inventory
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
        }
    }

    public void ToggleInventory()
    {
        inventoryScreen.SetActive(!inventoryScreen.activeSelf);
    }

    public void OpenInventory()
    {
        inventoryScreen.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryScreen.SetActive(false);
    }
}
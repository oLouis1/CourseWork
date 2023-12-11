using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Item[] items;
    public Transform cameraTransform;
    public Vector3 offset;
    public GameObject inventory;

    // Start is called before the first frame update
    void Start()
    {
        items = new Item[20];   //ammount of items in the inventory.
        inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //cameraTransform.position = transform.position + offset; //keep the inventory infront of the camera.

        if (Input.GetKeyDown(KeyCode.Q))//opening the invetory
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale=1f;
            }
            else
            {
                inventory.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
            
        }

    }

    void openInventory()
    {

    }
}

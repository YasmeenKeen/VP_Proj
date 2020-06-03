using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInit : MonoBehaviour
{
    public GameObject itemSlot;
    public GameObject parentGameObject;
    // Start is called before the first frame update
    void Start()
    {
        if (itemSlot != null)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Vector3 position = new Vector3(0, 0, 1);
                    // instantiate the object
                    GameObject go = (GameObject)Instantiate(itemSlot, position, Quaternion.identity);
                    go.name = "Slot("+x+","+y+")";
                    go.transform.SetParent(parentGameObject.transform, false);
                    go.GetComponent<ItemSlot>().x = x;
                    go.GetComponent<ItemSlot>().y = y;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

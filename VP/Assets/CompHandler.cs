using System.Collections.Generic;
using UnityEngine;

// rename this class to suit your needs
public class CompHandler : MonoBehaviour
{
    // the Equip prefab - required for instantiation
    public GameObject equipPrefab;

    public GameObject parentGameObject;
    public GameObject attab;
    public GameObject window;
    public GameObject arrowTab;
    public List<GameObject> createdObjects = new List<GameObject>();

    public void Start()
    {
        arrowTab = GameObject.Find("ArrowTab");
    }
    [System.Obsolete]
    public void CreateComp()
    {
        window = GameObject.Find("Window");
        // a prefab is need to perform the instantiation
        if (equipPrefab != null)
        {
            if (equipPrefab.name.Equals("Arrow"))
            {
                arrowTab.active = true;
                window = GameObject.Find("WindowRel");
            }
            else
            {
                arrowTab.active = false;
            }
            foreach (Transform child in window.transform)
            {
                Destroy(child.gameObject);
            }
            // get a random postion to instantiate the prefab - you can change this to be created at a fied point if desired
            Vector3 position = new Vector3(0, 0, 1);
            // instantiate the object
            GameObject go = (GameObject)Instantiate(equipPrefab, position, Quaternion.identity);
            go.name = equipPrefab.name;
            go.transform.SetParent(parentGameObject.transform, false);
            createdObjects.Add(go);
        }
    }
}
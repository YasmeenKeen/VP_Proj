using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawHandler : MonoBehaviour
{
    public GameObject currGO;
    public GameObject parentGameObject;
    public List<GameObject> createdObjects = new List<GameObject>();
    public int objCounter = 1;

    public TMP_InputField colourIF;
    public TMP_InputField textIF;
    public TMP_InputField classIF;
    public TMP_InputField styleIF;
    public TMP_InputField imageIF;

    public void UpdateCurrGO(string str)
    {
        currGO = GameObject.Find("Window/" + str);
    }

    [System.Obsolete]
    public void DragActivate()
    {
        // a prefab is need to perform the instantiation
        if (currGO != null)
        {
            Vector3 position = new Vector3(0, 0, 1);
            GameObject newCurrGO = (GameObject)Instantiate(currGO, position, Quaternion.identity);
            newCurrGO.name = "Comp" + objCounter;
            newCurrGO.transform.SetParent(parentGameObject.transform, false);
            newCurrGO.GetComponent<CompIndv>().ctype = currGO.name;
            SetAttributeSettings(newCurrGO);
            newCurrGO.GetComponent<DragDrop>().SetDragActivated();
            createdObjects.Add(newCurrGO);
            ClearAttributeSettings();
            DestroyObject(currGO);
            objCounter++;
        }
    }

    private void SetAttributeSettings(GameObject go)
    {
        go.GetComponent<CompIndv>().ccolour = colourIF.text;
        go.GetComponent<CompIndv>().ctext = textIF.text;
        go.GetComponent<CompIndv>().cclass = classIF.text;
        go.GetComponent<CompIndv>().cstyle = styleIF.text;
        go.GetComponent<CompIndv>().cimage = imageIF.text;
    }

    private void ClearAttributeSettings()
    {
        colourIF.text = string.Empty;
        textIF.text = string.Empty;
        classIF.text = string.Empty;
        styleIF.text = string.Empty;
        imageIF.text = string.Empty;
    }
}
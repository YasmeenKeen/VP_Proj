using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawHandler : MonoBehaviour
{
    public GameObject currGO;
    public GameObject parentGameObject;
    public List<GameObject> createdObjects = new List<GameObject>();
    public int objCounter = 1;

    public TMP_InputField xIF;
    public TMP_InputField yIF;
    public TMP_InputField colourIF;
    public TMP_InputField textIF;
    public TMP_InputField classIF;
    public TMP_InputField styleIF;
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
            colourIF.text = "";
            objCounter++;

        }

    }

    private void SetAttributeSettings(GameObject go)
    {
        
        go.GetComponent<CompIndv>().cposX = xIF.text;
        go.GetComponent<CompIndv>().cposY = yIF.text;
        go.GetComponent<CompIndv>().ccolour = colourIF.text;
        go.GetComponent<CompIndv>().ctext = textIF.text;
        go.GetComponent<CompIndv>().cclass = classIF.text;
        go.GetComponent<CompIndv>().cstyle = styleIF.text;
    }
    private void ClearAttributeSettings()
    {
        xIF.text = string.Empty;
        yIF.text = string.Empty;
        colourIF.text = string.Empty;
        textIF.text = string.Empty;
        classIF.text = string.Empty;
        styleIF.text = string.Empty;
    }

}

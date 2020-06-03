using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
            colourIF.text = "";
            objCounter++;
            ParseToTextFile();
        }
    }

    public void ParseToTextFile()
    {
        List<string> lines = new List<string>();
        for (int i = 0; i < createdObjects.Count; i++)
        {
            CompIndv obj = createdObjects[i].GetComponent<CompIndv>();
            string id = i + "";
            string type = obj.ctype;
            string text = obj.ctext;
            string colour = obj.ccolour;
            string cclass = obj.cclass;
            string style = obj.cstyle;
            string htmlType = "";
            switch (type)
            {
                case ("Button"):
                    htmlType = "button";
                    break;

                case ("Image"):
                    htmlType = "img";
                    break;

                case ("Header"):
                    htmlType = "header";
                    break;

                case ("Parag"):
                    htmlType = "p";
                    break;

                case ("Div"):
                    htmlType = "div";
                    break;
            }
            string line = id;
            line += htmlType.Length > 0 ? " " + htmlType : "";
            line += text.Length > 0 ? " " + "text=" + text : "";
            line += colour.Length > 0 ? " " + "color=" + colour : "";
            line += cclass.Length > 0 ? " " + "class=" + cclass : "";
            line += style.Length > 0 ? " " + "style=" + style : "";
            //line+= " "+"childof=";
            lines.Add(line);
        }
        CreateTextFile(lines);
    }

    public void CreateTextFile(List<string> lines)
    {
        using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("Output.txt"))
        {
            foreach (string line in lines)
            {
                file.WriteLine(line);
            }
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
        go.GetComponent<CompIndv>().cimage = imageIF.text;
    }

    private void ClearAttributeSettings()
    {
        xIF.text = string.Empty;
        yIF.text = string.Empty;
        colourIF.text = string.Empty;
        textIF.text = string.Empty;
        classIF.text = string.Empty;
        styleIF.text = string.Empty;
        imageIF.text = string.Empty;
    }
}
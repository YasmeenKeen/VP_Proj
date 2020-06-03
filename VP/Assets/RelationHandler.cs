using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RelationHandler : MonoBehaviour
{
    public GameObject currGO;
    public GameObject parentGameObject;
    public List<GameObject> createdRelations = new List<GameObject>();
    public int relCounter = 1;

    public TMP_InputField pxIF;
    public TMP_InputField pyIF;
    public TMP_InputField cxIF;
    public TMP_InputField cyIF;

    public int px;
    public int py;
    public int cx;
    public int cy;

    public Vector3 ParentPos;
    public Vector3 ChildPos;

    public void UpdateCurrGO(string str)
    {
        currGO = GameObject.Find("WindowRel/" + str);
    }

    [System.Obsolete]
    public void AddRelation()
    {
        // a prefab is need to perform the instantiation
        if (currGO != null)
        {
            ParentPos = GetParentCoords();

            ChildPos = GetChildCoords();
            Vector3 position = ParentPos;
            GameObject newCurrGO = (GameObject)Instantiate(currGO, position, Quaternion.identity);
            newCurrGO.name = "Rel" + relCounter;
            newCurrGO.transform.SetParent(parentGameObject.transform, false);
            newCurrGO.GetComponent<RelIndv>().ctype = currGO.name;
            SetAttributeSettings(newCurrGO);
            SetRelationship(newCurrGO);
            createdRelations.Add(newCurrGO);

            //Vector3 relative = ChildPos - ParentPos;
            //float maggy = relative.magnitude;

            //newCurrGO.transform.localScale = new Vector3(1, 1 , 0);

            //Vector3 midPointVector = (ChildPos + ParentPos) / 2;

            //newCurrGO.transform.position = midPointVector;

            ////        Quaternion rotationVector = Quaternion.LookRotation (relative);
            ////        rotationVector.z = 0;
            ////        rotationVector.w = 0;
            ////        transform.rotation = rotationVector - 90;

            //float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg ;
            //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            //newCurrGO.transform.rotation = q;

            //ParseToTextFile();

            
            ClearAttributeSettings();
            DestroyObject(currGO);
            relCounter++;
        }
    }

    public void SetRelationship(GameObject go)
    {
        go.GetComponent<RelIndv>().parent= GameObject.Find("Slot(" + px + "," + py + ")").GetComponent<ItemSlot>().inblock;
        go.GetComponent<RelIndv>().child = GameObject.Find("Slot(" + cx + "," + cy + ")").GetComponent<ItemSlot>().inblock;
       
    }

    public Vector3 GetParentCoords()
    {
        px = int.Parse(pxIF.text);
        py = int.Parse(pyIF.text);

        float x = 80 + (100 * (1 + px));
        float y = 20 - (100 * (py));
        return new Vector3(x, y, 0);
    }

    public Vector3 GetChildCoords()
    {
        cx = int.Parse(cxIF.text);
        cy = int.Parse(cyIF.text);
        float x = cx;
        float y = cy;
        return new Vector3(x, y, 0);
    }

    //unuseable rn so Yasmeen can check it to make sure
    public void ParseToTextFile()
    {
        List<string> lines = new List<string>();
        for (int i = 0; i < createdRelations.Count; i++)
        {
            CompIndv obj = createdRelations[i].GetComponent<CompIndv>();
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
        go.GetComponent<RelIndv>().pX = pxIF.text;
        go.GetComponent<RelIndv>().pY = pyIF.text;
        go.GetComponent<RelIndv>().cX = cxIF.text;
        go.GetComponent<RelIndv>().cY = cyIF.text;
    }

    private void ClearAttributeSettings()
    {
        pxIF.text = string.Empty;
        pyIF.text = string.Empty;
        cxIF.text = string.Empty;
        cyIF.text = string.Empty;
    }

}
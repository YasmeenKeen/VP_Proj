using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public GameObject compHandle;
    public GameObject relHandle;
    public List<GameObject> compCreated;
    public List<GameObject> relCreated;

    public void ClearChildren()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ParseChildren()
    {
        compCreated = compHandle.GetComponent<DrawHandler>().createdObjects;
        relCreated = relHandle.GetComponent<RelationHandler>().createdRelations;
        ParseToTextFile();
    }

    public void ParseToTextFile()
    {
        List<string> lines = new List<string>();
        for (int i = 0; i < compCreated.Count; i++)
        {
            CompIndv obj = compCreated[i].GetComponent<CompIndv>();
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

        for (int i = 0; i < relCreated.Count; i++)
        {
            RelIndv obj = relCreated[i].GetComponent<RelIndv>();
            string id = i + "";
            string parent = obj.parent.name;
            string child = obj.child.name;
            string htmlType = "relation";
            
            string line = id;
            line += htmlType.Length > 0 ? " " + htmlType : "";
            line += parent.Length > 0 ? " " + "parent=" + parent : "";
            line += child.Length > 0 ? " " + "child=" + child : "";
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
            string strCmdText;
            strCmdText = @"/C venv\scripts\activate && py parser.py";


            var process = System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }
    }
}
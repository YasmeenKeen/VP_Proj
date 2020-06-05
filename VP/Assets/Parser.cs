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
            if(compCreated[i]!= null)
            {
                CompIndv obj = compCreated[i].GetComponent<CompIndv>();
                string id = i + "";
                string type = obj.ctype;
                string text = obj.ctext;
                string colour = obj.ccolour;
                string cclass = obj.cclass;
                string style = obj.cstyle;
                string src = obj.cimage;
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
                line += src.Length > 0 ? " " + "src=" + src : "";
                for (int j = 0; j < relCreated.Count ; j++)
                {
                    if(relCreated[j]!= null)
                    {
                        RelIndv objRel = relCreated[j].GetComponent<RelIndv>();
                        string parent = objRel.parent.name;
                        string child = objRel.child.name;
                        int parent_id = int.Parse(parent[parent.Length - 1] + "") - 1;
                        int child_id = int.Parse(child[child.Length - 1] + "") - 1;
                        if (i == child_id)
                        {
                            line += " childof=" + parent_id + "";
                        }
                    }
                }
                //line+= " "+"childof=";
                lines.Add(line);
            }
           
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _SCVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); //create Dialouge list
        TextAsset csvData = Resources.Load<TextAsset>(_SCVFileName);// get file

        string[] data = csvData.text.Split(new char[] {'\n'});

        for(int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue();

            List<string> TextArrayList = new List<string>();

            do
            {
                TextArrayList.Add(row[2]);
                if (++i < data.Length)
                    row = data[i].Split(new char[] { ',' });
                else
                    break;
            } while (row[0].ToString() == "");

            dialogue.TextArray = TextArrayList.ToArray();

            dialogueList.Add(dialogue);
        }

        return dialogueList.ToArray();
    }
}

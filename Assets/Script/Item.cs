using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class Item : MonoBehaviour
{
    public List<string> ItemArray;
    public TextMeshProUGUI FirstColumText;
    public TextMeshProUGUI SecondColumText;
    public TextMeshProUGUI ThirdColumText;
    private int ColumnCount;

    private void Awake()
    {
        ColumnCount = 0;
    }

    void Start()
    {
        ItemArray.Add("����");
        ItemArray.Add("����");
        ItemArray.Add("����");
        ItemArray.Add("�ո�ð�");
        ItemArray.Add("�Ź�");
        ItemArray.Add("�Ȱ�");
        ItemArray.Add("���ġŲ");
        ItemArray.Add("��"); 
        ItemArray.Add("����");
        PrintItem();
    }

    public void PrintItem()
    {
        FirstColumText.text = "";
        SecondColumText.text = "";
        ThirdColumText.text = "";

        foreach (var Item in ItemArray)
        {
            switch (ColumnCount)
            {
                case 0: FirstColumText.text = FirstColumText.text + Item + "\n"; break;
                case 1: SecondColumText.text = SecondColumText.text + Item + "\n"; break;
                case 2: ThirdColumText.text = ThirdColumText.text + Item + "\n"; break;
            }
            ColumnCount = (ColumnCount + 1) % 3;
        }
    }

}

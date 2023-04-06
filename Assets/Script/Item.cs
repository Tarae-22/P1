using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class Item : MonoBehaviour
{
    public string[] ItemArray;
    public TextMeshProUGUI FirstColumText;
    public TextMeshProUGUI SecondColumText;
    public TextMeshProUGUI ThirdColumText;
    private int ItemCount;
    private int ColumnCount;

    private void Awake()
    {
        ItemArray = new string[20];
        ItemCount = 0;
        ColumnCount = 0;
    }


    void Start()
    {
        AddItem("����");
        AddItem("����");
        AddItem("����");
        AddItem("�ո�ð�");
        AddItem("�Ź�");
        AddItem("�Ȱ�");
        AddItem("���ġŲ");
        AddItem("��"); 
        AddItem("����");

    }

    void Update()
    {
        
    }

    public void AddItem(string ItemName)
    {
        switch (ColumnCount)
        {
            case 0: FirstColumText.text = FirstColumText.text + ItemName + "\n"; break;
            case 1: SecondColumText.text = SecondColumText.text + ItemName + "\n"; break;
            case 2: ThirdColumText.text = ThirdColumText.text + ItemName + "\n"; break;
        }

        ColumnCount = (ColumnCount + 1) % 3;
        ItemArray[ItemCount++] = ItemName;
    }

}

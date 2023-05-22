using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    private string[] TextData;
    public TextMeshProUGUI StoryText;
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject ThirdButton;
    public GameObject FourthButton;
    public GameManager GameManager;
    public DataManager GameData;
    public float Typingspeed;
    private string SText;
    private bool IsChoiced;

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
        Typingspeed = 0.05f;
        StartCoroutine(TextEffect());

    }

    IEnumerator TextEffect()
    {      
        var data = GameData.TextData;

        foreach(var Curdata in data)
        {
            IsChoiced = false; 
            yield return Typing(Curdata.DialogList); // ���� ���
            
            yield return new WaitForSeconds(0.5f);
            SetChoiceText(Curdata.LinkedTextID); // ������ ���
            GameManager.SetChoiceButton();

            while(!IsChoiced)// �����ϱ� ������ ���� �ؽ�Ʈ�� �ҷ����� ����
            {
                yield return null;
            }
            SText = "";

        }
    }

    IEnumerator Typing(string text) // Ÿ���� ȿ��
    {
        foreach (var character in text)
        {
            SText += character;
            StoryText.text = SText;
            yield return new WaitForSeconds(Typingspeed);
        }
    }

    public void SetChoiceText(string LinkedTID) //  ���� ��ư�� �ؽ�Ʈ ����
    {

        var ChoiceData = GameData.ChoiceText;
        var Select = ChoiceData.Where(data => data.ChoiceTID == LinkedTID);
        var SelectNum = 1;

        TextMeshProUGUI FirstText = FirstButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI SecondText = SecondButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI ThirdText = ThirdButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI FourthText = FourthButton.GetComponentInChildren<TextMeshProUGUI>();

        // �� ���� �������� LinkedTID �ִ� ��� ������ ��
        foreach (var a in Select)
        {
            if (SelectNum == 1)
            {
                FirstButton.SetActive(true);
                FirstText.text = a.Choicetext;
            }                
            else if (SelectNum == 2)
            {
                SecondButton.SetActive(true);
                SecondText.text = a.Choicetext;
            }              
            else if (SelectNum == 3)
            {
                ThirdButton.SetActive(true);
                ThirdText.text = a.Choicetext;
            }              
            else if (SelectNum == 4)
            {
                FourthButton.SetActive(true);
                FourthText.text = a.Choicetext;
            }
            SelectNum++;
        }
  
    }

    public string ResultText(string LinkedCID) // ��� �ؽ�Ʈ ���
    {
        var Resultdata = GameData.ResultText;
        var Select = Resultdata.Where(data => data.ResultTID == LinkedCID);
        string ReturnData = "";

        foreach(var Result in Select)
        {
            ReturnData = Result.Result + "\n";
        }
        return ReturnData;
    }

    public void SelectChoice() // ���� ��ư �Լ�
    {

    }
}

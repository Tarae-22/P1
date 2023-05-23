using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Image ChoicePanel;
    public TextMeshProUGUI StoryText;
    public ScrollRect scrollRect;
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject ThirdButton;
    public GameObject FourthButton;
    public DataManager GameData;
    public float Typingspeed;
    private string SText;
    private string resultText;
    private bool IsChoiced;
    private IEnumerable<ChoiceText> Select;
    


    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
        Typingspeed = 0.05f;
        StartCoroutine(TextEffect());
    }

    IEnumerator TextEffect()
    {      
        var data = GameData.TextData;

        foreach(var Curdata in data) //��ħ�� �� ����Ŭ
        {
            IsChoiced = false; 
            yield return Typing(Curdata.DialogList); // ���� ���
            
            yield return new WaitForSeconds(0.5f);
            SetChoiceText(Curdata.LinkedTextID); // ������ ���
            ChoicePanel.gameObject.SetActive(true);

            while (!IsChoiced)// �����ϱ� ������ ���� �ؽ�Ʈ�� �ҷ����� ����
            {
                yield return null;
            }
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return Typing(resultText); // ��� ���� ���
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
        Select = ChoiceData.Where(data => data.ChoiceTID == LinkedTID);
        var SelectNum = 1;

        TextMeshProUGUI FirstText = FirstButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI SecondText = SecondButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI ThirdText = ThirdButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI FourthText = FourthButton.GetComponentInChildren<TextMeshProUGUI>();
      
        foreach (var a in Select) // �� ���� �������� LinkedTID �ִ� ��� ������ ��
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

    public void ResultText(string LinkedCID) // ��� �ؽ�Ʈ ���
    {
        var Resultdata = GameData.ResultText;
        var Select = Resultdata.Where(data => data.ResultTID == LinkedCID);
        string ReturnData = "";

        foreach(var Result in Select)
        {
            ReturnData = Result.Result + "\n\n";
        }
        IsChoiced = true;
        resultText = ReturnData;
    }

    public void SelectChoice(int ButtonSelect) // ���� ��ư �Լ�
    {
        var ResultNum = 1;
        foreach(var a in Select)
        {
            if ( ResultNum == ButtonSelect)
                ResultText(a.LinkedChoiceID);
            ResultNum++;
        }
        ButtonOff();
    }

    public void ButtonOff() // ��ư �ʱ�ȭ
    {
        ChoicePanel.gameObject.SetActive(false);

        FirstButton.SetActive(false);
        SecondButton.SetActive(false);
        ThirdButton.SetActive(false);
        FourthButton.SetActive(false);
    }
}

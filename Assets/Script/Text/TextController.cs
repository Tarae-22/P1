using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public TextMeshProUGUI StoryText;
    public ScrollRect scrollRect;
    public ChoiceButtonUI ChoiceUI;
    public ConditionChecker ConCheck;
    private DataManager GameData;
    [HideInInspector]
    public float Typingspeed;
    private string SText;
    private string resultText;
    private bool IsChoiced;

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
        ConCheck = gameObject.GetComponent<ConditionChecker>();
        Typingspeed = 0.05f;
        
    }
    public void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {      
        var data = GameData.StoryText;

        foreach(var Curdata in data) //��ħ�� �� ����Ŭ
        {
            ConCheck.ConditionCheck();
            IsChoiced = false; 
            yield return TypingEffect(Curdata.DialogList); // ���� ���
            
            yield return new WaitForSeconds(0.5f);
            ChoiceUI.SetChoiceText(); // ������ ���
            ChoiceUI.SetButton();

            while (!IsChoiced)// �����ϱ� ������ ���� �ؽ�Ʈ�� �ҷ����� ����
            {
                yield return null;
            }
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // ��� ���� ���
        }
    }

    IEnumerator TypingEffect(string text) // Ÿ���� ȿ��
    {
        foreach (var character in text)
        {
            SText += character;
            StoryText.text = SText;
            yield return new WaitForSeconds(Typingspeed);
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
  
}

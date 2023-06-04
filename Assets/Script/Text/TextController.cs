using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.PlayerLoop.PreUpdate;

public class TextController : MonoBehaviour
{
    public TextMeshProUGUI StoryText;
    public ScrollRect scrollRect;
    public ChoiceButtonUI ChoiceUI;
    public ConditionChecker ConCheck;
    public DataManager GameData;
    public MapController MapController;
    public GameStat GameStat;
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
        StartCoroutine(GameStart());
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
        if (LinkedCID == null)
        {
            IsChoiced = true;
            return;
        }
            
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
    
    IEnumerator GameStart()
    {
        yield return Morning();
        yield return Lunch();
        yield return evening();
    }

    IEnumerator Morning()
    {
        var Morningdata = GameData.TextCondition.Join(GameData.StoryText, tc=>tc.TextID, st=>st.ConID, (tc, st)=> new { TextCondition=tc, StoryText=st  })
                          .Where(x=> x.TextCondition.Time == "0"); //��ħ ������ ��������

        foreach (var Curdata in Morningdata) //��ħ�� �� ����Ŭ
        {
            IsChoiced = false;
            yield return TypingEffect(Curdata.StoryText.DialogList); // ���� ���

            yield return new WaitForSeconds(0.5f);
            ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // ������ ���
            ChoiceUI.SetButton();

            while (!IsChoiced)// �����ϱ� ������ ���� �ؽ�Ʈ�� �ҷ����� ����
            {
                yield return null;
            }
            int i = 0;
            MapController.MapUpdate(MapController.MorningMapName[i++]); //���� �÷��̾� ��ġ ����
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // ��� ���� ���
        }
    }

    IEnumerator Lunch()
    {
        GameStat.CurTime = "1"; // �������� ����
        MapController.MapUpdate("����");

        var Lunchdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                        .Where(x => x.TextCondition.Time == "1" /*&& �����ؽ�Ʈ*/); //���� �����ؽ�Ʈ ��������

        foreach (var Curdata in Lunchdata) //���� �ʹݺκ� ����Ŭ
        {
            IsChoiced = false;
            yield return TypingEffect(Curdata.StoryText.DialogList); // ���� ���

            yield return new WaitForSeconds(0.5f);
            ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // ������ ���
            ChoiceUI.SetButton();

            while (!IsChoiced)// �����ϱ� ������ ���� �ؽ�Ʈ�� �ҷ����� ����
            {
                yield return null;
            }
            int i = 0;
            MapController.MapUpdate(MapController.LunchMapName[i++]); //���� �÷��̾� ��ġ ����
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // ��� ���� ���
        }


        for (int i = 0; i < 5; i++) // ���� �߹ݺκ� ����Ŭ
        {
            var MapData = GameData.TextCondition.Where(x => x.Position == GameStat.CurPos && x.Time == GameStat.CurPos /*&& x.Map == "1"*/);

            MapController.SetMap("���̸�"); //�� UI Ȱ��ȭ

        }

        //foreach() ���� �Ĺݺκ� ����Ŭ
    }

    IEnumerator evening()
    {
        GameStat.CurTime = "2"; // �������� ����

        var Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "2"); // ���� �����ؽ�Ʈ ��������

        foreach (var Curdata in Dinnerdata) // ���� ����Ŭ
        {

        }

        var EndGame = "�÷��� Ÿ�� : " + GameStat.GetPlayTime();
        yield return TypingEffect(EndGame);
    }
}

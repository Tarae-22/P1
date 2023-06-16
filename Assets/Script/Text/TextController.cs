using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public DataManager GameData;
    public MapController MapController;
    public GameStat GameStat;
    public BGMManager BGMManager;
    public UIManager UIManager;
    [HideInInspector]
    public float Typingspeed;
    private string SText;
    private string resultText;
    private bool IsChoiced;
    public bool HaveClue;
    public bool IsEnd;
    public bool HaveSight;
    public bool HaveConfidence;
    public bool HaveDetection;
    public bool HaveSense;
    public bool HaveDog;
    public string PreviousResult;

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
        Typingspeed = 0.000001f;
        HaveClue = true;


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
    
    IEnumerator GameStart() //���� �Լ�
    {
        //BGMManager.BGMSource.Stop();
        //yield return StartFade();
        //yield return Morning();
        //ClearText();
        //BGMManager.BGMSource.Stop();
        //yield return LunchBeginning();
        //yield return LunchMiddle();
        //ClearText();
        BGMManager.BGMSource.Stop();
        yield return Evening();
        yield return EveningMiddle();
        yield return Ending();
        ClearText();   
        yield return UIManager.SetEndingCredit();
        UIManager.EndingResult();
    }

    IEnumerator StartFade()
    {
        UIManager.textComponent.gameObject.SetActive(true);
        yield return UIManager.Fade();
        yield return new WaitForSeconds(1f);
        UIManager.textComponent.gameObject.SetActive(false);
    }

    IEnumerator Morning()
    {
        BGMManager.StartMorningBGM(); // ��ħ BGM ����

        var Morningdata = GameData.TextCondition.Join(GameData.StoryText, tc=>tc.TextID, st=>st.ConID, (tc, st)=> new { TextCondition=tc, StoryText=st  })
                          .Where(x=> x.TextCondition.Time == "0"); //��ħ ������ ��������
        foreach (var Curdata in Morningdata) //��ħ�� �� ����Ŭ
        {
            IsChoiced = false;
            GameStat.IsMapChoiced = false;

            yield return TypingEffect(Curdata.StoryText.DialogList); // ���� ���

            yield return new WaitForSeconds(0.5f);
            Debug.Log(Curdata.StoryText.LinkedChoiceID);
            ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // ������ ���
            ChoiceUI.SetButton();

            yield return WaitChoiceSelect();
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // ��� ���� ���
            yield return new WaitForSeconds(1f);
            if (Curdata.TextCondition.Map == "1")
            {
                MapController.SetMap(Curdata.TextCondition.MapPosition);
                yield return WaitMapSelect();
            }           
        }
    }

    IEnumerator LunchBeginning()
    {
        BGMManager.StartLunchBGM(); // ���� BGM ����


        GameStat.CurTime = "1"; // �������� ����
        MapController.MapUpdate("����");

        UIManager.ChangeBackground(); //�̹��� ����

        yield return StartFade();

        var Lunchdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                        .Where(x => x.TextCondition.Time == "1"); //���� �����ؽ�Ʈ ��������

        foreach (var Curdata in Lunchdata) //���� �ʹݺκ� ����Ŭ
        {
            IsChoiced = false;
            
            yield return TypingEffect(Curdata.StoryText.DialogList); // ���� ���

            yield return new WaitForSeconds(0.5f);
            ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // ������ ���
            ChoiceUI.SetButton();

            yield return WaitChoiceSelect();

            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // ��� ���� ���
            yield return new WaitForSeconds(1f);
            if (Curdata.TextCondition.Map == "1")
            {
                GameStat.IsMapChoiced = false;

                if (Curdata.TextCondition.MapPosition == "0")
                {
                    MapController.SetButton();
                    yield return WaitMapSelect();
                }
                else
                {
                    MapController.SetMap(Curdata.TextCondition.MapPosition);
                    yield return WaitMapSelect();
                }                
            }          
        }
    }

    IEnumerator LunchMiddle()
    {

        GameStat.CurTime = "2";

        for (int i = 0; i < 5; i++) // ���� �߹ݺκ� ����Ŭ
        {
            GameStat.CheckNPC(i);

            if (HaveClue == true && GameStat.CurPos == "����")
                yield break;

            var MapData = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                        .Where(x => x.TextCondition.Time == "2" && x.TextCondition.Position == GameStat.CurPos);

            var Curdata = MapData.First();
                         
            IsChoiced = false;
               
            GameStat.IsMapChoiced = false;

            yield return TypingEffect(Curdata.StoryText.DialogList); // ���� ���
               
            yield return new WaitForSeconds(0.5f);
                 
            if (Curdata.TextCondition.Map == "0")
            {
                yield return ClueCylce(i);
            }            
            else    
            {
                SText = "";              
                scrollRect.verticalNormalizedPosition = 1f;              
                MapController.SetButton();               
                yield return WaitMapSelect();           
            }
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator ClueCylce(int turn)
    {
        GameStat.CurTime = "3";

        var ClueData = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                        .Where(x => x.TextCondition.Time == "3" && x.TextCondition.Position == GameStat.CurPos && x.TextCondition.NPCType == GameStat.CheckNPC(turn));

        foreach (var b in ClueData)
        {
            IsChoiced = false;
            GameStat.IsMapChoiced = false;

            yield return TypingEffect(b.StoryText.DialogList); // ���� ���

            yield return new WaitForSeconds(0.5f);
            ChoiceUI.SetChoiceText(b.StoryText.LinkedChoiceID); // ������ ���
            ChoiceUI.SetButton();

            yield return WaitChoiceSelect();
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // ��� ���� ���
            yield return new WaitForSeconds(1f);
            if (HaveClue == true)
            {
                string clueText = "<b>�ܼ�</b>�� �����.\n\n";
                SText = "";
                scrollRect.verticalNormalizedPosition = 1f;
                yield return TypingEffect(clueText);
                
                yield return new WaitForSeconds(2f);
            }
                
            MapController.SetButton();
            yield return WaitMapSelect();
        }
    }

    IEnumerator Evening()
    {
        BGMManager.StartEveningBGM(); // ���� BGM ����

        GameStat.CurTime = "4";
        MapController.MapUpdate("����");

        UIManager.ChangeBackground(); //�̹��� ����
        yield return StartFade();

        var LunchLatedata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "4"); //���� �ʹ� ������ ��������

        foreach (var Curdata in LunchLatedata)
        {
            IsChoiced = false;
            GameStat.IsMapChoiced = false;

            yield return TypingEffect(Curdata.StoryText.DialogList); // ���� ���

            yield return new WaitForSeconds(0.5f);
            if (Curdata.StoryText.ConID == "Con_50" && HaveClue == false)
            {
                ChoiceUI.SetChoiceText("Cho_46");
                ChoiceUI.SetButton();
                yield return WaitChoiceSelect();
                SText = "";
                scrollRect.verticalNormalizedPosition = 1f;
                yield return TypingEffect(resultText);
                yield return new WaitForSeconds(3f);
                yield break;
            }
            else
            {
                ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // ������ ���
                ChoiceUI.SetButton();
            }
            

            yield return WaitChoiceSelect();
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // ��� ���� ���
            yield return new WaitForSeconds(1f);
            if (Curdata.TextCondition.Map == "1")
            {             
                MapController.SetMap(Curdata.TextCondition.MapPosition);
                yield return WaitMapSelect();
            }

        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator EveningMiddle()
    {
        SText = "";
        int ConIDnum= 54;
        if (HaveClue == false || IsEnd == true)
            yield break;

        GameStat.CurTime = "5";
        

        while(true)
        {
            Debug.Log(PreviousResult);

            var Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == ConIDnum); // ���� �ؽ�Ʈ ��������

            if (PreviousResult == "Res_45")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID >= 54);
            }
            else if (PreviousResult == "Res_46" || PreviousResult == "Res_47" || PreviousResult == "Res_48" || PreviousResult == "Res_49" || PreviousResult == "Res_50" || PreviousResult == "Res_51")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 58);
            }
            else if (PreviousResult == "Res_52" || PreviousResult == "Res_54")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 63);
            }
            else if (PreviousResult == "Res_53" || PreviousResult == "Res_66")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 59);
            }
            else if (PreviousResult == "Res_56")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 61);
            }
            else if (PreviousResult == "Res_59")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 84);
            }
            else if (PreviousResult == "Res_64")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 87);
            }
            else if (PreviousResult == "Res_65")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 85);
            }
            else if (PreviousResult == "Res_67")
            {
                break;
            }
            else if (PreviousResult == "Res_55" || PreviousResult == "Res_58")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 68);
            }
            else if (PreviousResult == "Res_63" || PreviousResult == "Res_68" || PreviousResult == "Res_69" || PreviousResult == "Res_70")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 55);
            }


            foreach (var Curdata in Dinnerdata) // ���� ����Ŭ
            {
                Debug.Log(Curdata.TextCondition.ConID);
                ConIDnum = Curdata.TextCondition.ConID;
                IsChoiced = false;

                yield return TypingEffect(Curdata.StoryText.DialogList);

                if (Curdata.StoryText.ConID == "Con_58" && HaveSense == true)
                {
                    ChoiceUI.SetChoiceText("Cho_59,Cho_61");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_58" && HaveSense == false)
                {
                    ChoiceUI.SetChoiceText("Cho_59,Cho_60");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_59" && HaveDetection == false)
                {
                    ChoiceUI.SetChoiceText("Cho_62,Cho_63");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_55" && HaveDog == false)
                {
                    ChoiceUI.SetChoiceText("Cho_53,Cho_54");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_56" && HaveDog == false)
                {
                    ChoiceUI.SetChoiceText("Cho_55,Cho_56");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_57" && HaveDog == false)
                {
                    ChoiceUI.SetChoiceText("Cho_57,Cho_58");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_63" && HaveDetection == false)
                {
                    ChoiceUI.SetChoiceText("Cho_62,Cho_69");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_68" && HaveSight == false)
                {
                    ChoiceUI.SetChoiceText("Cho_76,Cho_77");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_84" && HaveDog == false)
                {
                    ChoiceUI.SetChoiceText("Cho_81,Cho_82");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else
                {
                    ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // ������ ���
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                yield return new WaitForSeconds(1f);
                ConIDnum++;
                break;
            }
        }

    }

    IEnumerator Ending()
    {
        SText = "";

        if (HaveClue == true)
        {
            var EndingData = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "6"); // ���� �ؽ�Ʈ ��������

            foreach (var CurData in EndingData)
            {

                IsChoiced = false;
                yield return TypingEffect(CurData.StoryText.DialogList); // ���� ���

                yield return new WaitForSeconds(0.5f);

                ChoiceUI.SetChoiceText(CurData.StoryText.LinkedChoiceID); // ������ ���
                ChoiceUI.SetButton();
                yield return WaitChoiceSelect();
                SText = "";
            }
        }
        else
        {
            var EndingData = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "6" && x.TextCondition.ConID >= 90); // ���� �ؽ�Ʈ ��������

            foreach (var CurData in EndingData)
            {

                IsChoiced = false;
                yield return TypingEffect(CurData.StoryText.DialogList); // ���� ���

                yield return new WaitForSeconds(0.5f);

                ChoiceUI.SetChoiceText(CurData.StoryText.LinkedChoiceID); // ������ ���
                ChoiceUI.SetButton();
                yield return WaitChoiceSelect();
                SText = "";
            }
        }
        yield return new WaitForSeconds(2f);
    }
    IEnumerator WaitMapSelect()
    {
        while (!GameStat.IsMapChoiced)// ���� �����ϱ� ������ ���� �ؽ�Ʈ�� �ҷ����� ����
        {
            yield return null;
        }
    }

    IEnumerator WaitChoiceSelect()
    {
        while (!IsChoiced)// �����ϱ� ������ ���� �ؽ�Ʈ�� �ҷ����� ����
        {
            yield return null;
        }
    }

    public void ClearText()
    {
        StoryText.text = "";
    }

}

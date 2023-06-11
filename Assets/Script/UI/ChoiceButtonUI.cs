using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ChoiceButtonUI : MonoBehaviour
{
    public Image ChoicePanel;
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject ThirdButton;
    public GameObject FourthButton;
    public DataManager DataMGR;
    public TextController TC;
    private IEnumerable<string> LCIDData;

    private void Awake()
    {
        DataMGR = GameObject.Find("TextData").GetComponent<DataManager>();
    }

    public void SetChoiceText(string LCID) //  ���� ��ư�� �ؽ�Ʈ ����
    {
        TextMeshProUGUI FirstText = FirstButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI SecondText = SecondButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI ThirdText = ThirdButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI FourthText = FourthButton.GetComponentInChildren<TextMeshProUGUI>();

        LCIDData = LCID.Split(',');

        var SelectNum = 1;

        foreach (var a in LCIDData) // �� ���� �������� LinkedTID �ִ� ��� ������ ��
        {
            if (SelectNum == 1)
            {
                FirstButton.SetActive(true);
                Debug.Log(a);
                FirstText.text = DataMGR.ChoiceText.Where(x=>x.ChoiceTID == a).First().Choicetext;
            }
            else if (SelectNum == 2)
            {
                SecondButton.SetActive(true);
                SecondText.text = DataMGR.ChoiceText.Where(x => x.ChoiceTID == a).First().Choicetext;
            }
            else if (SelectNum == 3)
            {
                ThirdButton.SetActive(true);
                ThirdText.text = DataMGR.ChoiceText.Where(x => x.ChoiceTID == a).First().Choicetext;
            }
            else if (SelectNum == 4)
            {
                FourthButton.SetActive(true);
                FourthText.text = DataMGR.ChoiceText.Where(x => x.ChoiceTID == a).First().Choicetext;
            }
            SelectNum++;
        }
    }

    public void SelectChoice(int ButtonSelect) // ���� ��ư �Լ�
    {
        var ResultNum = 1;

        foreach (var a in LCIDData)
        {
            if (ResultNum == ButtonSelect)
            TC.ResultText(DataMGR.ChoiceText.Where(x => x.ChoiceTID == a).First().LinkedResultID);
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

    public void SetButton() // ��ư ����
    {
        ChoicePanel.gameObject.SetActive(true);
    }
}

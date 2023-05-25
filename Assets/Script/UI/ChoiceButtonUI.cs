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
    private ConditionChecker GameData;
    private TextController TC;
    private IEnumerable<ChoiceText> Select;

    private void Awake()
    {
        GameData = gameObject.GetComponent<ConditionChecker>();
    }

    public void SetChoiceText() //  ���� ��ư�� �ؽ�Ʈ ����
    {
        var SelectNum = 1;
        var LinkedTID = GameData.ChoiceData.FirstOrDefault().ChoiceTID.Split(',');
        Select = GameData.ChoiceData.Where(data => LinkedTID.Contains(data.ChoiceTID));

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

    public void SelectChoice(int ButtonSelect) // ���� ��ư �Լ�
    {
        var ResultNum = 1;

        foreach (var a in Select)
        {
            if (ResultNum == ButtonSelect)
                TC.ResultText(a.LinkedChoiceID);
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

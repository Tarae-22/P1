using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapController : MonoBehaviour
{
    public Character Character;
    public TextMeshProUGUI FirstMap;
    public TextMeshProUGUI SecondMap;
    public TextMeshProUGUI ThirdMap;
    public string[] MapName;
    public GameObject MapButton;
    public Image TimerBar;


    private void Awake()
    {
        MapName = new string[11] {"��ǥ��", "���ǰ��", "���", "����", "����", "ȸ����", "������", "�ͽ�Ʈ�� ��Ʈ����", "�ѷ��ڽ���", "�ͽ��� ��", "����ŷ"};
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(TimerBar.fillAmount == 0)
        {
            SetButton();
        }
    }

    //button click event
    public void SelectMap(int NextMap)
    {
        StartCoroutine(ChangeMap(NextMap));
    }


    IEnumerator ChangeMap(int NextMap)
    {
        yield return new WaitForSeconds(5f);
        if (NextMap == 1) //front
        {
            Character.CurrentMapIndex++;
        }
        else if (NextMap == 2) //back
        {
            Character.CurrentMapIndex--;
        }
        else if(NextMap ==3 )
        {
            //third map(���� ��ġ�� ����, ������ ��)
        }
        else
        {
            //Fourth map(���� ��ġ�� �ͽ�Ʈ�� ��Ʈ������ ��)
        }
    }

    public void SetButton()
    {
        if(Character.CurrentMapIndex == 1) // �ǳ�(������, ȸ���� ��)�� �� ��ư �ϳ��� or
        {
            //��ư �ϳ��� or xǥ��
            FirstMap.text = MapName[Character.CurrentMapIndex];
            SecondMap.text = "X";
            ThirdMap.text = "X";
        }
        else if(Character.CurrentMapIndex == 3)
        {
            FirstMap.text = MapName[Character.CurrentMapIndex + 1];
            SecondMap.text = MapName[Character.CurrentMapIndex + 4];
            ThirdMap.text = MapName[Character.CurrentMapIndex - 1];
        }
        else if(Character.CurrentMapIndex == 4)
        {
            FirstMap.text = MapName[Character.CurrentMapIndex];
            SecondMap.text = MapName[Character.CurrentMapIndex + 3];
            ThirdMap.text = MapName[Character.CurrentMapIndex - 2];
        }
        else if(Character.CurrentMapIndex == 5 )
        {
            FirstMap.text = MapName[Character.CurrentMapIndex];
            SecondMap.text = MapName[Character.CurrentMapIndex + 1];
            ThirdMap.text = MapName[Character.CurrentMapIndex - 2];
        }
        else if(Character.CurrentMapIndex == 6)
        {
            FirstMap.text = MapName[Character.CurrentMapIndex - 3];
            SecondMap.text = "X";
            ThirdMap.text = "X";
        }
        else if(Character.CurrentMapIndex == 8)
        {
            FirstMap.text = MapName[Character.CurrentMapIndex];
            SecondMap.text = MapName[Character.CurrentMapIndex + 1];
            ThirdMap.text = MapName[Character.CurrentMapIndex + 2];
        }
        else
        {
            FirstMap.text = MapName[Character.CurrentMapIndex];
            SecondMap.text = MapName[Character.CurrentMapIndex - 2];
            ThirdMap.text = "X";
        }

        SetButtonPosition();
        MapButton.SetActive(true);

    }

    public void SetButtonPosition()
    {
        //one button

        //two buttons

        //three buttons

        //four buttons
    }
}

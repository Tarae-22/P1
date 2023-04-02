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
        MapName = new string[6] {"��ǥ��", "���ǰ��", "���", "����", "����", "ȸ����"};
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
        else //third map(���� �ͽ�Ʈ����Ʈ�����϶� ����)
        {
            
        }
    }

    public void SetButton()
    {
        if(Character.CurrentMapIndex == 1) // �ǳ�(������, ȸ���� ��)�� �� ��ư �ϳ��� or
        {
            //��ư �ϳ��� 
        }
        
        FirstMap.text = MapName[Character.CurrentMapIndex];
        SecondMap.text = MapName[Character.CurrentMapIndex - 2];
        MapButton.SetActive(true);

    }
}

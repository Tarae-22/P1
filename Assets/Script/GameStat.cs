using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStat : MonoBehaviour
{
    public string CurTime;
    public float PlayTime;
    public string CurPos;
    public int GameTurn;
    public bool isNPC;
    public NPCMove NPCPosition;

    private void Awake()
    {
        GameTurn = 1;
        CurTime = "0";
        CurPos = "��ǥ��";
        isNPC = false;
        NPCPosition = new NPCMove();
        PlayTime = 0;
    }

    private void Update()
    {
        PlayTime += Time.deltaTime;
    }

    public void TurnIncrease()
    {
        //���� �ð� �ݿ�
        GameTurn++;
    }

    //NPC ��ġ Ȯ�� �� �ش� NPC �̸� ��ȯ
    public string CheckNPC(int Turn)
    {
        if (CurPos == NPCPosition.DollPos[Turn])
        {
            isNPC = true;  return "����Ż �˹�";
        }
            
        else if (CurPos == NPCPosition.RangerPos[Turn])
        {
            isNPC = true;  return "������";
        }
            
        else if (CurPos == NPCPosition.DogPos[Turn])
        {
            isNPC = true;  return "������";
        }
        else
        {
            isNPC = false;  return "";
        }
    }

    public string GetPlayTime()
    {
        int Min = Convert.ToInt32(PlayTime / 60);
        int Sec = Convert.ToInt32(PlayTime % 60);

        return Min + "�� " + Sec + "��";
    }
}

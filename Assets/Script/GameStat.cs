using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStat : MonoBehaviour
{
    public string CurTime;
    public string CurPos;
    public int GameTurn;

    private void Awake()
    {
        GameTurn = 1;
        CurTime = "0";
        CurPos = "��ǥ��";
    }

    public void TurnIncrease()
    {
        //���� �ð� �ݿ�
        GameTurn++;
    }
}

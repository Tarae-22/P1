using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string CurTime;
    public string CurPos;
    public int GameTurn;

    private void Awake()
    {
        GameTurn = 1;
    }

    public void TurnIncrease()
    {
        //���� �ð� �ݿ�
        GameTurn++;
    }
}

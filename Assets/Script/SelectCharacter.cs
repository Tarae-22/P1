using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public CharacterData characterData;

    //initialize stat
    public void SetCharacter(int Select)
    {
        if(Select == 1)
        {
            characterData.Name = "���� ����";
            characterData.CurrentMapIndex = 3;
            characterData.HP = 100;
            characterData.SAN = 80;
            characterData.STR = 15;
            characterData.END = 3;
            characterData.CON = 53;
            characterData.DEX = 12;
            characterData.INT = 24;
            characterData.EDU = 21;
            characterData.INS = 7;
            characterData.CHA = 10;
            characterData.PROB = 30;
        }
        else if (Select == 2)
        {
            characterData.Name = "������";
            characterData.CurrentMapIndex = 3;
            characterData.HP = 100;
            characterData.SAN = 80;
            characterData.STR = 15;
            characterData.END = 3;
            characterData.CON = 53;
            characterData.DEX = 12;
            characterData.INT = 24;
            characterData.EDU = 21;
            characterData.INS = 7;
            characterData.CHA = 10;
            characterData.PROB = 30;
        }
        else if (Select == 3)
        {
            characterData.Name = "���� �˹�";
            characterData.CurrentMapIndex = 3;
            characterData.HP = 100;
            characterData.SAN = 80;
            characterData.STR = 15;
            characterData.END = 3;
            characterData.CON = 53;
            characterData.DEX = 12;
            characterData.INT = 24;
            characterData.EDU = 21;
            characterData.INS = 7;
            characterData.CHA = 10;
            characterData.PROB = 30;
        }
        else if (Select == 4)
        {
            characterData.Name = "������";
            characterData.CurrentMapIndex = 3;
            characterData.HP = 100;
            characterData.SAN = 80;
            characterData.STR = 15;
            characterData.END = 3;
            characterData.CON = 53;
            characterData.DEX = 12;
            characterData.INT = 24;
            characterData.EDU = 21;
            characterData.INS = 7;
            characterData.CHA = 10;
            characterData.PROB = 30;
        }
    }

    
}

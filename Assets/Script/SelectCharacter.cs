using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class SelectCharacter : CharacterData
{
    //initialize stat
    public void SetCharacter(int Select)
    {
        if(Select == 1)
        {
            Name = "������ ����";
            CurrentMapIndex = 3;
            HP = 100;
            SAN = 80;
            STR = 15;
            END = 3;
            CON = 53;
            DEX = 12;
            INT = 24;
            EDU = 21;
            INS = 7;
            CHA = 10;
            PROB = 30;
        }
        else if (Select == 2)
        {
            Name = "������";
            CurrentMapIndex = 3;
            HP = 100;
            SAN = 80;
            STR = 15;
            END = 3;
            CON = 53;
            DEX = 12;
            INT = 24;
            EDU = 21;
            INS = 7;
            CHA = 10;
            PROB = 30;
        }
        else if (Select == 3)
        {
            Name = "���� �˹�";
            CurrentMapIndex = 3;
            HP = 100;
            SAN = 80;
            STR = 15;
            END = 3;
            CON = 53;
            DEX = 12;
            INT = 24;
            EDU = 21;
            INS = 7;
            CHA = 10;
            PROB = 30;
        }
        else if (Select == 4)
        {
            Name = "������";
            CurrentMapIndex = 3;
            HP = 100;
            SAN = 80;
            STR = 15;
            END = 3;
            CON = 53;
            DEX = 12;
            INT = 24;
            EDU = 21;
            INS = 7;
            CHA = 10;
            PROB = 30;
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCMove
{
    public List<string> DollPos { get; set; }
    public List<string> RangerPos { get; set; }
    public List<string> DogPos { get; set; }

    public NPCMove()
    {
        DogPos = new List<string>() { "����", "����", "ȸ����", "����", "����"}; // ������ ��ġ ����
        SetMoving(); // ������ ��ġ ����
    }

    public void SetMoving()
    {
        DollPos = new List<string>() { "����" };
        RangerPos = new List<string>() { "����" };

        int a = UnityEngine.Random.Range(0, 2);

        if (a == 0)
            Setting1();
        else
            Setting2();
    }

    public void Setting1()
    {
        DollPos.Add("����"); //����Ż �˹� ��ġ ����
        DollPos.Add("������");
        DollPos.Add("����");
        DollPos.Add("����");

        RangerPos.Add("�ͽ�Ʈ�� ��Ʈ����"); // ������ ��ġ ����
        int b = UnityEngine.Random.Range(0, 2);

        if (b == 1) // ����ŷ or �ѷ��ڽ���
            RangerPos.Add("����ŷ");
        else
            RangerPos.Add("�ѷ��ڽ���");

        RangerPos.Add("�ͽ�Ʈ�� ��Ʈ����");
        RangerPos.Add("����");
    }

    public void Setting2()
    {
        RangerPos.Add("����"); // ������ ��ġ ����
        RangerPos.Add("������");
        RangerPos.Add("����");
        RangerPos.Add("����");

        DollPos.Add("�ͽ�Ʈ�� ��Ʈ����"); // ����Ż �˹� ��ġ ����
        int b = UnityEngine.Random.Range(0, 2);

        if (b == 1) // ����ŷ or �ѷ��ڽ���
            DollPos.Add("����ŷ");
        else
            DollPos.Add("�ѷ��ڽ���");

        DollPos.Add("�ͽ�Ʈ�� ��Ʈ����");
        DollPos.Add("����");
    }
}

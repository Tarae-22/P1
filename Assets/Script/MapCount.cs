using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCount : MonoBehaviour
{
    public GameObject Player;
    public int[] MapInCount;

    private void Awake()
    {
        MapInCount = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void IncreaseMapCount(string MapName)
    {
        switch (MapName)
        {
            case "��ǥ��": MapInCount[0]++; break;
            case "���ǰ��": MapInCount[1]++; break;
            case "���": MapInCount[2]++; break;
            case "����": MapInCount[3]++; break;
            case "����": MapInCount[4]++; break;
            case "ȸ����": MapInCount[5]++; break;
            case "������": MapInCount[6]++; break;
            case "�ͽ�Ʈ�� ��Ʈ����": MapInCount[7]++; break;
            case "�ѷ��ڽ���": MapInCount[8]++; break;
            case "�ͽ�����": MapInCount[9]++; break;
            case "����ŷ": MapInCount[10]++; break;
        }
    }
}

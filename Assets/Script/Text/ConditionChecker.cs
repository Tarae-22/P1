using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    public DataManager GameData;
    public GameManager GMG;
    public IEnumerable<TextCondition> ConditionData;
    public IEnumerable<StoryText> StoryData;
    public IEnumerable<ChoiceText> ChoiceData;

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
        GMG = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ConditionCheck()
    {
        ConditionData = GameData.TextCondition.Where(Con => Con.Time == GMG.CurTime && Con.Location == GMG.CurPos); //������ Condition �� ��������
        StoryData = GameData.StoryText.Where(TID => TID.ConID == ConditionData.FirstOrDefault().TID); //������ Story �� ��������
        ChoiceData = GameData.ChoiceText.Where(CID => CID.ChoiceTID == StoryData.FirstOrDefault().LinkedTextID); //������ Choice �� ��������

    }
}

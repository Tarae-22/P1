using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    [HideInInspector]
    public DataManager GameData;
    public GameStat GMG;
    public IEnumerable<TextCondition> ConditionData;
    public IEnumerable<StoryText> StoryData;
    public IEnumerable<ChoiceText> ChoiceData;
    public IEnumerable<ChoiceText> Select;

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
    }

    public void ConditionCheck()
    {
        ConditionData = GameData.TextCondition.Where(Con => Con.Time == GMG.CurTime && Con.Position == GMG.CurPos); //������ Condition �� ��������
        StoryData = GameData.StoryText.Where(TID => TID.ConID == ConditionData.First().TextID); //������ Story �� ��������
        ChoiceData = GameData.ChoiceText.Where(CID => CID.ChoiceTID == StoryData.First().LinkedChoiceID); //������ Choice �� ��������
        var LinkedTID = ChoiceData.SelectMany(LID => LID.LinkedResultID.Split(','));
        Select = ChoiceData.Where(data => LinkedTID.Contains(data.LinkedResultID)); //LinkedResultID ��������

    }
}

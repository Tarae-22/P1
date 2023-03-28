using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public Text TextDisplay;
    public float TypingSpeed = 0.15f;
    [Tooltip("���")]
    public string[] TextArray;

    void Start()
    {
        TextDisplay.text = "aaa";
    }


    void Update()
    {

    }


}
[System.Serializable]

public class DialogueEvent
{
    public string name;

    public Vector2 line;
    public Dialogue[] dialogues;
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text Text;
    public AddScore AddScore;
    private void Awake()
    {
        Text= GetComponent<Text>();
        AddScore=GetComponent<AddScore>();
    }
    private void Update()
    {
        Text.text = ""+ AddScore.score;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI機能を扱うときに追記する
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTMP;
    [SerializeField] private  GameObject MainGameManager;
    private int score;

    // Update is called once per frame
    void Update()
    {
        score = MainGameManager.GetComponent<MainGameManager>().GetScore();
        scoreTMP.text = $"{score}本"; //ScoreTextの文字をScore:Scoreの値にする
    }
}
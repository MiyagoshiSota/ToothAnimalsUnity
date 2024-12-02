using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] int timeLimit; // ゲーム時間の制限
    [SerializeField] private TMP_Text textMeshPro;
    [SerializeField] private GameObject MainGameManager;
    [SerializeField] private GameObject BGM;
    [SerializeField] private GameObject QuestImg;
    float time;
    private bool gameStarted = false; // ゲームが始まったかどうかを追跡
    private bool gameEnded = false;

    void Start()
    {
        // スタートカウントダウンを開始
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        if (gameStarted && !gameEnded)
        {
            // フレーム毎の経過時間をtime変数に追加
            time += Time.deltaTime;

            // time変数をint型にし制限時間から引いた数をint型のremaining変数に代入
            int remaining = timeLimit - (int)time;

            // timerTextを更新していく
            if (0 <= remaining)
            {
                textMeshPro.text = remaining.ToString();
            }

            if(0 == remaining){
                gameEnded = true;
                MainGameManager.GetComponent<MainGameManager>().GameEnd();
            }
        }
    }

    IEnumerator StartCountdown()
    {
        // スタートカウントダウン: 3, 2, 1
        for (int i = 3; i > 0; i--)
        {
            textMeshPro.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        // スタートカウントダウン終了後にゲームを開始
        textMeshPro.text = "Go!";
        BGM.SetActive(true);
        QuestImg.SetActive(false);
        yield return new WaitForSeconds(1);
        gameStarted = true; // ゲーム開始をフラグ
    }
}
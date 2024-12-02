using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private TMP_Text yourScore;
    [SerializeField] private GameObject scoresS; // 総合スコア表示用ゲームオブジェクト(S)
    [SerializeField] private GameObject scoresA; // 総合スコア表示用ゲームオブジェクト(A)
    [SerializeField] private GameObject scoresB; // 総合スコア表示用ゲームオブジェクト(B)
    [SerializeField] private GameObject scoresC; // 総合スコア表示用ゲームオブジェクト(C)
    [SerializeField] private TextMeshProUGUI socialScore; // ハイスコア表示用のTextMeshProオブジェクト
    [SerializeField] private TextMeshProUGUI rankingScore1; // ハイスコア表示用のTextMeshProオブジェクト
    [SerializeField] private TextMeshProUGUI rankingScore2; // ハイスコア表示用のTextMeshProオブジェクト
    [SerializeField] private TextMeshProUGUI rankingScore3; // ハイスコア表示用のTextMeshProオブジェクト
    [SerializeField] private TextMeshProUGUI rankingNotice; // ハイスコア表示用のTextMeshProオブジェクト

    private bool _nextScene;
    private int _currentScore;

    private MoveScene _sceneObj;

    private TextMeshProUGUI _rankin;
    private float _time;
    private bool _enlarge;

    private bool _isDebug = false;

    private float _timer;

    private void Start()
    {
        _timer = 0;
        _sceneObj = GetComponent<MoveScene>();
        _nextScene = false;
        _enlarge = true;

        // スコアとランキングの表示
        _currentScore = PlayerPrefs.GetInt("SCORE", 0);
        PlayerResult();
        JudgeHighScore();

        if (!_isDebug)
        {
            MarmotSenser.Instance?.SendWriteUpNum("0");
            Invoke(nameof(SendHipoSenser), 3f);
            Invoke(nameof(SendTigerSenser), 6f);
        }
    }


    // 次のシーンに移動する用
    private void Update()
    {
        if (_timer > 3)
        {
            if (!_isDebug)
            {
                string value = MarmotSenser.Instance?.GetValue() ?? "";
                string[] tmpAr = value.Split(',');

                if (tmpAr.Length > 1 && int.TryParse(tmpAr[1], out int result) && result > 150 && !_nextScene)
                {
                    _nextScene = true;
                    _sceneObj.LoadNextSceneAsync();
                }
            }
        }
        else
        {
            _timer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _nextScene = true;
            _sceneObj.LoadNextSceneAsync();
        }

        if (_rankin != null)
        {
            MoveRankinTMP();
        }
    }

    // ハイスコアの判定と表示
    private void JudgeHighScore()
    {
        // 現在のハイスコアを取得
        int highScore1 = PlayerPrefs.GetInt("HIGHSCORE1", 0);
        int highScore2 = PlayerPrefs.GetInt("HIGHSCORE2", 0);
        int highScore3 = PlayerPrefs.GetInt("HIGHSCORE3", 0);

        Debug.Log(highScore1);
        Debug.Log(highScore2);
        Debug.Log(highScore3);

        // 新しいスコアがランキングのどこに入るか判定
        if (_currentScore == highScore1 || _currentScore == highScore2 || _currentScore == highScore3)
        {
            switch (_currentScore)
            {
                case var score when score == highScore1:
                    rankingNotice.SetText("ランキング更新:1位");
                    _rankin = rankingScore1;
                    break;
                case var score when score == highScore2:
                    rankingNotice.SetText("ランキング更新:2位");
                    _rankin = rankingScore2;
                    break;
                case var score when score == highScore3:
                    rankingNotice.SetText("ランキング更新:3位");
                    _rankin = rankingScore3;
                    break;
            }
        }
        else if (_currentScore > highScore1)
        {
            PlayerPrefs.SetInt("HIGHSCORE3", highScore2);
            PlayerPrefs.SetInt("HIGHSCORE2", highScore1);
            PlayerPrefs.SetInt("HIGHSCORE1", _currentScore);
            rankingNotice.SetText("ランキング更新:1位");
            _rankin = rankingScore1;
        }
        else if (_currentScore > highScore2)
        {
            PlayerPrefs.SetInt("HIGHSCORE3", highScore2);
            PlayerPrefs.SetInt("HIGHSCORE2", _currentScore);
            rankingNotice.SetText("ランキング更新:2位");
            _rankin = rankingScore2;
        }
        else if (_currentScore > highScore3)
        {
            PlayerPrefs.SetInt("HIGHSCORE3", _currentScore);
            rankingNotice.SetText("ランキング更新:3位");
            _rankin = rankingScore3;
        }
        else
        {
            rankingNotice.SetText("");
        }

        // ランキングを保存
        PlayerPrefs.Save();
        rankingScore1.SetText(PlayerPrefs.GetInt("HIGHSCORE1", 0).ToString());
        rankingScore2.SetText(PlayerPrefs.GetInt("HIGHSCORE2", 0).ToString());
        rankingScore3.SetText(PlayerPrefs.GetInt("HIGHSCORE3", 0).ToString());
        socialScore.SetText($"{_currentScore}本");
    }

    private void PlayerResult()
    {
        // 総合スコアの表示
        if (_currentScore >= 17)
        {
            scoresS.SetActive(true);
        }
        else if (_currentScore >= 12)
        {
            scoresA.SetActive(true);
        }
        else if (_currentScore >= 8)
        {
            scoresB.SetActive(true);
        }
        else
        {
            scoresC.SetActive(true);
        }
    }

    private void MoveRankinTMP()
    {
        _time += _enlarge ? Time.deltaTime : -Time.deltaTime;

        if (_time < 0) _enlarge = true;
        else if (_time > 0.7f) _enlarge = false;

        _rankin.fontSize = Mathf.Lerp(60f, 80f, _time / 0.7f);
    }

    private void SendHipoSenser()
    {
        HipoSenser.Instance?.SendWriteUpNum("-1");
    }

    private void SendTigerSenser()
    {
        TigerSenser.Instance?.SendWriteUpNum("-1");
    }
}
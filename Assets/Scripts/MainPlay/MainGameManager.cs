using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private GameObject ToothManager;
    [SerializeField] private GameObject MessageManager;
    [SerializeField] private GameObject ActionSlideBar;
    [SerializeField] private GameObject clearVoiceHipo;
    [SerializeField] private GameObject clearVoiceTiger;
    [SerializeField] private GameObject cleanMouthHipo;
    [SerializeField] private GameObject cleanMouthTiger;
    [SerializeField] private GameObject endSE;

    private GameObject nowMessage;

    private int score;
    private string whoAnimal = "";
    private string[] tmp;
    private ActionSlideBar _toothSlideBar;
    private AudioSource _cleanMouthHipo;
    private AudioSource _cleanMouthTiger;

    void Start()
    {
        score = 0;

        _toothSlideBar = ActionSlideBar.GetComponent<ActionSlideBar>();
        _cleanMouthHipo = cleanMouthHipo.GetComponent<AudioSource>();
        _cleanMouthTiger = cleanMouthTiger.GetComponent<AudioSource>();
    }


    private void Update()
    {
        UpdateActionSlideBar();
    }

    private void UpdateActionSlideBar()
    {
        switch (whoAnimal)
        {
            case "Hipo":
                tmp = HipoSenser.Instance.GetValue().Split(",");
                _toothSlideBar.SetValue(int.Parse(tmp[1]));
                break;
            case "Tiger":
                tmp = TigerSenser.Instance.GetValue().Split(",");
                _toothSlideBar.SetValue(int.Parse(tmp[1]));
                break;
        }
    }

    public void NextTooth(int nextToothNum, string who, int index)
    {
        whoAnimal = who;

        nowMessage = MessageManager.GetComponent<ShowMessage>().Show(nextToothNum, who);

        string indexSt = index.ToString();

        if (who.Equals("Hipo"))
        {
            HipoSenser.Instance.SendWriteUpNum(indexSt);
        }
        else
        {
            TigerSenser.Instance.SendWriteUpNum(indexSt);
        }
    }

    public void ScoreUp()
    {
        score++;

        ClearMessage();

        if (whoAnimal.Equals("Hipo"))
        {
            _cleanMouthHipo.Play();
            Invoke(nameof(PlayClearVoiceHipo), 1);
        }
        else
        {
            _cleanMouthTiger.Play();
            Invoke(nameof(PlayClearVoiceTiger), 1);
        }

        //MessageManagerのnullチェック
        if (nowMessage != null)
        {
            MessageManager.GetComponent<MessageDestroy>().GameobjectDestroy(nowMessage);
        }

        Invoke(nameof(ClearTooth), 0.5f);
    }

    private void PlayClearVoiceHipo()
    {
        clearVoiceHipo.GetComponent<AudioSource>().Play();
    }

    private void PlayClearVoiceTiger()
    {
        clearVoiceTiger.GetComponent<AudioSource>().Play();
    }

    public int GetScore()
    {
        return score;
    }

    public void GameEnd()
    {
        endSE.GetComponent<AudioSource>().Play();
        Invoke(nameof(NextScene), 3f);
    }

    private void NextScene()
    {
        SetScorePrefs();
        this.GetComponent<MoveScene>().LoadNextSceneAsync();
    }

    private void ClearMessage()
    {
        // メッセージクリアの実装が必要
    }

    private void SetScorePrefs()
    {
        PlayerPrefs.SetInt("SCORE", score);
        PlayerPrefs.Save();
    }

    private void ClearTooth()
    {
        ToothManager.GetComponent<ToothManager>().FixDecay();
        ToothManager.GetComponent<ToothManager>().DecayRandomTooth();
    }
}
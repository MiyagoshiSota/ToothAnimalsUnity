using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutrialGameManager : MonoBehaviour
{
    [SerializeField] private GameObject TutrialActionSlideBar;
    [SerializeField] private GameObject clearVoice;
    [SerializeField] private GameObject cleanMouth;
    [SerializeField] private GameObject showSBObj;
    [SerializeField] private GameObject MessageManager;

    private GameObject HipoSenser;
    private GameObject nowMessage;
    private MoveScene _sceneObj;
    private void Start()
    {
        HipoSenser = GameObject.Find("HipoSenserManager");
        _sceneObj = gameObject.GetComponent<MoveScene>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _sceneObj.LoadNextSceneAsync();
        }
    }

    public void StartTutrial()
    {
        nowMessage = MessageManager.GetComponent<ShowMessage>().Show(0, "Hipo");
        TutrialActionSlideBar.GetComponent<TutrialActionSlideBar>().SetStartflg();
        HipoSenser.GetComponent<HipoSenser>().SendWriteUpNum("0");
    }

    //シーン移動
    public void TutrialClear()
    {
        //クリア音声の再生
        cleanMouth.GetComponent<AudioSource>().Play();
        Invoke(nameof(PlayClearVoice), 1);

        //クリア後メッセージの表示
        Invoke(nameof(ShowSB),3);
        Invoke(nameof(ShowSB),6);
        Invoke(nameof(ShowSB),9);

        Invoke(nameof(MovetMaingame), 12);
    }

    private void ShowSB()
    {
        showSBObj.GetComponent<TutrialShowSB>().Change();
    }

    private void PlayClearVoice()
    {
        Destroy(nowMessage);
        clearVoice.GetComponent<AudioSource>().Play();
    }

    private void MovetMaingame()
    {
        this.GetComponent<MoveScene>().LoadNextSceneAsync();
    }
}

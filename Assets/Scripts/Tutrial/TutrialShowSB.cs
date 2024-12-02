using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class SBs
{
    public GameObject obj;
    public GameObject voice;
    public string who;
}

public class TutrialShowSB : MonoBehaviour
{
    [SerializeField] private SBs[] sbs;
    [SerializeField] private GameObject TutrialGameManager;
    [SerializeField] private GameObject Decay;
    private GameObject oldMessage;
    private int index = 0;
    // Update is called once per frame

    [SerializeField] private AudioClip audioClip1;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform AspectController;

    //動物のポジション
    private const int marmotPosX = 600, marmotPosY = 340;
    private const int hipoPosX = -600, hipoPosY = 300;
    private const int tigerPosX = 600, tigerPosY = 300;
    private const int decayPosX = -470, decayPosY = 145;


    private void Start()
    {
        Decay.SetActive(false);
        
        Invoke(nameof(Change), 1f); //虎「ご飯食べたらそのまま寝るのが1番だよな」
        Invoke(nameof(Change), 3f); //マーモット「待て！そのまんま寝たらあいつが来てまうぞ！」
        Invoke(nameof(ShowDecay), 5f); //虫歯菌「ハッハッハ！歯を磨かないで寝てるヤツはどいつだ〜？？？」
        Invoke(nameof(PlayMp3), 5f); //虫歯菌「ハッハッハ！歯を磨かないで寝てるヤツはどいつだ〜？？？」
        Invoke(nameof(Change), 7f); //虎「うわ！虫歯菌だ！俺らの歯を狙ってきたんだ！」
        Invoke(nameof(Change), 9f); //虫歯菌「お前たちの歯をぜーんぶ蝕んでやるぞ！」
        Invoke(nameof(Change), 11f); //かば「うわ〜〜！！！」
        Invoke(nameof(Change), 13f); //マーモット「そこにいる人間！俺たちの歯を磨いて守ってくれ！！！」
        Invoke(nameof(Change), 15f); //マーモット「そこにいる人間！俺たちの歯を磨いて守ってくれ！！！」

        Invoke(nameof(StartTutrial), 17f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Change();
        }
    }

    private void PlayMp3()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip1;
        audioSource.Play();
    }

    private void ShowDecay()
    {
        Decay.SetActive(true);
    }

    public void Change()
    {
        if (index != 0)
        {
            Destroy(oldMessage);
        }

        SBs showsb;
        Quaternion rot = Quaternion.Euler(0f, 0f, 0f);

        showsb = sbs[index];

        string who = showsb.who;

        //Tigerであった場合、吹き出しを回転させる
        if (who == "Hipo")
        {
            rot = Quaternion.Euler(0f, 180f, 0f);
        }

        oldMessage = Instantiate(showsb.obj, transform.position, rot, AspectController);
        RectTransform rectTransform = oldMessage.GetComponent<RectTransform>();

        if (who == "Hipo")
        {
            //位置の調整
            rectTransform.anchoredPosition = new Vector2(hipoPosX, hipoPosY);
        }
        else if (who == "Tiger")
        {
            //位置の調整
            rectTransform.anchoredPosition = new Vector2(tigerPosX, tigerPosY);
        }
        else if (who == "Marmot")
        {
            //位置の調整
            rectTransform.anchoredPosition = new Vector2(marmotPosX, marmotPosY);
        }
        else if (who == "Decay")
        {
            //位置の調整
            rectTransform.anchoredPosition = new Vector2(decayPosX, decayPosY);
        }

        showsb.voice.GetComponent<AudioSource>().Play();

        index++;
    }

    private void StartTutrial()
    {
        TutrialGameManager.GetComponent<TutrialGameManager>().StartTutrial();
    }
}

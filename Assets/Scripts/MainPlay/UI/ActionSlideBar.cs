using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSlideBar : MonoBehaviour
{
    public Slider slider;
    int maxHp = 100;
    public static int Hp;
    public static int Score;

    private int nowValue;
    private float lastMaxHpTime; // 最後にHpがmaxHpになった時刻を記録する変数

    private int oldHp = 0;
    private AudioSource _migakuAudioSource;

    [SerializeField] private GameObject MainGameManager;
    [SerializeField] private GameObject migakuSE;

    void Start()
    {
        slider.maxValue = maxHp;
        slider.value = 0;
        Hp = 0;
        Score = 0;
        lastMaxHpTime = -1f; // 初期化

        _migakuAudioSource= migakuSE.GetComponent<AudioSource>();

        if (MainGameManager == null)
        {
            Debug.LogError("MainGameManager is not assigned!");
        }
    }

    void Update()
    {
        //以下デバッグ用
        //------------------------------------------------------
        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     Hp = Mathf.Max(0, Hp - 10);
        // }
        // if (Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     Hp = Mathf.Min(maxHp, Hp + 10);
        // }
        //------------------------------------------------------

        Hp = nowValue / 2;
        Debug.Log(Hp);

        if(oldHp != Hp){
            _migakuAudioSource.Play();
        }

        if (Hp < 88)
        {
            slider.value = Hp;
        }
        else
        {
            // 1秒以内にこの条件に入らないようにする
            if (Time.time - lastMaxHpTime >= 2f)
            {
                Hp = 0;
                slider.value = Hp;
                Score++;
                nowValue = 0;
                lastMaxHpTime = Time.time; // 現在の時間を記録

                if (MainGameManager != null)
                {
                    MainGameManager.GetComponent<MainGameManager>().ScoreUp();
                }
            }
        }
        oldHp = Hp;
    }

    public void SetValue(int v)
    {
        nowValue = v;
    }
}

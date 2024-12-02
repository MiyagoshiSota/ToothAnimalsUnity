using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutrialActionSlideBar : MonoBehaviour
{
    public Slider slider;

    int maxHp = 100;
    public static int Hp;
    public static int Score;

    private HipoSenser hip;

    [SerializeField] private GameObject TutrialGameManager;
    [SerializeField] private GameObject migakuSE;
    private int oldHp = 0;
    private bool startflg;

    private GameObject HipoSenser;

    void Start()
    {
        // Sliderの最大値を設定
        slider.maxValue = maxHp;

        // Sliderを最大にする。
        slider.value = 0;

        // HPを最大HPと同じ値に。
        Hp = 0;

        Score = 0;

        startflg = false;

        HipoSenser = GameObject.Find("HipoSenserManager");
    }


    void Update()
    {
        if (startflg)
        {
            //以下デバッグ用
            //------------------------------------------------------x
            // if (Input.GetKeyDown(KeyCode.LeftArrow))
            // {
            //     // デバッグログ
            //     Debug.Log("Left Arrow Pressed");
            //
            //     // HPから10を引く
            //     Hp = Mathf.Max(0, Hp - 10); // HPが0未満にならないようにする
            // }
            // if (Input.GetKeyDown(KeyCode.RightArrow))
            // {
            //     // デバッグログ
            //     Debug.Log("Right Arrow Pressed");
            //
            //     // HPに10を加える
            //     Hp = Hp + 10; // HPが最大HPを超えないようにする
            //
            //     // HPをSliderに反映。
            // }
            //------------------------------------------------------x

            string[] value = HipoSenser.GetComponent<HipoSenser>().GetValue().Split(",");
            Hp = int.Parse(value[1]) / 2;

            if (oldHp != Hp)
            {
                migakuSE.GetComponent<AudioSource>().Play();
            }

            if (Hp < 92)
                slider.value = Hp;
            else
            {
                startflg = false;
                Debug.Log("hi");
                TutrialGameManager.GetComponent<TutrialGameManager>().TutrialClear();
            }
            oldHp = Hp;
        }
    }

    public void SetStartflg()
    {
        startflg = true;
    }
}

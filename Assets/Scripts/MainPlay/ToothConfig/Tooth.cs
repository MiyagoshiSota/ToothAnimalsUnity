using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    public bool isDecayed = false;

    public void MakeDecayed()
    {
        isDecayed = true;
        // 虫歯にする処理を追加
        GetComponent<Renderer>().material.color = Color.black;  // 例: 色を黒くする
    }

    public void Heal()
    {
        isDecayed = false;
        // 虫歯を治す処理を追加
        GetComponent<Renderer>().material.color = new Color(232f / 255f, 221f / 255f, 188f / 255f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDecayed)
        {
            // Hpの値に応じて色を変える。Hpが0のとき黒、100のとき白。
            byte colorValueR = (byte)(230 * ActionSlideBar.Hp / 100);
            byte colorValueG = (byte)(221 * ActionSlideBar.Hp / 100);
            byte colorValueB = (byte)(191 * ActionSlideBar.Hp / 100);
            GetComponent<Renderer>().material.color = new Color32(colorValueR, colorValueG, colorValueB, 255);
        }
    }
}

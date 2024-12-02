using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[Serializable]
public class Messages
{
    public GameObject obj;
    public int index;
    public int posx;
    public int posy;
}

public class ShowMessage : MonoBehaviour
{
    [SerializeField] private Messages[] msgs;

    [SerializeField]
    private Transform AspectController;

    private int index;

    public GameObject Show(int num, string who)
    {
        Messages showms;
        Quaternion rot;

        //どの動物かの判定
        if (who == "Hipo")
        {
            index = 0;
            rot = Quaternion.Euler(0f, 180f, 0f);

            //削除スクリプトのオン
            this.GetComponent<MessageSE>().Play("Hipo");
        }
        else
        {
            index = 1;
            rot = Quaternion.Euler(0f, 0f, 0f);

            //削除スクリプトのオン
            this.GetComponent<MessageSE>().Play("Tiger");
        }

        //メッセージオブジェクトの代入と簡単な位置の設定
        showms = msgs[index];
        GameObject msg = Instantiate(showms.obj, transform.position, rot, AspectController);

        //位置の調整
        RectTransform rectTransform = msg.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(showms.posx, showms.posy);

        return msg;
    }
}

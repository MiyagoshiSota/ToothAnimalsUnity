using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[Serializable]
public class Tooths
{
    public GameObject obj;
    public string who;
    public int index;
}

public class ToothManager : MonoBehaviour
{
    private Tooths selectedTooth;
    [SerializeField] private Tooths[] tooths;
    private int nextToothNum;

    //Message表示用
    [SerializeField] private GameObject MainGameManager;

    private void Start() {
        Invoke(nameof(DecayRandomTooth), 4f);
    }

    // ランダムに歯を選んで虫歯にする
    public void DecayRandomTooth()
    {

        if (tooths.Length > 0)
        {
            int ran;

            //ランダム値が重複しないようにする
            do{
                ran = UnityEngine.Random.Range(0, tooths.Length);
            }
            while(ran == nextToothNum);

            nextToothNum = ran;
            selectedTooth = tooths[nextToothNum];

            //メッセージの表示とライトの点灯
            MainGameManager.GetComponent<MainGameManager>().NextTooth(nextToothNum,selectedTooth.who,selectedTooth.index);

            // ここで歯を虫歯にする処理を追加
            selectedTooth.obj.GetComponent<Tooth>().MakeDecayed();
        }
    }

    private void NextMakeDecayed(){
        selectedTooth.obj.GetComponent<Tooth>().MakeDecayed();
    }

    public void FixDecay()
    {
        selectedTooth.obj.GetComponent<Tooth>().Heal();
    }
}
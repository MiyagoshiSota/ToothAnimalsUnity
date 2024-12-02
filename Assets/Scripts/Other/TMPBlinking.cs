using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPBlinking : MonoBehaviour
{
    // 点滅させる対象
    [SerializeField] private Renderer _target;
    // 点滅周期[s]
    [SerializeField] private float _cycle = 1;

    private Material _material;
    private double _time;

    private void Awake()
    {
        // レンダラーのマテリアルを保持しておく
        _material = _target.material;
    }

    private void Update()
    {   
        // 周期cycleで繰り返す値の取得
        // 0～cycleの範囲の値が得られる
        var repeatValue = Mathf.Sin(Time.time * _cycle) / 2 + 0.7f;

        if(repeatValue > 1){
            repeatValue = 1;
        }
        
        // 内部時刻timeにおける明滅状態を反映
        // マテリアル色のアルファ値を変更している
        var color = _material.color;
        color.a = repeatValue;
        _material.color = color;
    }

    private void OnDestroy()
    {
        // 不要になったマテリアルを破棄
        Destroy(_material);
    }
}

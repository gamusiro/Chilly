using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HP : MonoBehaviour
{
    [SerializeField] private List<Image> _hpImageList;//表示画像
    int _hp = 0;//現在の体力

    private void Start()
    {
        _hp = _hpImageList.Count - 1;//HPの値を設定する

       
        Recover();
    }

    public void Hit()
    {
        //やられていなければ処理を続行
        if (_hp - 1 < 0)
            return;

        //アニメーション
        float alpha = 0.0f;
        float time = 1.0f;
        _hpImageList[_hp].DOFade(alpha, time).SetLink(this.gameObject);

        //体力を減らす
        _hp--;

        //Debug.Log("ダメージ: " + _hp);
    }

    public void Recover()
    {
        //体力が上限になっていなければ処理を続行
        if (_hp >= _hpImageList.Count - 1)
            return;

        //体力を減らす
        _hp++;

        //アニメーション
        float alpha = 1.0f;
        float time = 1.0f;
        _hpImageList[_hp].DOFade(alpha, time).SetLink(this.gameObject);

        //Debug.Log("回復: " + _hp);
    }
}

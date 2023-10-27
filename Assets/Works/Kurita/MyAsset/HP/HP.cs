using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField] private SpriteSO _spriteList;//spriteList
    [SerializeField] private Image _image;//表示画像
    [SerializeField] private int _hp;

    //フェードアニメーション系
    private bool _animeFlag;//アニメーション中かの判定
    private float _alpha;//α値
    private float _curflashNum;//現在の点滅回数
    private const float MAX_FLASH_NUM = 3.0f;//最大の点滅回数

    private void Start()
    {
        _hp = _spriteList.sprite.Count;
        _animeFlag = false;
        _curflashNum = 0;
    }

    private void Update()
    {
        
    }

    private void Hit()
    {
        //体力を減らす
        _hp--;

        //やられた判定
        if (_hp <= 0)
            return;

        //HP情報を更新
        SetHitAnimation();
    }

    private void SetHitAnimation()
    {
        _animeFlag = true;
        _alpha = 1.0f;
        _curflashNum = MAX_FLASH_NUM;
    }
    private void HitAnimation()
    {
        if(_animeFlag)
        {
            if (_curflashNum > 0)
            {
                _alpha -= 0.1f;

                if (_alpha <= 0.0f)
                {
                    _alpha = 1.0f;
                    _curflashNum--;
                }
            }
            else 
            {
            _image.sprite = _spriteList.sprite[_hp];
            
            }
        }
    }
}

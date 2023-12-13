using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using DG.Tweening;

public class GameEnemy : Enemy
{
    //体のパーティクルに関するもの
    [SerializeField] protected ParticleSystem _particleSystem;
    protected float _frameHit;//ヒットしてからのフレーム

    [SerializeField] protected Gradient _gradientOriginal;//元の色
    [SerializeField] protected Gradient _gradientHit;//ヒット時の色
    [SerializeField] protected Color _defeatColor;//倒したときの色

    //動き
    [SerializeField] protected Transform _standardPosition;
    protected float _moveSpeed;

    private void Start()
    {
        //目と口のラジアン
        _eyeTransform[(int)Eye.Left].eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
        _eyeTransform[(int)Eye.Right].eulerAngles = new Vector3(0.0f, 0.0f, 225.0f);
        _subEyeSpeed = _eyeSpeed = 4.0f;
        _subEyeSpeed *= 0.1f;
        _mouthScaleeRadian = 0.0f;
        _subMouthSpeed = _mouthSpeed = 0.04f;
        _subMouthSpeed *= 0.4f;
        _standardMouthScale = _mouthTransform.localScale;
        _moveSpeed = 3.0f;
        //その他変数
        _frameHit = 999.0f;//ヒットフレーム
    }

    private void FixedUpdate()
    {
        HitAnimation();
        Eyes();
        Mouth();

        float range = 10.0f;
        Move(range);
    }

    //当たり判定
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HeartNotes")
        {
            Hit();
            Debug.Log("トリガーにヒット");
        }
    }

    //攻撃された判定
    protected void Hit()
    {
        _frameHit = 0;
    }

    protected void HitAnimation()
    {
        //色変更
        ParticleSystem.ColorOverLifetimeModule colorOverLifetime = _particleSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient gradient;
        gradient = _gradientOriginal;

        //ヒットアニメーションが作動する時間
        if (_frameHit < 2.0f)
        {
            _frameHit += Time.deltaTime * 10.0f;

            //点滅させる
            if (((int)_frameHit) % 2 == 0)
                gradient = _gradientHit;
            else
                gradient = _gradientOriginal;
        }
        colorOverLifetime.color = gradient;
    }
}

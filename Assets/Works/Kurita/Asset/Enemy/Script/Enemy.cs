using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class Enemy : MonoBehaviour
{
    //目の動き関するもの
    private enum Eye   
    {
        Left,
        Right
    }
    [SerializeField] protected List<Transform> _eyeTransform = new List<Transform>();
    protected List<float> _eyeRadian = new List<float>();//回転角度
    protected float _eyeSpeed;
    protected float _subEyeSpeed;

    //光
    [SerializeField] protected List<Transform> _extinctionTransform = new List<Transform>();

    //口に関するもの
    [SerializeField] protected Transform _mouthTransform;
    protected float _mouthScaleeRadian;//口の大きさ変更を決定する値
    protected Vector3 _standardMouthScale;
    protected float _mouthSpeed;
    protected float _subMouthSpeed;

    //体のパーティクルに関するもの
    [SerializeField] protected ParticleSystem _particleSystem;
     protected float _frameHit;//ヒットしてからのフレーム

    [SerializeField] protected Gradient _gradientOriginal;//元の色
    [SerializeField] protected Gradient _gradientHit;//ヒット時の色
    [SerializeField] protected Color _defeatColor;//倒したときの色

    //やられたときの目(Prefab)
    [SerializeField] protected DisapperEyes _disapperEyePrefab;
    protected DisapperEyes _disapperEyeInstance = null;
    [SerializeField] protected GameObject _explosionPrefab;
    [SerializeField] protected GameObject _explosionPrefab2;

    public void Start()
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

        //その他変数
        _frameHit = 999.0f;//ヒットフレーム
    }

    public void FixedUpdate()
    {
        HitAnimation();
        Eyes();
        Mouth();
    }

    //目の動き
    protected void Eyes()
    {
        _eyeTransform[(int)Eye.Left].Rotate(0.0f, 0.0f, _eyeSpeed * 2.0f);
        _eyeTransform[(int)Eye.Right].Rotate(0.0f, 0.0f, -_eyeSpeed);
    }

    //口の動き
    protected void Mouth()
    {
        //ぱくぱく
        float cosRange = 6.0f;
        float sinRangege = _standardMouthScale.y * 0.5f;

        _mouthScaleeRadian += _mouthSpeed;
        Vector3 addValue = Vector3.zero;
        addValue.y = Mathf.Sin(Mathf.Cos(_mouthScaleeRadian) * cosRange) * sinRangege;
        _mouthTransform.localScale = _standardMouthScale + addValue;
    }

    //当たり判定
    protected void OnParticleCollision(GameObject other)
    {
        //インスペクター側でレイヤーの設定済
        Hit();
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
        if (_frameHit < 10.0f)
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

    //----倒された時の処理----
    //やられる
    public async void Disapper(Transform parent)
    {
        _disapperEyeInstance = Instantiate(_disapperEyePrefab, this.transform);

        while (true) 
        {
            await UniTask.DelayFrame(1);

            //動きを遅くする
            _eyeSpeed -= _subEyeSpeed * Time.deltaTime;
            _eyeSpeed = Mathf.Clamp(_eyeSpeed, 0.0f, _eyeSpeed);
            _mouthSpeed -= _subMouthSpeed * Time.deltaTime;
            _mouthSpeed = Mathf.Clamp(_mouthSpeed, 0.0f, _mouthSpeed);
            //やられたときの目の動きと通常の目の動きを合わせる
            if (_disapperEyeInstance)
                _disapperEyeInstance.SetAngle(_eyeTransform[(int)Eye.Left].eulerAngles, _eyeTransform[(int)Eye.Right].eulerAngles);
        }
    }

    //爆発
    public async void Explosion(Transform parent)
    {
        Instantiate(_explosionPrefab, parent);
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        Instantiate(_explosionPrefab2, parent);
    }
}

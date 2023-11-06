using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //口に関するもの
    [SerializeField] protected Transform _mouthTransform;
    protected float _MouthScaleeRadian;//口の大きさ変更を決定する値
    protected Vector3 _standardMouthScale;

    //体のパーティクルに関するもの
    [SerializeField] protected ParticleSystem _particleSystem;
     protected float _frameHit;//ヒットしてからのフレーム

    [SerializeField] protected Gradient _gradientOriginal;//元の色
    [SerializeField] protected Gradient _gradientHit;//ヒット時の色
    [SerializeField] protected Color _defeatColor;//倒したときの色

    //状態
    private bool _defeated;

    public void Start()
    {
        //目と口のラジアン
        _eyeTransform[(int)Eye.Left].eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
        _eyeTransform[(int)Eye.Right].eulerAngles = new Vector3(0.0f, 0.0f, 225.0f);
        _MouthScaleeRadian = 0.0f;
        _standardMouthScale = _mouthTransform.localScale;

        //その他変数
        _frameHit = 999.0f;//ヒットフレーム
        _defeated = false;
    }

    public void Update()
    {
        if (!_defeated)
        {
            HitAnimation();
        }
        Eyes();
        Mouth();
        //ChangeColor();
    }


    //目の動き
    protected void Eyes()
    {
        float speed = 2.0f;
        _eyeTransform[(int)Eye.Left].Rotate(0.0f, 0.0f, speed);
        _eyeTransform[(int)Eye.Right].Rotate(0.0f, 0.0f, -speed); 
    }

    //口の動き
    protected void Mouth()
    {
        //ぱくぱく
        float speed = 0.04f;
        float cosRange = 6.0f;
        float sinRangege = _standardMouthScale.y * 0.5f;

        _MouthScaleeRadian += speed;
        Vector3 addValue = Vector3.zero;
        addValue.y = Mathf.Sin(Mathf.Cos(_MouthScaleeRadian) * cosRange) * sinRangege;
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

    private void ChangeColor()
    {
        //色変更
        ParticleSystem.ColorOverLifetimeModule colorOverLifetime = _particleSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;
        colorOverLifetime.color = _gradientHit;


        ParticleSystem.MainModule main = _particleSystem.main;
        main.startColor = _defeatColor;
        _frameHit = 0;
    }

    //---アニメーターからの呼び出し---
    [SerializeField] public void ChangeColorByAnimator()
    {
        _defeated = true;
    }
}

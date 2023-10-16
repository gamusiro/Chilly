using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseObject
{
    //目の動き関するもの
    [SerializeField] protected List<Transform> _transformEyes;
    [SerializeField] protected List<Transform> _transformEyesStandardPosition;//目の標準位置

    protected List<float> _radianEyesRotate = new();//目の回転角度
    protected List<float> _radianEyesCircleMotion = new();//目の円運動の角度

    //口に関するもの
    [SerializeField] protected Transform _transformMouth;
    [SerializeField] protected Transform _transformMouthStandardPosition;//口の標準の位置

    protected Vector3 _standardMouthScale = new Vector3();//口の標準の大きさ
    protected float _radianMouthRescale = new();//口の大きさ変更の角度
    protected float _radianMouthRotate;//口の回転角度
    protected float _radianMouthCircleMotion;//口の円運動の角度

    //体のパーティクルに関するもの
    [SerializeField] protected ParticleSystem _particleSystem;
     protected float _frameHit = new();//ヒットしてからのフレーム

    [SerializeField] protected Gradient _gradientOriginal;//元の色
    [SerializeField] protected Gradient _gradientHit;//ヒット時の色

    public override void Initialized()
    {
        //各パーツの角度を決める
        foreach (var transformEyes in _transformEyes)
        {
            float radian = Random.Range(0.0f, 2.0f) * Mathf.PI;
            _radianEyesRotate.Add(radian);
            _radianEyesCircleMotion.Add(radian);
        }
        _radianMouthRotate = Random.Range(0.0f, 2.0f) * Mathf.PI;
        _radianMouthCircleMotion = Random.Range(0.0f, 2.0f) * Mathf.PI;
        _radianMouthRescale = Random.Range(0.0f, 2.0f) * Mathf.PI;

        //その他変数
        _standardMouthScale = _transformMouth.localScale;//口の標準の大きさ
        _frameHit = 999.0f;//ヒットフレーム
    }

    public override void Updated()
    {
        HitAnimation();
        Eyes();
        Mouth();
    }

    //プラスとマイナスを交互に出力する。
    protected float AlternatingPlusAndMinus(int index)
    {
        if (index % 2 == 0)
            return 1.0f;
        else
            return -1.0f;
    }

    //目の動き
    protected void Eyes()
    {
        for (int i = 0; i < _transformEyes.Count; i++)
        {
            //円運動
            _radianEyesCircleMotion[i] +=  AddValue(AlternatingPlusAndMinus(i) * 0.04f, 20.0f);
            Vector3 addValue = new Vector3(Mathf.Sin(_radianEyesCircleMotion[i]), Mathf.Cos(_radianEyesCircleMotion[i]), 0.0f);
            addValue *= AddValue(0.5f,1.0f);
            _transformEyes[i].position = _transformEyesStandardPosition[i].position + addValue;

            //回転
            _transformEyes[i].Rotate(0.0f, 0.0f, AddValue(AlternatingPlusAndMinus(i) * 4.0f, 5.0f));
        }
    }

    //口の動き
    protected void Mouth()
    {
        //円運動
        _radianMouthCircleMotion += AddValue(0.01f, 1.0f);
        Vector3 addValue = new Vector3(Mathf.Sin(_radianMouthCircleMotion), Mathf.Cos(_radianMouthCircleMotion), 0.0f);
        addValue *= 0.5f;
        _transformMouth.position = _transformMouthStandardPosition.position + addValue;

        //回転
        _radianMouthRotate += AddValue(0.03f, 1.0f);
        Vector3 eulerAngle = _transformMouth.eulerAngles;
        eulerAngle.z = Mathf.Sin(_radianMouthRotate) * 13.0f;
        _transformMouth.eulerAngles = eulerAngle;

        //ぱくぱく
        _radianMouthRescale += AddValue(0.01f, 6.0f);
        Vector3 addScale = Vector3.zero;
        addScale.y = Mathf.Sin(Mathf.Cos(_radianMouthRescale * 3.0f) * 6.0f) * 4.0f;
        _transformMouth.localScale = _standardMouthScale + addScale;
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
        Gradient gradient = new();
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

    protected float AddValue(float value,float multiple)
    {
        if (_frameHit < 10.0f)
            return value * multiple;
        else
            return value;
    }
}

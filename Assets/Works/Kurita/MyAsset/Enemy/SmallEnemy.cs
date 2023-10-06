using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : Enemy
{
    [Header("スポーン地点")]
    [SerializeField] private Transform _spawnTransform;
    [Header("移動開始時間(スポーンしてから計測)")]
    [SerializeField] private Transform _startTime;
    [Header("存在する時間(移動開始してから計測)")]
    [SerializeField] private Transform _lifeTime;


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

    private new void Eyes()
    {
        for (int i = 0; i < _transformEyes.Count; i++)
        {
            //円運動
            _radianEyesCircleMotion[i] += AddValue(AlternatingPlusAndMinus(i) * 0.04f, 20.0f);
            Vector3 addValue = new Vector3(Mathf.Sin(_radianEyesCircleMotion[i]), Mathf.Cos(_radianEyesCircleMotion[i]), 0.0f);
            addValue *= AddValue(0.5f, 1.0f);
            _transformEyes[i].position = _transformEyesStandardPosition[i].position + addValue;
        }
    }
}

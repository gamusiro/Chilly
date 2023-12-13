using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using DG.Tweening;

public class SmallEnemy : Enemy
{
    //やられる
    [SerializeField] private GameObject _explosionPrefabA;
    [SerializeField] private Transform _explosionParent;

    //カメラ
    [SerializeField] private CameraPhaseManager _cameraPhaseManager;

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
        Move();
    }

    //プレイヤーにぶつかったら爆発する
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //爆発する
            Instantiate(_explosionPrefabA, _explosionParent);

            //カメラを揺らす
            _cameraPhaseManager.Shake();

            //自分を消す
            Destroy(this.gameObject);
        }
    }
}
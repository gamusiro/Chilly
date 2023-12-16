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
    private Transform _explosionParent = null;

    //カメラ
    private CameraPhaseManager _cameraPhaseManager;

    public void Initialize(CameraPhaseManager cpm, Transform parent)
    {
        _cameraPhaseManager = cpm;
        _explosionParent = parent;
    }

    private void Start()
    {
        //目と口のラジアン
        _mouthScaleeRadian = 0.0f;
        _subMouthSpeed = _mouthSpeed = 0.04f;
        _subMouthSpeed *= 0.4f;
        _standardMouthScale = _mouthTransform.localScale;
        _moveSpeed = 3.0f;
    }

    private void FixedUpdate()
    {
        Mouth();

        float range = 5.0f;
        Move(range);
    }

    //プレイヤーにぶつかったら爆発する
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        if (other.tag == "Player")
        {
            CS_Player player = obj.GetComponent<CS_Player>();
            if (player.IsDashing)
            {
                if (_explosionParent == null) 
                    Debug.LogError("親が設定されていません");

                //爆発する
                Instantiate(_explosionPrefabA, this.transform.position, Quaternion.identity, _explosionParent);

                //カメラを揺らす
                _cameraPhaseManager.Shake();

                // 音を鳴らす
                CS_AudioManager.Instance.PlayAudio("DestroySmallEnemy");

                //自分を消す
                Destroy(this.gameObject);
            }
            else
            {
                // ダメージ
                player.Damage();
            }
        }
    }
}
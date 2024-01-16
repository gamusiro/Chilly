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
    [SerializeField] private Transform _enemyModel;
    [SerializeField] private GameObject _explosionPrefabA;
    private Transform _explosionParent = null;
    private Transform _player;
    [SerializeField] private GameObject _smallEnemyRoot;
    private Vector3 _angle = Vector3.zero;

    //カメラ
    private GameCameraPhaseManager _cameraPhaseManager;

    public void Initialize(GameCameraPhaseManager cpm, Transform parent,Transform player)
    {
        _cameraPhaseManager = cpm;
        _explosionParent = parent;
        _player = player;
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
        //プレイヤーの方を向く
        LookAt();  
    }

    //プレイヤーにぶつかったら爆発する
    private void OnTriggerStay(Collider other)
    {
        GameObject obj = other.gameObject;

        if (other.tag == "Player")
        {
            CS_Player player = obj.GetComponent<CS_Player>();
            if (player.IsDashing)
            {
                if (_explosionParent == null) 
                    Debug.LogError("親が設定されていません");

                //プレイヤーの動きを止める
                player.ResetVel();

                //爆発する
                Instantiate(_explosionPrefabA, this.transform.position, Quaternion.identity, _explosionParent);

                //カメラを揺らす
                _cameraPhaseManager.Shake();

                // 音を鳴らす
                CS_AudioManager.Instance.PlayAudio("DestroySmallEnemy");

                //自分を消す
                Destroy(_smallEnemyRoot);
            }
            else
            {
                // ダメージ
                player.Damage();
            }
        }
    }

    //プレイヤーの方を向く
    private void LookAt()
    {
        //逆向きの時以外は
        if (_cameraPhaseManager?.GetCurCamera().tag == "ReverseCamera")
        {
            _enemyModel.transform.eulerAngles = Vector3.zero;
            _enemyModel.transform.eulerAngles += new Vector3(0.0f, 180.0f, 0.0f);
        }
        else
        {
            if (_player != true)
                return;

            _enemyModel.transform.LookAt(_player.position);

            Vector3 angel = _enemyModel.transform.eulerAngles;
            const float frequency = 180.0f;
            angel.x = frequency - angel.x;
            angel.y = frequency - angel.y;
            angel.z = frequency - angel.z;
            _enemyModel.transform.eulerAngles = angel;
        }
    }
}
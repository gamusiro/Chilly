using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using DG.Tweening;

public class SmallEnemy : Enemy
{
    //�����
    [SerializeField] private GameObject _explosionPrefabA;
    [SerializeField] private Transform _explosionParent;

    //�J����
    [SerializeField] private CameraPhaseManager _cameraPhaseManager;

    private void Start()
    {
        //�ڂƌ��̃��W�A��
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

    //�v���C���[�ɂԂ������甚������
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //��������
            Instantiate(_explosionPrefabA, this.transform.position, Quaternion.identity, _explosionParent);

            //�J������h�炷
            _cameraPhaseManager.Shake();

            //����������
            Destroy(this.gameObject);
        }
    }
}
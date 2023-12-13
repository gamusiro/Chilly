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
        _eyeTransform[(int)Eye.Left].eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
        _eyeTransform[(int)Eye.Right].eulerAngles = new Vector3(0.0f, 0.0f, 225.0f);
        _subEyeSpeed = _eyeSpeed = 4.0f;
        _subEyeSpeed *= 0.1f;
        _mouthScaleeRadian = 0.0f;
        _subMouthSpeed = _mouthSpeed = 0.04f;
        _subMouthSpeed *= 0.4f;
        _standardMouthScale = _mouthTransform.localScale;
        _moveSpeed = 3.0f;
        //���̑��ϐ�
        _frameHit = 999.0f;//�q�b�g�t���[��
    }

    private void FixedUpdate()
    {
        HitAnimation();
        Eyes();
        Mouth();
        Move();
    }

    //�v���C���[�ɂԂ������甚������
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //��������
            Instantiate(_explosionPrefabA, _explosionParent);

            //�J������h�炷
            _cameraPhaseManager.Shake();

            //����������
            Destroy(this.gameObject);
        }
    }
}
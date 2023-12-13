using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    //�ڂ̓����ւ������
    protected enum Eye
    {
        Left,
        Right
    }
    [SerializeField] protected List<Transform> _eyeTransform = new List<Transform>();
    protected List<float> _eyeRadian = new List<float>();//��]�p�x
    protected float _eyeSpeed;
    protected float _subEyeSpeed;

    //���Ɋւ������
    [SerializeField] protected Transform _mouthTransform;
    protected float _mouthScaleeRadian;//���̑傫���ύX�����肷��l
    protected Vector3 _standardMouthScale;
    protected float _mouthSpeed;
    protected float _subMouthSpeed;

    //����
    [SerializeField] protected Transform _standardPosition;
    protected float _moveSpeed;

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
    }

    private void FixedUpdate()
    {
        Eyes();
        Mouth();
        float range = 10.0f;
        Move(range);
    }

    //�ڂ̓���
    protected void Eyes()
    {
        _eyeTransform[(int)Eye.Left].Rotate(0.0f, 0.0f, _eyeSpeed * 2.0f);
        _eyeTransform[(int)Eye.Right].Rotate(0.0f, 0.0f, -_eyeSpeed);
    }

    //���̓���
    protected void Mouth()
    {
        //�ς��ς�
        float cosRange = 6.0f;
        float sinRangege = _standardMouthScale.y * 0.5f;

        _mouthScaleeRadian += _mouthSpeed;
        Vector3 addValue = Vector3.zero;
        addValue.y = Mathf.Sin(Mathf.Cos(_mouthScaleeRadian) * cosRange) * sinRangege;
        _mouthTransform.localScale = _standardMouthScale + addValue;
    }

    //����
    protected void Move(float range)
    {
        Vector3 position = this.transform.position;
        position.y = _standardPosition.position.y + Mathf.Sin(Time.time* _moveSpeed) * range;
        this.transform.position = position;

        Vector3 angle = this.transform.eulerAngles;
        angle.y = Mathf.Sin(Time.time * _moveSpeed * 0.4f)*range;
        angle.z = Mathf.Cos(Time.time * _moveSpeed) *range;
        this.transform.eulerAngles = angle;
    }
}

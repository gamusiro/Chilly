using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : Enemy
{
    //private Vector3 _velocity = new Vector3();
    private bool _first = new bool();
    //private bool _isMoving = new bool();

    //public void Initialized(Vector3 velocity)
    //{
    //    //�ړ��x�N�g�������߂�
    //    _velocity = velocity;

    //    //�e�p�[�c�̊p�x�����߂�
    //    foreach (var transformEyes in _transformEyes)
    //    {
    //        float radian = Random.Range(0.0f, 2.0f) * Mathf.PI;
    //        _radianEyesRotate.Add(radian);
    //        _radianEyesCircleMotion.Add(radian);
    //    }
    //    _radianMouthRotate = Random.Range(0.0f, 2.0f) * Mathf.PI;
    //    _radianMouthCircleMotion = Random.Range(0.0f, 2.0f) * Mathf.PI;
    //    _radianMouthRescale = Random.Range(0.0f, 2.0f) * Mathf.PI;

    //    //���̑��ϐ�
    //    _standardMouthScale = _transformMouth.localScale;//���̕W���̑傫��
    //    _frameHit = 999.0f;//�q�b�g�t���[��
    //    _first = true;
    //    _isMoving = false;
    //}

    //private new void Update()
    //{
    //    if (_first)
    //    {
    //        HitAnimation();
    //        Eyes();
    //        Mouth();
    //        //Move();
    //    }
    //}

    //private new void Eyes()
    //{
    //    for (int i = 0; i < _eyeTransform.Count; i++)
    //    {
    //        //�~�^��
    //        _radianEyesCircleMotion[i] += AddValue(AlternatingPlusAndMinus(i) * 0.04f, 20.0f);
    //        Vector3 addValue = new Vector3(Mathf.Sin(_radianEyesCircleMotion[i]), Mathf.Cos(_radianEyesCircleMotion[i]), 0.0f);
    //        addValue *= AddValue(0.5f, 1.0f);
    //        _eyeTransform[i].position = _transformEyesStandardPosition[i].position + addValue;
    //    }
    //}

    //private void Move()
    //{
    //    if (_isMoving)
    //        transform.position += _velocity * Time.deltaTime;
    //}

    //public void StartMoving()
    //{
    //    _isMoving = true;
    //}

    //public void Destroy()
    //{
    //    Destroy(this.gameObject);
    //}
}

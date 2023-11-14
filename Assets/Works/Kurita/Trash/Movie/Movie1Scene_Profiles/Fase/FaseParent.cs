using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

//ToDo:���o�����肵�Ă��琧��ĊJ

//�t�F�[�Y�N���X�̐e
public class FaseParent : MonoBehaviour
{
    [SerializeField] protected CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] protected Image _fadeImage;
    [SerializeField] protected LerpSO _fadeImageAlphaLerpSO;

    //protected void Awake()
    //{
    //    //�J�����̗D��x�ݒ�
    //    _cinemachineVirtualCamera.Priority = 1;

    //    //�t�F�[�h
    //    //_fadeImageAlphaLerpSO = new LerpSO();
    //    //_fadeImageAlphaLerpSO.StartValue = 1.0f;
    //    //_fadeImageAlphaLerpSO.EndValue = 0.0f;
    //    //_fadeImageAlphaLerpSO.CurrentPoint = 0.9f;
    //    //_fadeImageAlphaLerpSO.Speed = 0.05f;
    //    //  ChangeAlpha(_fadeImage, 1.0f);
    //    Debug.LogError("�Ăяo�����");
    //}

    //protected void OnDestroy()
    //{
    //    //�J�����̗D��x�ݒ�
    //    _cinemachineVirtualCamera.Priority = 0;
    //}

    //�I�u�W�F�N�g�̉�]������������
    protected void RotateObject(Transform transform, float speed)
    {
        Quaternion addAngle = Quaternion.Euler(0.0f, speed, 0.0f);
        transform.rotation = addAngle * transform.rotation;
    }

    //�I�u�W�F�N�g�̈ړ�����������
    protected void MoveObject(Transform transform, float speed)
    {
        Vector3 position = transform.position;
        Vector3 addVelocity = new Vector3(0.0f, speed, 0.0f);
        transform.position += addVelocity;
    }

    //�J�����̈Ó]
    protected void FadeOut()
    {
        _fadeImageAlphaLerpSO.CurrentPoint += _fadeImageAlphaLerpSO.Speed * Time.deltaTime / (_fadeImageAlphaLerpSO.StartValue - _fadeImageAlphaLerpSO.EndValue);
        float alpha = Mathf.Lerp(_fadeImageAlphaLerpSO.StartValue, _fadeImageAlphaLerpSO.EndValue,
            _fadeImageAlphaLerpSO.CurrentPoint);
        ChangeAlpha(_fadeImage, alpha);
    }

    //���l�̕ύX
    protected void ChangeAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    //true:�J�����̑J�ڂ���u�ōs��
    //false:�J�����̑J�ڂ����炩�ɍs��
    public virtual bool InstantaneousTransition()
    {
       return true;
       //return false;
    }
}


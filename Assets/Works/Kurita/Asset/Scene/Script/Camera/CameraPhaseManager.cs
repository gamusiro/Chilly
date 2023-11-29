using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using Cinemachine;

public class CameraPhaseManager : AbstractBasePhaseManager
{
    //�o�[�`�����J�����̐ݒ�
    [SerializeField] protected List<CinemachineVirtualCamera> _virtualCamera = new List<CinemachineVirtualCamera>();
    protected int _cameraIndex;

    //������
    private async void Start()
    {
        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //�J�����̑J�ڏ���
        while (_cameraIndex < _transTimeList.Count)
        {
            Debug.Log(_cameraIndex);
            await UniTask.Delay(TimeSpan.FromSeconds(_transTimeList[_cameraIndex]));
            NextCamera();
        }
    }

    //�J������؂�ւ���
    protected void NextCamera()
    {
        if (_cameraIndex + 1 >= _virtualCamera.Count) 
            return;

        _virtualCamera[_cameraIndex].Priority = 0;
        _cameraIndex++;
        _virtualCamera[_cameraIndex].Priority = 1;
    }

    public GameObject GetCurCamera()
    {
        return _virtualCamera[_cameraIndex].gameObject;
    }
}
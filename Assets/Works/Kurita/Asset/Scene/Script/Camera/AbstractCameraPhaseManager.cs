using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using Cinemachine;
using Cysharp.Threading.Tasks;

public class AbstractCameraPhaseManager : AbstractBasePhaseManager
{
    //�o�[�`�����J�����̐ݒ�
    [SerializeField] protected List<CinemachineVirtualCamera> _virtualCamera = new ();
    protected int _cameraIndex;

    //������
    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //�J�����̑J�ڏ���
        while (_cameraIndex < _transTimeList.Count)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_transTimeList[_cameraIndex]), cancellationToken: token);
            NextCamera();
        }
    }

    //�J������؂�ւ���
    public void NextCamera()
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

    public int GetCurrentCameraIndex()
    {
        return _cameraIndex;
    }

    public void Shake()
    {
        var impulseSource = GetComponent<CinemachineImpulseSource>();
        impulseSource.GenerateImpulse();
    }
}

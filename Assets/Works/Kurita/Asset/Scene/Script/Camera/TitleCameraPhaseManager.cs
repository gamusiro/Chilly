using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleCameraPhaseManager : AbstractCameraPhaseManager
{
    private bool _canUpdate;

    [SerializeField, CustomLabel("����")]
    PlayerInput _input;

    [SerializeField, CustomLabel("�t�F�[�h")]
    Fade _fade;

    private void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;
    }

    private void Update()
    {
        if (_fade.GetState() != Fade.STATE.NONE)
            return;

        if (GetCurrentCameraIndex() == 0)
        {
            if (_input.currentActionMap["Commit"].triggered)//B�{�^���ɂ���@��
            {
                CS_AudioManager.Instance.PlayAudio("Commit");
                NextCamera();
                _canUpdate = true;
            }
        }
    }

    //�J������؂�ւ���
    public new void NextCamera()
    {
        _virtualCamera[_cameraIndex].Priority = 0;
        _cameraIndex = 1 - _cameraIndex;
        _virtualCamera[_cameraIndex].Priority = 1;
    }

    public bool GetCanUpdate()
    {
        return _canUpdate;
    }
}
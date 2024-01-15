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

    private void Update()
    {
        if (GetCurrentCameraIndex() == 0)
        {
            if (_input.currentActionMap["Commit"].triggered)//B�{�^���ɂ���@��
            {
                NextCamera();
                _canUpdate = true;
            }
        }
        else if (GetCurrentCameraIndex() == 1)
        {
            if (_input.currentActionMap["Cancel"].triggered)//A�{�^���ɂ���@��
            {
                NextCamera();
                _canUpdate = false;
            }
        }
    }

    public bool GetCanUpdate()
    {
        return _canUpdate;
    }
}
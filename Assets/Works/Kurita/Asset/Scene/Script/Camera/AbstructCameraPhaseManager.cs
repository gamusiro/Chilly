using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstructCameraPhaseManager : AbstructBasePhaseManager
{
    //�o�[�`�����J�����̐ݒ�
    [SerializeField] protected List<CinemachineVirtualCamera> _virtualCamera = new List<CinemachineVirtualCamera>();
    protected int _cameraIndex;

    //�t�F�[�Y�̐ݒ�
    protected new enum Phase { Stay,ZoomIn,Max };
    protected new Phase _phaseIndex = Phase.Stay;

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch(_phaseIndex)
        {
            case Phase.Stay:
                if (NextPhase(50.0f))
                    NextCamera();
                break;
            case Phase.ZoomIn:
                if (NextPhase(1.0f))
                    NextCamera();
                break;
        }
    }

    //������
    protected new void Init()
    {
        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;
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

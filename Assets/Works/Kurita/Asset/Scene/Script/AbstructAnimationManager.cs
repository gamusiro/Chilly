using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstructAnimationManager : MonoBehaviour
{
    //�o�[�`�����J�����̐ݒ�
    [SerializeField] protected List<CinemachineVirtualCamera> _virtualCamera = new List<CinemachineVirtualCamera>();
    protected int _cameraIndex;

    //�t�F�[�Y�̐ݒ�
    protected enum Phase { Stay,ZoomIn,Max };
    protected Phase _phaseIndex = Phase.Stay;

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
    protected void Init()
    {
        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //�t�F�[�Y������������
        _phaseIndex = 0;
    }

    //n�b��Ɏ��̃t�F�[�Y�ɑJ��
    protected bool NextPhase(float transTime)
    {
        if (CS_AudioManager.Instance.TimeBGM < transTime)//�J�ڎ��ԂɒB���Ă��Ȃ���ΏI��
            return false;

        _phaseIndex++;
        return true;
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

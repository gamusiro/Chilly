using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovieCameraManager : MonoBehaviour
{
    [SerializeField] protected List<CinemachineVirtualCamera> _virtualCamera = new List<CinemachineVirtualCamera>();
    protected int _cameraIndex;

    protected enum Fase { Stay,ZoomIn,Max };
    protected Fase _phaseIndex;
    protected float _time;

    void Start()
    {
        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera){virtualCamera.Priority = 0;}
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //�t�F�[�Y������������
        _phaseIndex = 0;
        _time = 0.0f;
    }

    void Update()
    {
        switch(_phaseIndex)
        {
            case Fase.Stay:
                NextCamera();
                NextPhase(5f);
                break;
            case Fase.ZoomIn:
                NextCamera();
                NextPhase(1.0f);
                break;
        }

        _time += Time.deltaTime;
    }

    //�J������؂�ւ���
    private void NextCamera()
    {
        if (_cameraIndex + 1 >= _virtualCamera.Count) 
            return;

        _virtualCamera[_cameraIndex].Priority = 0;
        _cameraIndex++;
        _virtualCamera[_cameraIndex].Priority = 1;
    }

    //���̃t�F�[�Y�ɑJ��
    protected bool NextPhase(float transTime)
    {
        if (_time < transTime)//�J�ڎ��ԂɒB���Ă��Ȃ���ΏI��
            return false;

        _phaseIndex++;
        _time = 0.0f;
        return true;
    }
}

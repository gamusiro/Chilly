using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCameraPhaseManager : AbstructCameraPhaseManager
{
    //�t�F�[�Y�̐ݒ�
    protected new enum Phase { Stay, Up, Max };
    protected new Phase _phaseIndex = Phase.Stay;

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (_phaseIndex)
        {
            case Phase.Stay:
                if (NextPhase(1.0f))
                    NextCamera();
                break;
            case Phase.Up:
                if (NextPhase(1.0f))
                    NextCamera();
                break;
        }
    }

    //n�b��Ɏ��̃t�F�[�Y�ɑJ��
    protected bool NextPhase(float transTime)
    {
        // += Time.deltaTime; �X��ύX
        if (_time < transTime)//�J�ڎ��ԂɒB���Ă��Ȃ���ΏI��
            return false;

        return true;
    }
}

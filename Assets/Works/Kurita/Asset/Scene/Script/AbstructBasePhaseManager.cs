using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstructBasePhaseManager : MonoBehaviour
{
    //�t�F�[�Y�̐ݒ�
    protected enum Phase { Max };
    protected Phase _phaseIndex = Phase.Max;

    void Start()
    {
        Init();
    }

    void Update()
    {
        //switch(_phaseIndex)
        //{
        //    case Phase.Stay:
        //        if (NextPhase(50.0f))
        //            NextCamera();
        //        break;
        //    case Phase.ZoomIn:
        //        if (NextPhase(1.0f))
        //            NextCamera();
        //        break;
        //}
    }

    //������
    protected void Init()
    {
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
}

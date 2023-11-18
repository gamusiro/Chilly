using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstructBasePhaseManager : MonoBehaviour
{
    //�t�F�[�Y�̐ݒ�
    protected enum Phase { Max };
    private Phase _phaseIndex = Phase.Max;
    protected float _time;

    //������
    protected void Init()
    {
        //�t�F�[�Y������������
        _phaseIndex = 0;
        _time = 0.0f;
    }

    //n�b��Ɏ��̃t�F�[�Y�ɑJ��
    protected bool NextPhase(float transTime)
    {
        _time+=Time.deltaTime;
        if (_time < transTime)//�J�ڎ��ԂɒB���Ă��Ȃ���ΏI��
            return false;

        return true;
    }
}

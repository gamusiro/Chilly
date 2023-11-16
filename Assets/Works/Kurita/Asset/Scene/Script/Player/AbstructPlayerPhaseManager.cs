using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstructPlayerPhaseManager : AbstructBasePhaseManager
{
    //�t�F�[�Y�̐ݒ�
    protected new enum Phase { Max };
    protected new Phase _phaseIndex = Phase.Max;

    //������
    protected new void Init()
    {
        //�t�F�[�Y������������
        _phaseIndex = 0;
    }
}

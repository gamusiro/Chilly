using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstructMoviePhaseManager : AbstructBasePhaseManager
{
    //�t�F�[�Y�̐ݒ�
    protected new enum Phase { Max };
    protected new Phase _phaseIndex = Phase.Max;

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
    protected new void Init()
    {
        //�t�F�[�Y������������
        _phaseIndex = 0;
    }
}

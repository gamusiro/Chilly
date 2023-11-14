using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstructMoviePhaseManager : AbstructBasePhaseManager
{
    //フェーズの設定
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

    //初期化
    protected new void Init()
    {
        //フェーズを初期化する
        _phaseIndex = 0;
    }
}

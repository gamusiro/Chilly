using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstructPlayerPhaseManager : AbstructBasePhaseManager
{
    //フェーズの設定
    protected new enum Phase { Max };
    protected new Phase _phaseIndex = Phase.Max;

    //初期化
    protected new void Init()
    {
        //フェーズを初期化する
        _phaseIndex = 0;
    }
}

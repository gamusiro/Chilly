using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstructBasePhaseManager : MonoBehaviour
{
    //フェーズの設定
    protected enum Phase { Max };
    protected Phase _phaseIndex = Phase.Max;

    //初期化
    protected void Init()
    {
        //フェーズを初期化する
        _phaseIndex = 0;
    }

    //n秒後に次のフェーズに遷る
    protected bool NextPhase(float transTime)
    {
        if (CS_AudioManager.Instance.TimeBGM < transTime)//遷移時間に達していなければ終了
            return false;

        _phaseIndex++;
        return true;
    }
}

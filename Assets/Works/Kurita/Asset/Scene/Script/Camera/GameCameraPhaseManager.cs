using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCameraPhaseManager : AbstructCameraPhaseManager
{
    //フェーズの設定
    private int _phaseIndex = 0;

    // 時間の変更
    [SerializeField, CustomLabel("切り替え時間")]
    List<float> timeList;

    void Start()
    {
        Init();
        _phaseIndex = 0;
    }

    void Update()
    {
        if (_phaseIndex >= timeList.Count)
            return;

        if (NextPhase(timeList[_phaseIndex]))
        {
            NextCamera();
            CS_MoveController.GetObject("Player").GetComponent<CS_Player>().SetUsingCamera();
            _phaseIndex++;
        }
    }

    //n秒後に次のフェーズに遷る
    protected bool NextPhase(float transTime)
    {
        // += Time.deltaTime; 個々を変更
        if (CS_AudioManager.Instance.TimeBGM < transTime)//遷移時間に達していなければ終了
            return false;

        return true;
    }
}

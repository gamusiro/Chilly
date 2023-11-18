using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCameraPhaseManager : AbstructCameraPhaseManager
{
    //�t�F�[�Y�̐ݒ�
    private int _phaseIndex = 0;

    // ���Ԃ̕ύX
    [SerializeField, CustomLabel("�؂�ւ�����")]
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

    //n�b��Ɏ��̃t�F�[�Y�ɑJ��
    protected bool NextPhase(float transTime)
    {
        // += Time.deltaTime; �X��ύX
        if (CS_AudioManager.Instance.TimeBGM < transTime)//�J�ڎ��ԂɒB���Ă��Ȃ���ΏI��
            return false;

        return true;
    }
}

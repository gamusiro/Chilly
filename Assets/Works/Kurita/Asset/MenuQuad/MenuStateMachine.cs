using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuStateMachine : MenuStateMachineBase<MenuStateMachine>
{                               
    private void Start()
    {
        SetStageInfo();
        SetNextState(new MenuStateMachine.LeftTriangle(this), false);
    }

    private class LeftTriangle : MenuStateBase<MenuStateMachine>
    {
        public LeftTriangle(MenuStateMachine machine) : base(machine)
        {

        }

        public override void OnEnter()
        {
            ChangeRedColor((int)StateType.LeftTriangle);
        }

        public override void OnUpdate()
        {
            if (_canUpdate)
            {
                if (_input.currentActionMap["Right"].triggered)
                    machine.SetNextState(new MenuStateMachine.Play(machine));
                if (_input.currentActionMap["Down"].triggered)
                    machine.SetNextState(new MenuStateMachine.BGM(machine));

                if (_input.currentActionMap["Left"].triggered
                    || _input.currentActionMap["Commit"].triggered)
                    NextStage(false);
            }
        }

        public override void OnExit()
        {
            ChangeBlueColor((int)StateType.LeftTriangle);
        }
    }

    private class RightTriangle : MenuStateBase<MenuStateMachine>
    {
        public RightTriangle(MenuStateMachine machine) : base(machine)
        { 

        }

        public override void OnEnter()
        {
            ChangeRedColor((int)StateType.RightTriangle);
        }

        public override void OnUpdate()
        {
            if (_canUpdate)
            {
                if (_input.currentActionMap["Left"].triggered)
                    machine.SetNextState(new MenuStateMachine.Play(machine));
                if (_input.currentActionMap["Down"].triggered)
                    machine.SetNextState(new MenuStateMachine.BGM(machine));

                if (_input.currentActionMap["Right"].triggered
                    || _input.currentActionMap["Commit"].triggered)
                    NextStage(true);
            }
        }

        public override void OnExit()
        {
            ChangeBlueColor((int)StateType.RightTriangle);
        }
    }

    private class Play : MenuStateBase<MenuStateMachine>
    {
        public Play(MenuStateMachine machine) : base(machine)
        {

        }

        public override void OnEnter()
        {
            ChangeRedColor((int)StateType.Play);
        }

        public override void OnUpdate()
        {
            if (_canUpdate )
            {
                if (_input.currentActionMap["Left"].triggered)
                    machine.SetNextState(new MenuStateMachine.LeftTriangle(machine));
                if (_input.currentActionMap["Right"].triggered)
                    machine.SetNextState(new MenuStateMachine.RightTriangle(machine));
                if (_input.currentActionMap["Down"].triggered)
                    machine.SetNextState(new MenuStateMachine.BGM(machine));

                // �V�[���J��
                if (_input.currentActionMap["Commit"].triggered)
                {
                    CS_AudioManager.Instance.PlayAudio("Commit");

                    // �ǂݍ��ރm�[�c�t�H���_���w��
                    StageInfo stageInfo = GetSelectStage();
                    CS_LoadNotesFile.SetFolderName(stageInfo.AudioName);

                    // �`���[�g���A��
                    CS_GameManager.SetTutorial(false);

                    // �I�[�v�j���O�V�[�����K�v�Ȃ���
                    machine.SetFadeOut(1.0f,
                        () =>
                        {
                            CS_AudioManager.Instance.MasterVolume = 0.0f;
                            CS_AudioManager.Instance.StopBGM();
                            SceneManager.LoadScene(stageInfo.NextSceneName);
                        });
                }
            }
        }

        public override void OnExit()
        {
            ChangeBlueColor((int)StateType.Play);
        }
    }

    private class BGM : MenuStateBase<MenuStateMachine>
    {
        const float amount = 0.1f;

        public BGM(MenuStateMachine machine) : base(machine)
        {
        }

        public override void OnEnter()
        {
            ChangeRedColor((int)StateType.BGM);
        }

        public override void OnUpdate()
        {
            if (_canUpdate)
            {
                if (_input.currentActionMap["Up"].triggered)
                    machine.SetNextState(new MenuStateMachine.Play(machine));
                if (_input.currentActionMap["Down"].triggered)
                    machine.SetNextState(new MenuStateMachine.SE(machine));


                if (_input.currentActionMap["Left"].triggered)
                    CS_AudioManager.Instance.BGMVolume -= amount;

                if (_input.currentActionMap["Right"].triggered)
                    CS_AudioManager.Instance.BGMVolume += amount;

                machine.SetBGMVolume(CS_AudioManager.Instance.BGMVolume);
            }
        }

        public override void OnExit()
        {
            ChangeBlueColor((int)StateType.BGM);
        }
    }

    private class SE : MenuStateBase<MenuStateMachine>
    {
        const float amount = 0.1f;

        public SE(MenuStateMachine machine) : base(machine)
        {

        }

        public override void OnEnter()
        {
            ChangeRedColor((int)StateType.SE);
        }

        public override void OnUpdate()
        {
            if (_canUpdate)
            {
                if (_input.currentActionMap["Up"].triggered)
                    machine.SetNextState(new MenuStateMachine.BGM(machine));

                if (_input.currentActionMap["Left"].triggered)
                    CS_AudioManager.Instance.SEVolume -= amount;

                if (_input.currentActionMap["Right"].triggered)
                    CS_AudioManager.Instance.SEVolume += amount;

                machine.SetSEVolume(CS_AudioManager.Instance.SEVolume);
            }
        }

        public override void OnExit()
        {
            ChangeBlueColor((int)StateType.SE);
        }
    }   
}



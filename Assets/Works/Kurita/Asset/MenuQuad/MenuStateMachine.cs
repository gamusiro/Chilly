using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuStateMachine : MenuStateMachineBase<MenuStateMachine>
{    
    public void SetMenuAudio()
    {
        StageInfo info = _stageInfoList[_currentState.StageIndex];
        CS_AudioManager.Instance.PlayAudioAndFadeBeteenTime(info.AudioName, info.StartTime, info.EndTime);
    }


    private void Start()
    {
        SetStageInfo();
        SetNextState(new MenuStateMachine.LeftTriangle(this), false);
        SetBGMVolume(CS_AudioManager.Instance.BGMVolume);
        SetSEVolume(CS_AudioManager.Instance.SEVolume);
        _currentState.NextStage(_currentState.StageIndex);
        _currentState.QuitUIOn(false);
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

                if (_input.currentActionMap["Cancel"].triggered)
                {
                    QuitUIOn(true);
                    machine.SetNextState(new MenuStateMachine.Exit_Yes(machine));
                }
                    

                if (_input.currentActionMap["Left"].triggered
                    || _input.currentActionMap["Commit"].triggered)
                    NextStage(_stageIndex - 1);
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

                if (_input.currentActionMap["Cancel"].triggered)
                {
                    QuitUIOn(true);
                    machine.SetNextState(new MenuStateMachine.Exit_Yes(machine));
                }

                if (_input.currentActionMap["Right"].triggered
                    || _input.currentActionMap["Commit"].triggered)
                    NextStage(_stageIndex + 1);
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

                if (_input.currentActionMap["Cancel"].triggered)
                {
                    QuitUIOn(true);
                    machine.SetNextState(new MenuStateMachine.Exit_Yes(machine));
                }

                // シーン遷移
                if (_input.currentActionMap["Commit"].triggered)
                {
                    CS_AudioManager.Instance.PlayAudio("Commit");

                    // 読み込むノーツフォルダを指定
                    StageInfo stageInfo = GetSelectStage();
                    CS_LoadNotesFile.SetFolderName(stageInfo.AudioName);

                    // チュートリアル
                    if(stageInfo.AudioName == "WeMadeIt")
                        CS_GameManager.SetTutorial(true);
                    else
                        CS_GameManager.SetTutorial(false);

                    // オープニングシーンが必要なもの
                    machine.SetFadeOut(1.0f,
                        () =>
                        {
                            CS_AudioManager.Instance.MasterVolume = 0.0f;
                            CS_AudioManager.Instance.StopBGM();
                            SceneManager.LoadScene("Re_Opening");
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

                if (_input.currentActionMap["Cancel"].triggered)
                {
                    QuitUIOn(true);
                    machine.SetNextState(new MenuStateMachine.Exit_Yes(machine));
                }

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

                if (_input.currentActionMap["Cancel"].triggered)
                {
                    QuitUIOn(true);
                    machine.SetNextState(new MenuStateMachine.Exit_Yes(machine));
                }

                machine.SetSEVolume(CS_AudioManager.Instance.SEVolume);
            }
        }

        public override void OnExit()
        {
            ChangeBlueColor((int)StateType.SE);
        }
    }

    private class Exit_Yes : MenuStateBase<MenuStateMachine>
    {
        public Exit_Yes(MenuStateMachine machine) : base(machine)
        {

        }

        public override void OnEnter()
        {
            ChangeRedColor((int)StateType.Exit_Yes);
        }

        public override void OnUpdate()
        {
            if (_canUpdate)
            {
                if (_input.currentActionMap["Left"].triggered 
                    || _input.currentActionMap["Right"].triggered)
                    machine.SetNextState(new MenuStateMachine.Exit_No(machine));

                if (_input.currentActionMap["Commit"].triggered)
                    Application.Quit();

                if (_input.currentActionMap["Cancel"].triggered)
                {
                    QuitUIOn(false);
                    machine.SetNextState(new MenuStateMachine.LeftTriangle(machine));
                }
            }
        }

        public override void OnExit()
        {
            ChangeBlueColor((int)StateType.Exit_Yes);
        }
    }

    private class Exit_No : MenuStateBase<MenuStateMachine>
    {
        public Exit_No(MenuStateMachine machine) : base(machine)
        {

        }

        public override void OnEnter()
        {
            ChangeRedColor((int)StateType.Exit_No);
        }

        public override void OnUpdate()
        {
            if (_canUpdate)
            {
                if (_input.currentActionMap["Left"].triggered
                    || _input.currentActionMap["Right"].triggered)
                    machine.SetNextState(new MenuStateMachine.Exit_Yes(machine));

                if (_input.currentActionMap["Cancel"].triggered
                    || _input.currentActionMap["Commit"].triggered)
                {
                    QuitUIOn(false);
                    machine.SetNextState(new MenuStateMachine.LeftTriangle(machine));
                }
            }
        }

        public override void OnExit()
        {
            ChangeBlueColor((int)StateType.Exit_No);
        }
    }
}



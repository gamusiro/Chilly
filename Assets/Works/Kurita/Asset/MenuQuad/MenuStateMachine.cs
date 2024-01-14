using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuStateMachine : MenuStateMachineBase<MenuStateMachine>
{                               
    private void Start()
    {
        SetNextState(new MenuStateMachine.LeftTriangle(this));
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
            if (_canUpdate)
            {
                if (_input.currentActionMap["Left"].triggered)
                    machine.SetNextState(new MenuStateMachine.LeftTriangle(machine));
                if (_input.currentActionMap["Right"].triggered)
                    machine.SetNextState(new MenuStateMachine.RightTriangle(machine));
                if (_input.currentActionMap["Down"].triggered)
                    machine.SetNextState(new MenuStateMachine.BGM(machine));

                // シーン遷移
                if (_input.currentActionMap["Commit"].triggered)
                {
                    // 読み込むノーツフォルダを指定
                    StageInfo stageInfo = GetSelectStage();
                    CS_LoadNotesFile.SetFolderName(stageInfo.AudioName);

                    // チュートリアル
                    CS_GameManager.SetTutorial(false);

                    // オープニングシーンが必要なもの
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
                    machine.SetBGMVolume(0.0f);

                if (_input.currentActionMap["Right"].triggered)
                    machine.SetBGMVolume(1.0f);

                if (Input.GetKeyDown(KeyCode.X))
                    machine.SetBGMVolume(0.5f);
            }
        }

        public override void OnExit()
        {
            ChangeBlueColor((int)StateType.BGM);
        }
    }

    private class SE : MenuStateBase<MenuStateMachine>
    {
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
                    machine.SetSEVolume(0.0f);

                if (_input.currentActionMap["Right"].triggered)
                    machine.SetSEVolume(1.0f);

                if (Input.GetKeyDown(KeyCode.X))
                    machine.SetSEVolume(0.5f);
            }
        }

        public override void OnExit()
        {
            ChangeBlueColor((int)StateType.SE);
        }
    }   
}



using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;


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
                if (Input.GetKeyDown(KeyCode.D))
                    machine.SetNextState(new MenuStateMachine.Play(machine));
                if (Input.GetKeyDown(KeyCode.S))
                    machine.SetNextState(new MenuStateMachine.BGM(machine));

                if (Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.Return))
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
                if (Input.GetKeyDown(KeyCode.A))
                    machine.SetNextState(new MenuStateMachine.Play(machine));
                if (Input.GetKeyDown(KeyCode.S))
                    machine.SetNextState(new MenuStateMachine.BGM(machine));

                if (Input.GetKeyDown(KeyCode.D) ||
                    Input.GetKeyDown(KeyCode.Return))
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
                if (Input.GetKeyDown(KeyCode.A))
                    machine.SetNextState(new MenuStateMachine.LeftTriangle(machine));
                if (Input.GetKeyDown(KeyCode.D))
                    machine.SetNextState(new MenuStateMachine.RightTriangle(machine));
                if (Input.GetKeyDown(KeyCode.S))
                    machine.SetNextState(new MenuStateMachine.BGM(machine));
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
                if (Input.GetKeyDown(KeyCode.W))
                    machine.SetNextState(new MenuStateMachine.Play(machine));
                if (Input.GetKeyDown(KeyCode.S))
                    machine.SetNextState(new MenuStateMachine.SE(machine));


                if (Input.GetKeyDown(KeyCode.A))
                    machine.SetBGMVolume(0.0f);

                if (Input.GetKeyDown(KeyCode.D))
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
                if (Input.GetKeyDown(KeyCode.W))
                    machine.SetNextState(new MenuStateMachine.BGM(machine));

                if (Input.GetKeyDown(KeyCode.A))
                    machine.SetSEVolume(0.0f);

                if (Input.GetKeyDown(KeyCode.D))
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



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{
    public class PlayerGroundedState : State{


        // protected bool IsMove;

        public PlayerGroundedState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override StateType GetId()
        {
            return StateType.Ground;
        }

        public override void LogicUpdate()
        {
        }



        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


        }

}

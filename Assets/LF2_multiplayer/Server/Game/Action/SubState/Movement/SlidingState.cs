using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  LF2.Server{

    public class SlidingState : State
    {
        private float _runSpeed;
        private float _gainDecreaseRunSpeed;

        public SlidingState(PlayerStateMachine player) : base(player)
        {
            _runSpeed = player.serverplayer.NetState.CharacterClass.Speed;
            _gainDecreaseRunSpeed = 4f;
        }

        //     _runSpeed =playerData.runVelocity;
        // _gainDecreaseRunSpeed = playerData.GainDecreaseRunSpeed;

        public override void CanChangeState(StateRequestData actionRequestData){
            
        }

        public override void LogicUpdate()
        {
            _runSpeed -= _runSpeed*Time.deltaTime*_gainDecreaseRunSpeed;
            
            if (_runSpeed <= 0.1f ){
                player.ChangeState(StateType.Idle);
            }
        }
        public override void Enter()
        {
            base.Enter();
            // ko cho nhay lan thu 2 khi Run
            m_Data.StateTypeEnum = StateType.Sliding;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();
            ResetRunVelocity();
        }

        public void ResetRunVelocity(){
            _runSpeed = player.serverplayer.NetState.CharacterClass.Speed;
        }

        public override StateType GetId(){
            return StateType.Sliding;
        }


    }
}
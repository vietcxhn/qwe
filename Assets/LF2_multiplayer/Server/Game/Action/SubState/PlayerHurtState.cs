using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 namespace LF2.Server{

    public class PlayerHurtState : State
    {

        private int m_nbHurt; 

        public PlayerHurtState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            m_nbHurt += 1;
            // if (m_nbHurt){

            // }
            m_Data.StateTypeEnum = StateType.Hurt;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);

        }


        public override void Exit()
        {
            base.Exit();
        }



        public override void LogicUpdate()
        {
            base.LogicUpdate();

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void End()
        {
            player.ChangeState(StateType.Idle);

            // if ( Time.time > startTime + 0.3f){
            //     stateMachine.ChangeState(player.IdleState);
            // }
        }

        public override StateType GetId()
        {
            return StateType.Hurt;
        }

    }
 }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server
{
    public class PlayerDoubleJumpState : PlayerAirState
    {
        public PlayerDoubleJumpState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
        }

        public override StateType GetId()
        {
            return StateType.DoubleJump;
        }

        public override void Enter()
        {
            base.Enter();
            
            //
            m_Data.StateTypeEnum = StateType.DoubleJump;
            // player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
            //
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{


    public class PlayerAttackJump1 : PlayerAirState
    {
        public PlayerAttackJump1(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
        }

        public override void Enter()
        {
            base.Enter();
  
            m_Data.StateTypeEnum = StateType.AttackJump1;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override StateType GetId()
        {
            return StateType.AttackJump1;
        }
    }


}

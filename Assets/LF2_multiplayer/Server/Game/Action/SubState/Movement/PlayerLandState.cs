using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{
    public class PlayerLandState : State
    {

        public PlayerLandState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
            // Attack , Jump , Defense 
            // DDA , DUA  
            if (actionRequestData.StateTypeEnum == StateType.Jump){
                // player.ServerCharacterMovement.SetDoubleJump(moveDir);
                player.ChangeState(StateType.DoubleJump);
            }

        }

        public override void Enter()
        {
            base.Enter();
            m_Data.StateTypeEnum = StateType.Land;
            // player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
        }

        public override StateType GetId()
        {
            return StateType.Land;
        }

        public override void End()
        {
            base.Exit();
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{

    //In this State :  Player Stand Still  , do nothing , wait to request. 
    //                  Can do some initilize (for Exemple reset number Jump)
    public class PlayerIdleState : State
    {
        public PlayerIdleState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
            // if (actionRequestData.StateTypeEnum == StateType.Jump){
            //     player.ServerCharacterMovement.SetJump(Vector3.zero);
            // }
            // if we are Idle wanna jump so , Jump up 
            player.ChangeState(actionRequestData.StateTypeEnum);
            
        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
            if (IsMove){
                player.ChangeState(StateType.Move);
            }
        }


        public override void Enter()
        {
            base.Enter();
            // player.ServerCharacterMovement.CancelMove();
            m_Data.StateTypeEnum = StateType.Idle;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);

        }




        public override StateType GetId()
        {
            return StateType.Idle;
        }




    }
}


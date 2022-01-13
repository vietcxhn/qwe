using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    public class PlayerMoveStateFX : StateFX
    {
        public PlayerMoveStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {
            if (data.StateTypeEnum == StateType.Attack || data.StateTypeEnum == StateType.Jump ){
                Anticipated = true;
                MPlayerMachineFX.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum);
            }
            else if (data.StateTypeEnum == StateType.Run)
            {
                MPlayerMachineFX.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum);
            }
        }

 
        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
            if (!IsMove){
                MPlayerMachineFX.ChangeState(new PlayerIdleStateFX(MPlayerMachineFX));
            }
        }

        

        public override void Enter()
        {
            // if( !Anticipated)
            // {
                // PlayAnim(MPlayerMachineFX.CurrentStateViz.GetId());
            // }
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Blend Tree");
            MPlayerMachineFX.m_ClientVisual.OurAnimator.SetFloat("Speed", 1);
            base.Enter();
        }
        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            // MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Walk_anim");
        }

        public override StateType GetId()
        {
            return StateType.Move;
        }



        public override bool LogicUpdate()
        {
            MPlayerMachineFX.m_ClientVisual.coreMovement.SetVelocityXZ(MPlayerMachineFX.moveDir);
            return true;
        }
    }
}


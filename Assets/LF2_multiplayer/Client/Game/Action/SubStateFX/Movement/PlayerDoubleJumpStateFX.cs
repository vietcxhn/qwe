using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerDoubleJumpStateFX : PlayerAirStateFX
    {

        public PlayerDoubleJumpStateFX(PlayerStateMachineFX mPlayerMachineFX ) : base(mPlayerMachineFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {
        }

        public override void Enter()
        {
            
            if( !Anticipated)
            {
                PlayAnim(MPlayerMachineFX.CurrentStateViz.GetId());
            }
            base.Enter();
         }

        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("DoubleJump_anim");
        }

        
        public override void End(){
            base.End();
        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }


        public override StateType GetId()
        {
            return StateType.DoubleJump;
        }

        public override bool LogicUpdate() {
            return base.LogicUpdate();
        }

    }
}
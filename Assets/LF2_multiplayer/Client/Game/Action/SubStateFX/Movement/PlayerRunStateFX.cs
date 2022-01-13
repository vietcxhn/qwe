using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerRunStateFX : StateFX
    {
        private float runVelocity ;

        public PlayerRunStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }


        public override void AnticipateState(ref StateRequestData requestData)
        {

            if (requestData.StateTypeEnum == StateType.Jump){
                MPlayerMachineFX.ChangeState(new PlayerDoubleJumpStateFX(MPlayerMachineFX));
            }
            else if (requestData.StateTypeEnum == StateType.Defense){
                // MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Rolling_anim");

                MPlayerMachineFX.ChangeState(new PlayerRollingStateFX(MPlayerMachineFX));
            }
            else if (requestData.StateTypeEnum == StateType.Attack)
            {
                MPlayerMachineFX.ChangeState(new PlayerAttackStateFX(MPlayerMachineFX));
            }           
        }


        public override void Enter()
        {
            base.Enter();
            // ko cho nhay lan thu 2 khi Run
            // player.JumpState.DecreaseAmountOfJumpsLeft();
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override StateType GetId()
        {
            return StateType.Run;
        }



        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
            //Debug.Log(position);
            //Debug.Log(moveDir.z);
        }



        public override void OnAnimEvent(string id)
        {
            base.OnAnimEvent(id);
        }

        public override void PlayAnim(StateType currentState, int nbAniamtion = 0)
        {
            
            base.PlayAnim(currentState, nbAniamtion);
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Run_anim");
        }




        public override bool LogicUpdate()
        {
            MPlayerMachineFX.m_ClientVisual.coreMovement.SetRun(2f);
            if ( MPlayerMachineFX.moveDir.z >0.9f || MPlayerMachineFX.moveDir.z < -0.9f  ){
                MPlayerMachineFX.ChangeState(new SlidingStateFX(MPlayerMachineFX));
            }

            return true;
        }
    }
}

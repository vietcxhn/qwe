using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    public class PlayerIdleStateFX : StateFX
    {
        public PlayerIdleStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {
            if (data.StateTypeEnum == StateType.Jump){
                MPlayerMachineFX.m_ClientVisual.coreMovement.SetJump(Vector3.zero);
                MPlayerMachineFX.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum);

            }

            if (data.NbAnimation > 0 ){
                MPlayerMachineFX.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum, data.NbAnimation);
            }else{
                MPlayerMachineFX.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum);
            }
            // else if (data.StateTypeEnum == StateType.Attack){
            //     m_PlayerFX.stateMachineViz.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum, data.NbAnimation);
            // }
            // else{
            //     m_PlayerFX.stateMachineViz.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum);
            // }
        }


        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
            if (IsMove){
                // m_PlayerFX.m_ClientVisual.OurAnimator.Play("Walk_anim");
                MPlayerMachineFX.ChangeState(new PlayerMoveStateFX(MPlayerMachineFX));
            }
        }



        public override void Enter()
        {
            // if(!Anticipated)
            // {
            //     PlayAnim(MPlayerMachineFX.CurrentStateViz.GetId());  
            //     
            // }
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Blend Tree");
            MPlayerMachineFX.m_ClientVisual.OurAnimator.SetFloat("Speed", 0);
            base.Enter();
        }

        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            // MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Idle_anim");
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override StateType GetId()
        {
            return StateType.Idle;
        }



        public override bool LogicUpdate()
        {
            // base.LogicUpdate();
            // if  (player.InputHandler.canRun ){
            //     player.InputHandler.ResetRun();
            //     stateMachine.ChangeState(player.RunState);
            // }
            // else if (IsMove){
            //     stateMachine.ChangeState(player.MoveState);
            // }
            // else if (JumpInput){
            //     player.InputHandler.UseJumpInput();
            //     core.SetMovement.SetVelocityJump(playerData.jumpVelocity ,moveDir);
            //     stateMachine.ChangeState(player.JumpState);
            // }
            // else if (player.InputHandler.AttackInput && !isAttack ){
            //     // Error : Player stuck in State Attack , but animation is IDLE  
            //     stateMachine.ChangeState(player.AttackState12 , AttackType.Attack1);
            //     lastTimeAttack = Time.deltaTime;
            // }
            // else if (player.InputHandler.DefenseInput){
            //     stateMachine.ChangeState(player.DefenseState);
            // }

            Debug.Log("IdleStateVisual");
            return true;


        }





    }
}


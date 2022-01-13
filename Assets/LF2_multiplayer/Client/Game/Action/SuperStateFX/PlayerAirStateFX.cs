using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerAirStateFX : StateFX
    {
        private int amountOfJumpLeft ;

        public PlayerAirStateFX(PlayerStateMachineFX mPlayerMachineFX ) : base(mPlayerMachineFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public bool CanJump(){
            if (amountOfJumpLeft > 0){
                return true;
            }else return false;
        }

        
        public override bool LogicUpdate() {

            // Debug.Log("AirState");
            MPlayerMachineFX.m_ClientVisual.coreMovement.CheckIfShouldFlip((int)moveDir.x);
            if (MPlayerMachineFX.m_ClientVisual.coreMovement.IsGounded() && Time.time - TimeStarted > 0.3f ){
                return false;
            }
            return true;
        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }

        public override void End()
        {
            MPlayerMachineFX.ChangeState(new PlayerLandStateFX(MPlayerMachineFX));
        }


        public void DecreaseAmountOfJumpsLeft()=>amountOfJumpLeft--;

        public override StateType GetId()
        {
            return StateType.Air;
        }


        

    }
}
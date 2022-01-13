using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    public class PlayerRollingStateFX : StateFX
    {
        float rollingSpeed;
        float distanceRolling;
        private float distanceRollingTravelled;

        public PlayerRollingStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }

        public override void AnticipateState(ref StateRequestData requestData)
        {
            base.AnticipateState(ref requestData);
        }


        public override void End()
        {
            base.End();
        }

        public override void Enter()
        {
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Rolling_anim");
            base.Enter();
        }







        public override StateType GetId()
    {
        return StateType.Rolling;
    }

        public override bool LogicUpdate()
        {
            MPlayerMachineFX.m_ClientVisual.coreMovement.SetRun(1f);
            return true;
        }

        public override void OnAnimEvent(string id)
        {
            base.OnAnimEvent(id);
        }

        public override void PlayAnim(StateType currentState, int nbAniamtion = 0)
        {
            base.PlayAnim(currentState, nbAniamtion);
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Rolling_anim");


        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }
        
    }
}

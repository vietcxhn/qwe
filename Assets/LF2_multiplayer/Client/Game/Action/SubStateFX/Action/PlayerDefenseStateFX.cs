using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    

    public class PlayerDefenseStateFX : StateFX
    {

        public PlayerDefenseStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                PlayAnim(MPlayerMachineFX.CurrentStateViz.GetId());
            }
            base.Enter();
        }


        public override StateType GetId()
        {
            return StateType.Defense;
        }

        public override bool LogicUpdate()
        {
            Debug.Log("Defense Visual");
            return true;
        }


        public override void End(){
            MPlayerMachineFX.idle();
        }


        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            Debug.Log("Defense_anim");
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Defense_anim");
        }
    }
}

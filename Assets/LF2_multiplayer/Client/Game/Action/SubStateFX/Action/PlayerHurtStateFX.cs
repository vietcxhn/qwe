using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace LF2.Visual{

    public class PlayerHurtStateFX : StateFX
    {
        public PlayerHurtStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }

        public override void Enter()
        {
            if( !Anticipated)
            {
                PlayAnim(MPlayerMachineFX.CurrentStateViz.GetId() , Data.NbAnimation) ;
            }            
            base.Enter();
            if (Data.Direction != Vector3.zero){
                MPlayerMachineFX.m_ClientVisual.coreMovement.SetJump(new Vector3(0,0.5f,0));
            }
        }


        public override void Exit()
        {
            base.Exit();
        }


        public override bool LogicUpdate()
        {
            return true;
        }


        public override void End()
        {
            MPlayerMachineFX.idle();
        }

        public override void PlayAnim(StateType currentState , int nbanim )
        {
            base.PlayAnim(currentState,nbanim);
            // Debug.Log(" AnimationAttack");
            Debug.Log(nbanim);
            // m_PlayerFX.m_ClientVisual.OurAnimator.Play("Hurt1_anim");

            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Hurt1_anim");

            // switch (nbanim){
            //     case 1 : 
            //         MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Hurt1_anim");
            //         break;
            //     case 2 : 
            //         MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Hurt2_anim");
            //         break;
            //     case 3 : 
            //         MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Attack3_anim");
            //         break;
            //
            // } 
        }

        public override StateType GetId()
        {
            return StateType.Hurt;
        }


    }
 }

using Unity.Netcode;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    

    public class PlayerAttackStateFX : StateFX
    {
        // private List<IDamageable> dectectedDamageable = new List<IDamageable>();
        // Transform attackTransform ;
        float attack12distance;
        private bool m_ImpactPlayed;

        private Client.ClientCharacter clientChar;

        private SO_AttackDetails attackDetails;


        public PlayerAttackStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {

        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                PlayAnim(MPlayerMachineFX.CurrentStateViz.GetId() , Data.NbAnimation);
            }
            base.Enter();
            PlayHitReact();
        }


        public override StateType GetId()
        {
            return StateType.Attack;
        }

        public override bool LogicUpdate()
        {
           // Debug.Log("Attack Visual");
            return true;
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override void End(){
            MPlayerMachineFX.idle();
        }


        public override void PlayAnim(StateType currentState , int nbanim )
        {
            base.PlayAnim(currentState,nbanim);

            // DownCasting data 

            if (MPlayerMachineFX.SkillDescription(StateType.Attack).GetType() == typeof(SO_AttackDetails)){
                attackDetails =(SO_AttackDetails)MPlayerMachineFX.SkillDescription(StateType.Attack);
            }

            if (attackDetails.AttackDetails.Length < 3 && nbanim == 3 ){
                nbanim = 1;
            // Debug.Log(nbanim);
            }
            switch (nbanim){
                default : 
                    MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Attack1_anim");
                    break;
                case 2 : 
                    MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Attack2_anim");
                    break;
                case 3 : 
                    MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("AttackRun_anim");
                   

                    break;
            } 
            // Debug.Log(" AnimationAttack");

           
        }



        private void PlayHitReact()
        {
            // if (m_ImpactPlayed) { return; }
            // m_ImpactPlayed = true;
            // Debug.Log("PlayHitReact");
            // Debug.Log(Data);
            //Is my original target still in range? Then definitely get him!
            if (Data.TargetIds != null && 
                Data.TargetIds.Length > 0 && 
                NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(Data.TargetIds[0], out var targetNetworkObj)
                && targetNetworkObj != null)
            {
                if (targetNetworkObj.NetworkObjectId != MPlayerMachineFX.m_ClientVisual.NetworkObjectId)
                {
                    // string hitAnim = Description.ReactAnim;
                    // if(string.IsNullOrEmpty(hitAnim)) { hitAnim = k_DefaultHitReact; }
                    clientChar = targetNetworkObj.GetComponent<Client.ClientCharacter>();
                    // Debug.Log(originalTarget);

                    if (clientChar && clientChar.ChildVizObject && clientChar.ChildVizObject.OurAnimator)
                    {
                        // Dont have Owner Ship to call serverRPC (can ignore but dont know further) 
                        StateRequestData m_data = new StateRequestData();
                        m_data.StateTypeEnum = StateType.Hurt;
                        m_data.NbAnimation = Data.NbAnimation;
                        // Debug.Log(Data.Direction);
                        // if (Data.Direction != null){
                        //     m_data.Direction = Data.Direction;
                        // } 
                        // clientChar.ChildVizObject.m_NetState.DoPassiveActionServerRPC(m_data);
                        // clientChar.ChildVizObject.m_statePlayerViz.stateMachineViz.ChangeState(StateType.Hurt);
                        
                        //clientChar.ChildVizObject.MStateMachinePlayerViz.AnticipateState(ref m_data);

                        // clientChar.ChildVizObject.OurAnimator.Play("Hurt1_anim");

                    }
                }

            }

            //in the future we may do another physics check to handle the case where a target "ran under our weapon".
            //But for now, if the original target is no longer present, then we just don't play our hit react on anything.
        }
    }
}

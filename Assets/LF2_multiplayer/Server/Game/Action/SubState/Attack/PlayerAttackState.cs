using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace LF2.Server{
    //in this State :  Player Actack  , can change to desied State follow some request. 
    //                 It is not explicitly targeted (so can attack to all time ), but rather detects the foe (enemy ) that was hit with a physics check.
    public class PlayerAttackState : State
    {
        // private List<IDamageable> dectectedDamageable = new List<IDamageable>();
        // Transform attackTransform ;
        bool m_ExecutionFired;
        float m_MaxDistance = 0.35f;
        private static RaycastHit[] s_Hits = new RaycastHit[4];


        private ulong m_ProvisionalTarget;

        public PlayerAttackState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {

        }

        public override void Enter()
        {      
            base.Enter();

            
            // m_Data.StateTypeEnum = StateType.Attack;

            IDamageable foe = DetectFoe();

            if (foe != null)
            {
                // fill data to send 
                Debug.Log(foe);
                m_ProvisionalTarget = foe.NetworkObjectId;
                m_Data.TargetIds = new ulong[] { foe.NetworkObjectId };
                if (m_Data.NbAnimation == 3 ){
                    m_Data.Direction = player.ServerCharacterMovement.FacingDirection*new Vector3(0,0.5f,0);
                }
            }
            
            // Debug.Log(m_Data.NbAnimation);
            // player.serverplayer.serverAnimationHandler.NetworkAnimator.SetTrigger(Description.Anim);
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
        }


        public override StateType GetId()
        {
            return StateType.Attack;
        }

        public override void PhysicsUpdate()
        {
            // Debug.Log("AttackState");
            if (!m_ExecutionFired)
            {
                m_ExecutionFired = true;
                var foe = DetectFoe(m_ProvisionalTarget);
                if (foe != null)
                {
                    
                    // this.player.stateMachine.ChangeState(StateType.Hurt);
                    // player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
                    // m_attackcombo += 1 ; 
                    SkillsDescription value ;
                    var found = player.m_CharacterSkillsDescription.SkillDataByType.TryGetValue(StateType.Attack , out value);
                    Debug.AssertFormat(found, "Tried to find StateType %s but it was missing from GameDataSource!", StateType.Attack);
                    foe.ReceiveHP(this.player.serverplayer, -value.Amount);
                }
            }
        }

        public override void End()
        {
            player.ChangeState(StateType.Idle);
            m_ExecutionFired = false;
        }

        public override void Exit()
        {
            m_ExecutionFired = false;
        }

        /// <summary>
        /// Returns the ServerCharacter of the foe we hit, or null if none found.
        /// </summary>
        /// <returns></returns>
        private IDamageable DetectFoe(ulong foeHint = 0)
        {
            return GetIdealMeleeFoe(player.serverplayer.IsNpc,player.serverplayer.physicsWrapper.DamageCollider, m_MaxDistance, foeHint);
        }
            
            
        /// <summary>
        /// Utility used by Actions to perform Melee attacks. Performs a melee hit-test
        /// and then looks through the results to find an alive target, preferring the provided
        /// enemy.
        /// </summary>
        /// <param name="isNPC">true if the attacker is an NPC (and therefore should hit PCs). False for the reverse.</param>
        /// <param name="ourCollider">The collider of the attacking GameObject.</param>
        /// <param name="meleeRange">The range in meters to check for foes.</param>
        /// <param name="preferredTargetNetworkId">The NetworkObjectId of our preferred foe, or 0 if no preference</param>
        /// <returns>ideal target's IDamageable, or null if no valid target found</returns>
        public  IDamageable GetIdealMeleeFoe(bool isNPC, Collider ourCollider, float meleeRange, ulong preferredTargetNetworkId)
        {
            RaycastHit[] results;
            int numResults = StateUtils.DetectMeleeFoe(isNPC, ourCollider, meleeRange, out results);

            IDamageable foundFoe = null;

            //everything that got hit by the raycast should have an IDamageable component, so we can retrieve that and see if they're appropriate targets.
            //we always prefer the hinted foe. If he's still in range, he should take the damage, because he's who the client visualization
            //system will play the hit-react on (in case there's any ambiguity).
            for (int i = 0; i < numResults; i++)
            {
                var damageable = results[i].collider.GetComponent<IDamageable>();
                if (damageable != null && damageable.IsDamageable() && damageable.NetworkObjectId != player.serverplayer.NetworkObjectId)
                {
                    foundFoe = damageable;
                }
                
            }

            return foundFoe;
        }




    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{

    public class PlayerDUAState : State
    {
        // private List<IDamageable> dectectedDamageable = new List<IDamageable>();
        // Transform attackTransform ;
        bool m_ExecutionFired;
        float m_MaxDistance = 0.35f;


        private ulong m_ProvisionalTarget;


        public PlayerDUAState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
           
        }

        public override void Enter()
        {      
            base.Enter();

            ulong target = player.serverplayer.NetState.TargetId.Value;

            m_Data.StateTypeEnum = StateType.Attack;
            m_Data.TargetIds = new ulong[] {target};

            IDamageable foe = DetectFoe(target);
            if (foe != null)
            {
                m_ProvisionalTarget = foe.NetworkObjectId;
                m_Data.TargetIds = new ulong[] { foe.NetworkObjectId };
            }

            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
          
        }


        public override StateType GetId()
        {
            return StateType.DUA;
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
                    // Debug.Log(foe);

                    foe.ReceiveHP(this.player.serverplayer, -SkillDescription(StateType.Attack).Amount);
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
            return GetIdealMeleeFoe(player.serverplayer.IsNpc,player.serverplayer.GetComponent<Collider>(), m_MaxDistance, foeHint);
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

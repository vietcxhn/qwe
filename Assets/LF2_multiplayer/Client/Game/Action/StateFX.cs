using UnityEngine;
using System.Collections.Generic;
using System;

namespace LF2.Visual{

    /// <summary>
    /// Abstract base class for playing back the visual feedback of Current State.
    /// </summary>
    public abstract class StateFX: StateBase{

        protected PlayerStateMachineFX MPlayerMachineFX;

        public StateRequestData Data;

        public bool IsMove { get; private set; }

        //Vector use for All Movement of player
        protected Vector3 moveDir;



        protected StateFX(PlayerStateMachineFX mPlayerMachineFX)
        {
            this.MPlayerMachineFX = mPlayerMachineFX;
            Enter();
        }



        public bool Anticipated { get; protected set; }


        public abstract StateType GetId();

        // Alaways check if player are already play animation first
        public virtual void Enter(){
            Anticipated = false; //once you start for real you are no longer an anticipated action.
            TimeStarted = Time.time;
        }
        public abstract bool LogicUpdate();



        public virtual void Exit(){
            Anticipated = false;
        }


        /// <summary>
        /// Called when the visualization receives an animation event.
        /// </summary>
        public virtual void OnAnimEvent(string id) { }

        // Play Animation (shoulde be add base.PlayAnim() in specific (class) that derived from State ) 
        // See in class AttackStateFX 
        // public virtual void  PlayAnim(StateType currentState ){
        //     m_PlayerFX.stateMachineViz.CurrentStateViz = currentState;
        //     Anticipated = true;
        //     TimeStarted = UnityEngine.Time.time;  
        // }
        public virtual void  PlayAnim(StateType currentState , int nbAniamtion = 0 ){
            MPlayerMachineFX.CurrentStateViz = currentState.getStateFX(MPlayerMachineFX);
            Anticipated = true;
            TimeStarted = UnityEngine.Time.time;  
        }

        public virtual void SetMovementTarget(Vector2 position)
        {
             MPlayerMachineFX.moveDir.Set(position.x , 0, position.y);   
            IsMove  = position.x != 0 || position.y != 0;
        }

        public virtual void AnticipateState(ref StateRequestData requestData)
        {
        }
 
        public virtual void End()
        {
            MPlayerMachineFX.idle();
        }
    }
}
   
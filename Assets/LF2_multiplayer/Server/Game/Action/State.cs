using System;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server
{
    /// <summary>
    /// The abstract parent class that all State derive from.
    /// </summary>
    /// <remarks>
    /// The State System is a generalized mechanism for Characters to "do stuff" in a networked way. State
    /// include everything from your basic character attack, to a fancy skill  Shot, 

    /// For every StateType enum, there will be one specialization of this class.

    ///
    /// The flow for State is:
    /// Initially: Enter()
    /// Every frame:   LogicUpdate() + PhysicUpdate() (can be 1 of 2)
    /// On shutdown: End() (end this State Naturelly) or Exit() (be interrupted by some logic (force to change State))
    /// After End(): Almost time will Switch to Idle .  
    ///

    // / Note also that if Start() returns false, no other functions are called on the Action, not even End().
    /// </remarks>
    public abstract class State : StateBase
    {
        protected PlayerStateMachine player;

        public StateRequestData m_Data;

        protected MovementState currentMovementState;
        

        // protected StateRequestData m_ActionRequestData;

        public bool IsMove { get; private set; }

        //Vector use for All Movement of player
        protected Vector3 moveDir;


        protected static ulong OurNetWorkID ;

        protected State(PlayerStateMachine player)
        {
            
            this.player = player;
        }

        public virtual SkillsDescription SkillDescription(StateType stateType){
            SkillsDescription value ;
            var found = player.m_CharacterSkillsDescription.SkillDataByType.TryGetValue(stateType , out value);
            Debug.AssertFormat(found, "Tried to find StateType %s but it was missing from GameDataSource!", stateType);
            return value;
            //           Debug.Log(result);
            // Debug.AssertFormat(found, "Tried to find StateType %s but it was missing from GameDataSource!", Data.StateTypeEnum);
        }

        // Get the StateType of current State  
        public abstract StateType GetId();
        public virtual void Enter(){
            TimeStarted = Time.time;
        }
        public virtual void LogicUpdate() {}

        public virtual void PhysicsUpdate(){}
   

        public virtual void Exit(){
            //getSet.stateMachine.ChangeState(StateType.Idle);
        }

        // If we have a request so check if we can change to desired state 
        // NOTE :   (Only 3 basic State can check Attack , Jump , Defense)
        public abstract void  CanChangeState(StateRequestData actionRequestData);

        public virtual void SetMovementTarget(Vector2 position)
        {
            player.moveDir.Set(position.x , 0, position.y);   
            IsMove  = position.x != 0 || position.y != 0;
        }

        public virtual void End()
        {
            player.ChangeState(StateType.Idle);
        }

        public enum GameplayActivity
        {
            AttackedByEnemy,
            Healed,
            StoppedChargingUp,
            UsingAttackAction, // called immediately before we perform the attack Action
        }

        public virtual void OnGameplayActivity(GameplayActivity activityThatOccurred)
        {
        }
    }
}
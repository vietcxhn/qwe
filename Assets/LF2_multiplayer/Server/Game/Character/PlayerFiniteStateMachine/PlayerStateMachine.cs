using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{
    
    public class PlayerStateMachine 
    {
        public ServerCharacter serverplayer;
        public CharacterTypeEnum chacterType;
        public ServerCharacterMovement ServerCharacterMovement;

        public Vector3 moveDir;

        public CharacterSkillsDescription m_CharacterSkillsDescription
        {

            get
            {
                CharacterSkillsDescription result;
                var found = GameDataSource.Instance.CharacterSkillDataByType.TryGetValue(chacterType, out result);
                // Debug.Log(result);
                Debug.AssertFormat(found, "Tried to find StateType but it was missing from GameDataSource!");
                return result;
            }
        }

        protected bool isAnimationFinished;

        protected float startTime;


        public PlayerStateMachine(ServerCharacter serverplayer ,ServerCharacterMovement serverCharacterMovement  ){
            this.serverplayer = serverplayer;
            ServerCharacterMovement = serverCharacterMovement;

            chacterType =  serverplayer.NetState.CharacterType;
            // Debug.Log(chacterType);
            RegisterState(new PlayerIdleState(this ));
            RegisterState(new PlayerMoveState(this ));
            RegisterState(new PlayerRunState(this));
            RegisterState(new SlidingState(this));
            RegisterState(new PlayerRollingState(this));

            RegisterState(new PlayerJumpState(this ));
            RegisterState(new PlayerDoubleJumpState(this ));
            

            RegisterState(new PlayerLandState(this ));

            RegisterState(new PlayerAttackState(this ));
            RegisterState(new PlayerAttackJump1(this ));
            RegisterState(new PlayerDefenseState(this));

            RegisterState(new PlayerHurtState(this));



            RegisterState(new PlayerDDAState(this));

            ChangeState(StateType.Idle);

            

        }

        
        public virtual SkillsDescription SkillDescription(StateType stateType){
            SkillsDescription value ;
            var found = m_CharacterSkillsDescription.SkillDataByType.TryGetValue(stateType , out value);
            Debug.AssertFormat(found, "Tried to find StateType %s but it was missing from GameDataSource!", stateType);
            return value;
            //           Debug.Log(result);
            // Debug.AssertFormat(found, "Tried to find StateType %s but it was missing from GameDataSource!", Data.StateTypeEnum);
        }

        // Client send input to Server to change the state of player
        public void RequestToState(ref StateRequestData requestData)
        {
            // Get data resquest to state correspond
            GetState(requestData.StateTypeEnum).m_Data = requestData;
            RequestChangeState(requestData);
        }

        // Client request to Server to Move the state of player

        // Something happen in the game 
        // Used to change State passively (tu chuyen doi state khi gap mot su kien nao do)
        // Exemple : Hurt (Attack by someone) change state player in Server to HurtState

        public State[] states = new State[Enum.GetNames(typeof(StateType)).Length];
        public StateType CurrentState{ get ; private set;}
        

        // RegisterState , instantiated in the first time 
        public void RegisterState(State state){
            int index = (int)state.GetId();
            states[index] = state;
        }

        // Do convert enum StateType == > State corresponse 
        public State GetState (StateType stateType){
            int index = (int)stateType;
            return states[index];
        }

        public void Update() {
            Debug.Log(CurrentState);
            GetState(CurrentState)?.LogicUpdate();
            if(CurrentState != StateType.Idle && CurrentState != StateType.Move  ){
                SkillsDescription skillsDescription = GetState(CurrentState).SkillDescription(CurrentState);
                if (skillsDescription.expirable)
                {
                    bool timeExpired = GetState(CurrentState).TimeRunning >= skillsDescription.DurationSeconds;
                    
                    if (timeExpired)
                    {
                        GetState(CurrentState)?.End();
                    }
                }
            }
        }


        public void ChangeState(StateType newState ){
            GetState(CurrentState)?.Exit();
            CurrentState = newState;
            GetState(CurrentState)?.Enter();
        }

        public void SetMovementDirection(Vector2 targetPosition)
        {
            GetState(CurrentState).SetMovementTarget( targetPosition);
        }

        public void PhysicUpdate()
        {
            GetState(CurrentState)?.PhysicsUpdate();
        }

        // Note Only 3 basics State (Attack , Jump , Defense )  and Skills (DDA , DDJ ...) can be checked 
        public void RequestChangeState(StateRequestData data){

            GetState(CurrentState).CanChangeState(data);
        }


        /// <summary>
        /// Tells all active Actions that a particular gameplay event happened, such as being hit,
        /// getting healed, dying, etc. Actions can change their behavior as a result.
        /// </summary>
        /// <param name="activityThatOccurred">The type of event that has occurred</param>
        public void OnGameplayActivity(State.GameplayActivity activityThatOccurred)
        {
            if (activityThatOccurred == State.GameplayActivity.AttackedByEnemy){
                // GetState(CurrentState).OnGameplayActivity(activityThatOccurred);
                ChangeState(StateType.Hurt);
                }


        }

    }
    
}

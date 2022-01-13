using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{

    /// <summary>
    /// This is a companion class to ClientCharacterVisualization that is specifically responsible for visualizing State. 

    //     
    /// The flow for Visual is:
    /// Initially: Aniticipate() only play action 
    /// if recevie signal from Server , call PlayState() to active  LogicUpdate() + PhysicUpdate() 
  


    /// </summary>
    public class PlayerStateMachineFX
    {

        
        public StateFX[] statesViz = new StateFX[Enum.GetNames(typeof(StateType)).Length]; // All State we declare 
        public StateFX CurrentStateViz; // CurrentState visual we are 

        private StateFX m_lastStateViz; 

        private SkillsDescription skillsDescription;

        public Vector3 moveDir;
        
        public CharacterTypeEnum characterType;
        
        public ClientCharacterVisualization m_ClientVisual { get; }

        public CharacterSkillsDescription m_CharacterSkillsDescription
        {
            get
            {
                CharacterSkillsDescription result;
                var found = GameDataSource.Instance.CharacterSkillDataByType.TryGetValue(characterType, out result);
                // Debug.Log(result);
                Debug.AssertFormat(found, "Tried to find StateType but it was missing from GameDataSource!");
                return result;
            }
        }
        
        public PlayerStateMachineFX(ClientCharacterVisualization parent , CharacterTypeEnum characterType)
        {
            m_ClientVisual = parent;
            this.characterType = characterType;
            
            // Intiliazie State
            CurrentStateViz = new PlayerIdleStateFX(this);

        }

        // Aticipate State in CLient , Just run Animation , So not run Update () 
        public void AnticipateState(ref StateRequestData data)
        {
            CurrentStateViz.AnticipateState(ref data);
        }

        // Play correct State that sent by Server 
        public void PerformActionFX(ref StateRequestData data)
        {
            ChangeState(data.StateTypeEnum.getStateFX(this));
            CurrentStateViz.Data = data;
        }
        
        public virtual SkillsDescription SkillDescription(StateType stateType){
            SkillsDescription value ;
            var found = m_CharacterSkillsDescription.SkillDataByType.TryGetValue(stateType , out value);
            Debug.AssertFormat(found, "Tried to find StateType %s but it was missing from GameDataSource!", stateType);
            return value;
            //           Debug.Log(result);
            // Debug.AssertFormat(found, "Tried to find StateType %s but it was missing from GameDataSource!", Data.StateTypeEnum);
        }

        // Do convert enum StateType == > State corresponse 
        public StateFX GetState (StateType stateType){
            return stateType.getStateFX(this);
        }

        
        /// Every frame:  Check current Animation to end the animation , 
        // If recevie request form Server can active  LogicUpdate() of this State
        public void Update() {
            // Check ALL State that have actual Action correspond ( See in Game Data Soucre Objet )

            Debug.Log(CurrentStateViz);
            if (CurrentStateViz.GetId() == StateType.Idle) return;

            if (CurrentStateViz.GetId() == StateType.Move){
                m_lastStateViz = CurrentStateViz;
                CurrentStateViz.LogicUpdate();
                return;
            }

            if ( m_lastStateViz != CurrentStateViz){
                m_lastStateViz = CurrentStateViz;
                if (CurrentStateViz.GetId() == StateType.Idle || CurrentStateViz.GetId() == StateType.Move ) return;
                skillsDescription =  SkillDescription(CurrentStateViz.GetId()); // Get All Skills Data of actual Player Charater we current play.
            } 

            if (skillsDescription!= null){
                if (skillsDescription.expirable)
                {
                    bool timeExpired = CurrentStateViz.TimeRunning >= skillsDescription.DurationSeconds;

                    // Check if this State Can End Naturally (== time Expired )
                    if ( timeExpired ){
                        CurrentStateViz?.End();
                        return;
                    }
                }
                
            }
            if (!CurrentStateViz.LogicUpdate()){
                CurrentStateViz?.End();
            }

            

        }

        public void OnAnimEvent(string id)
        {
            CurrentStateViz.OnAnimEvent(id);
        }

        // Switch to Another State , (we force to Change State , so that mean this State may be not End naturally , be interruped by some logic  ) 
        public void ChangeState( StateFX state){
            if (CurrentStateViz.GetId() != state.GetId()){
                CurrentStateViz?.Exit();
                CurrentStateViz = state;
            }
        }

        public void idle()
        {
            ChangeState(new PlayerIdleStateFX(this));
        }

        // Movement in Client 
        public void OnMoveInput(Vector2 position)
        {
            CurrentStateViz.SetMovementTarget(position);
        }
    }
    
    
    
}

using System.Collections.Generic;
using LF2.Client;
// using Cinemachine;
using Unity.Netcode;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace LF2.Visual
{
    /// <summary>
    /// <see cref="ClientCharacterVisualization"/> is responsible for displaying a character on the client's screen based on state information sent by the server.
    /// </summary>
    public class ClientCharacterVisualization : MonoBehaviour
    {
        [SerializeField]
        private Animator m_ClientVisualsAnimator;

        // [SerializeField]
        // private CharacterSwap m_CharacterSwapper;

        private ClientInputSender inputSender;
        [SerializeField]
        private VisualizationConfiguration m_VisualizationConfiguration;
        

        /// <summary>
        /// Returns a reference to the active Animator for this visualization
        /// </summary>
        public Animator OurAnimator { get { return m_ClientVisualsAnimator; } }

        public bool CanPerformActions { get { return m_NetState.CanPerformActions; } }


        PhysicsWrapper m_PhysicsWrapper;

        
        // public CoreMovement m_coreMovement {get ; private set;}
        [SerializeField]
        public CoreMovement coreMovement  ;


        public NetworkCharacterState m_NetState;

        public ulong NetworkObjectId => m_NetState.NetworkObjectId;


        // public Transform Parent { get; private set; }

        public PlayerStateMachineFX MStateMachinePlayerViz{ get; private set; } 


        /// Player characters need to report health changes and chracter info to the PartyHUD
        PartyHUD m_PartyHUD;

        float m_SmoothedSpeed;

        int m_HitStateTriggerID;
        private float m_MaxDistance = 0.2f;


        public event Action<Animator> animatorSet;


        /// <inheritdoc />
        public void Start()
        {            
            if (!NetworkManager.Singleton.IsClient || transform.parent == null)
            {
                enabled = false;
                return;
            }

            m_NetState = GetComponentInParent<NetworkCharacterState>();


            // Parent = m_NetState.transform;

            // if (Parent.TryGetComponent(out ClientAvatarGuidHandler clientAvatarGuidHandler))
            // {
            //     m_ClientVisualsAnimator = clientAvatarGuidHandler.graphicsAnimator;

            //     // Netcode for GameObjects (Netcode) does not currently support NetworkAnimator binding at runtime. The
            //     // following is a temporary workaround. Future refactorings will enable this functionality.
            //     animatorSet?.Invoke(clientAvatarGuidHandler.graphicsAnimator);
            // }

            m_PhysicsWrapper = m_NetState.GetComponent<PhysicsWrapper>();

            MStateMachinePlayerViz = new PlayerStateMachineFX( this,m_NetState.CharacterType);


            m_NetState.DoActionEventClient += PerformActionFX;
            m_NetState.CancelAllActionsEventClient += CancelAllActionFXs;
            m_NetState.CancelActionsByTypeEventClient += CancelActionFXByType;
            m_NetState.OnStopChargingUpClient += OnStoppedChargingUp;
            m_NetState.IsStealthy.OnValueChanged += OnStealthyChanged;

            // sync our visualization position & rotation to the most up to date version received from server

            transform.SetPositionAndRotation(m_PhysicsWrapper.Transform.position, m_PhysicsWrapper.Transform.rotation);


            // ...and visualize the current char-select value that we know about
            SetAppearanceSwap();


            if (!m_NetState.IsNpc)
            {
                name = "AvatarGraphics" + m_NetState.OwnerClientId;

                if (m_NetState.IsOwner)
                {
                    gameObject.AddComponent<CameraController>();
                    inputSender = GetComponentInParent<ClientInputSender>();
                    // Debug.Log(inputSender);
                    // TODO: revisit; anticipated actions would play twice on the host
                    
                    inputSender.ActionInputEvent += OnActionInput;
                    inputSender.ClientMoveEvent += OnMoveInput;
                
                }
            }
        }

        // Do anticipate State : Only play Animation , not change state
        private void OnActionInput(StateRequestData data)
        {
            MStateMachinePlayerViz.AnticipateState(ref data);
        }

        
        private void PerformActionFX(StateRequestData data)
        {
            // That event do actual State from Server .
            MStateMachinePlayerViz.PerformActionFX(ref data);
        }

        // Play Animation and change state between Idle and Move State Visual
        private void OnMoveInput(Vector2 position)
        {
            // OurAnimator.SetInteger("Speed" , 1);
            MStateMachinePlayerViz.OnMoveInput(position);
        }

        private void OnDestroy()
        {
            if (m_NetState)
            {
                m_NetState.DoActionEventClient -= PerformActionFX;
                m_NetState.CancelAllActionsEventClient -= CancelAllActionFXs;
                m_NetState.CancelActionsByTypeEventClient -= CancelActionFXByType;
                m_NetState.OnStopChargingUpClient -= OnStoppedChargingUp;
                m_NetState.IsStealthy.OnValueChanged -= OnStealthyChanged;

                if (m_NetState.IsOwner)
                {
                    
                        inputSender.ActionInputEvent -= OnActionInput;
                        inputSender.ClientMoveEvent -= OnMoveInput;

                }
                
            }

        }



        private void CancelAllActionFXs()
        {
        }

        private void CancelActionFXByType(StateType actionType)
        {
        }

        private void OnStoppedChargingUp(float finalChargeUpPercentage)
        {
        }



        // private void OnHealthChanged(int previousValue, int newValue)
        // {
        //     // don't do anything if party HUD goes away - can happen as Dungeon scene is destroyed
        //     if (m_PartyHUD == null) { return; }

        //     if (IsLocalPlayer)
        //     {
        //         this.m_PartyHUD.SetHeroHealth(newValue);
        //     }
        //     // else
        //     // {
        //     //     this.m_PartyHUD.SetAllyHealth(m_NetState.NetworkObjectId, newValue);
        //     // }
        // }

        // private void OnCharacterAppearanceChanged(int oldValue, int newValue)
        // {
        //     SetAppearanceSwap();
        // }

        private void OnStealthyChanged(bool oldValue, bool newValue)
        {
            SetAppearanceSwap();
        }

        private void SetAppearanceSwap()
        {
            // if (m_CharacterSwapper)
            // {

            //     m_CharacterSwapper.SwapToModel(m_NetState.CharacterAppearance.Value);
            // }
        }



        void Update()
        {


            // if (m_ClientVisualsAnimator)
            // {
            //     OurAnimator.SetFloat("Speed", GetVisualMovementSpeed());
            //
            // }

            
                
            
            MStateMachinePlayerViz.Update();
            
        }

        // Huy : Not use Yet        
        public void OnAnimEvent(string id)
        {
            //if you are trying to figure out who calls this method, it's "magic". The Unity Animation Event system takes method names as strings,
            //and calls a method of the same name on a component on the same GameObject as the Animator. See the "attack1" Animation Clip as one
            //example of where this is configured.

            MStateMachinePlayerViz.OnAnimEvent(id);
        }

        /// <summary>
        /// Returns the value we should set the Animator's "Speed" variable, given current gameplay conditions.
        /// </summary>
        private float GetVisualMovementSpeed()
        {
            // Assert.IsNotNull(m_VisualizationConfiguration);
            // if (m_NetState.NetworkLifeState.LifeState.Value != LifeState.Alive)
            // {
            //     return m_VisualizationConfiguration.SpeedDead;
            // }

            switch (m_NetState.MovementStatus.Value)
            {

                case MovementStatus.Walking:
                    return 1;
                default:
                    return 0;
            }
        }

        

    }
}

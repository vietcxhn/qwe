using System;
using Unity.Netcode;
using UnityEngine;

namespace LF2.Client
{
    /// <summary>
    /// Generic movement object that updates transforms based on the state of an INetMovement source.
    /// This is part of a temporary movement system that will be replaced once Netcode for GameObjects can drive
    /// movement internally.
    /// </summary>
    public class ClientGenericMovement : NetworkBehaviour
    {
        [SerializeField]
        NetworkCharacterState m_MovementSource;
        // private Rigidbody m_Rigidbody;
        private bool m_Initialized;


        // Start is called before the first frame update
        void Start()
        {
            // m_Rigidbody = GetComponent<Rigidbody>(); //this may be null.
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                //this component is not needed on the host (or dedicated server), because ServerCharacterMovement will directly
                //update the character's position.
                this.enabled = false;
            }
            m_Initialized = true;

            m_MovementSource.NetworkRotationY.OnValueChanged += SetRotation;

        }

        private void SetRotation(int previousValue, int newValue)
        {
            // In client predict the rotation so dont need to change 
            if (transform.rotation.y != newValue){
                transform.rotation = Quaternion.Euler(0, newValue, 0);
            }
        }


      
    }
}


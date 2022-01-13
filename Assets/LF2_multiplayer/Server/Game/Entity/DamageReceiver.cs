using System;
using Unity.Netcode;
using UnityEngine;

namespace LF2.Server
{
    public class DamageReceiver : NetworkBehaviour, IDamageable
    {
        public event Action<ServerCharacter, int> damageReceived;

        public event Action<Collision> collisionEntered;

        [SerializeField]
        NetworkLifeState m_NetworkLifeState;

        
        public ServerCharacter serverCharacter;

        public void ReceiveHP(ServerCharacter inflicter, int HP)
        {
            damageReceived?.Invoke(inflicter, HP);
        }


        public bool IsDamageable()
        {
            return m_NetworkLifeState.LifeState.Value == LifeState.Alive;
        }

        void OnCollisionEnter(Collision other)
        {
            collisionEntered?.Invoke(other);
        }
    }
}

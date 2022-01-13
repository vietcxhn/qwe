using System;
using LF2.Visual;
using Unity.Netcode;
using UnityEngine;

namespace LF2.Client
{
    /// <summary>
    /// Client-side component that awaits a state change on an avatar's Guid, and fetches matching Avatar from the
    /// AvatarRegistry, if possible. Once fetched, the Graphics GameObject is spawned.
    /// </summary>
    // [RequireComponent(typeof(NetworkAvatarGuidState))]
    public class ClientAvatarGuidHandler : NetworkBehaviour
    {
        [SerializeField]
        ClientCharacter m_ClientCharacter;

        [SerializeField]
        NetworkAvatarGuidState m_NetworkAvatarGuidState;

        [SerializeField]
        private Animator m_Animator;


        public event Action<GameObject> AvatarGraphicsSpawned;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            InstantiateAvatar();
        }

        void InstantiateAvatar()
        {
            if (m_ClientCharacter.ChildVizObject)
            {
                // we may receive a NetworkVariable's OnValueChanged callback more than once as a client
                // this makes sure we don't spawn a duplicate graphics GameObject
                return;
            }
            var graphicsGameObject = m_NetworkAvatarGuidState.RegisteredAvatar.Graphics;

            m_Animator.runtimeAnimatorController = graphicsGameObject.GetComponent<Animator>().runtimeAnimatorController;

            // m_GraphicsAnimator = graphicsGameObject.GetComponent<Animator>();

            // spawn avatar graphics GameObject
            // var graphicsGameObject = Instantiate(m_NetworkAvatarGuidState.RegisteredAvatar.Graphics, m_GraphicsAnimator.transform);

            m_ClientCharacter.SetCharacterVisualization(GetComponent<ClientCharacterVisualization>());

            // m_GraphicsAnimator.Rebind();
            // m_GraphicsAnimator.Update(0f);

            // AvatarGraphicsSpawned?.Invoke(graphicsGameObject);
        }
    }
}

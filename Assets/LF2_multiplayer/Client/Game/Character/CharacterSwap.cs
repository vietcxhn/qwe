using UnityEngine.Assertions;
using UnityEngine;
using System.Collections.Generic;

namespace LF2.Client
{
    /// <summary>
    /// Responsible for storing of all of the pieces of a character, and swapping the pieces out en masse when asked to.
    /// </summary>
    public class CharacterSwap : MonoBehaviour
    {
        [System.Serializable]
        public class CharacterModelSet
        {
            public Sprite image;

            public RuntimeAnimatorController animatorOverrides; // references a separate stand-alone object in the project
            
        }

        [SerializeField]
        private CharacterModelSet[] m_CharacterModels;

        /// <summary>
        /// Reference to our shared-characters' animator.
        /// Can be null, but if so, animator overrides are not supported!
        /// </summary>
        [SerializeField]
        private Animator m_Animator;

        /// <summary>
        /// Reference to the original controller in our Animator.
        /// We switch back to this whenever we don't have an Override.
        /// </summary>
        private RuntimeAnimatorController m_OriginalController;

        [SerializeField]
        private GameObject Background;




        /// <summary>
        /// When we swap all our Materials out for a special material,
        /// we keep the old references here, so we can swap them back.
        /// </summary>
        private Dictionary<Renderer, Material> m_OriginalMaterials = new Dictionary<Renderer, Material>();
        private bool firstime = true;

        private void Awake()
        {
            if (m_Animator)
            {
                m_OriginalController = m_Animator.runtimeAnimatorController;
            }
        }

        private void OnDisable()
        {
            // It's important that the original Materials that we pulled out of the renderers are put back.
            // Otherwise nothing will Destroy() them and they will leak! (Alternatively we could manually
            // Destroy() these in our OnDestroy(), but in this case it makes more sense just to put them back.)
        }

        /// <summary>
        /// Swap the visuals of the character to the index passed in.
        /// </summary>
        /// <param name="modelIndex">Zero-based array index of the model</param>
        /// <param name="specialMaterialMode">Special Material to apply to all body parts</param>
        public void SwapToModel(int modelIndex)
        {
            Assert.IsTrue(modelIndex < m_CharacterModels.Length);

            if (firstime ){
                Background.SetActive(true);
                firstime = false ; 
            }
            

            if (m_Animator)
            {
                // plug in the correct animator override... or plug the original non - overridden version back in!
                if (m_CharacterModels[modelIndex].animatorOverrides)
                {
                    m_Animator.runtimeAnimatorController = m_CharacterModels[modelIndex].animatorOverrides;
                }
                else
                {
                    m_Animator.runtimeAnimatorController = m_OriginalController;
                }            
            }

        }





#if UNITY_EDITOR
        private void OnValidate()
        {
            // if an Animator is on the same GameObject as us, assume that's the one we'll be using!
            if (!m_Animator)
                m_Animator = GetComponent<Animator>();
        }
#endif
    }
}

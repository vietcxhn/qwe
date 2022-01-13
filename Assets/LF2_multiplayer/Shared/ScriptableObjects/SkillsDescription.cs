
using System;
using UnityEngine;


namespace LF2{
    /// <summary>
    /// Data description of a single State of sigle Player (For exemple Attack of David), including the information to visualize it (animations etc), and the information
    /// to play it back on the server.
    /// </summary>
    [CreateAssetMenu(fileName = "GameData/CharacterClass", menuName = "SkillsDescription")]
    [System.Serializable]
    public class SkillsDescription : ScriptableObject
    {
        public StateType StateType; //The kind of the move
        
        public ActionLogic ActionLogic;
        public int Amount;

        public int ManaCost;

        public float Range;

        public Vector3 velocity;
        // public AnimationCurve animationCurve;

        public float DurationSeconds;

        public bool expirable;

        [Serializable]
        public class ProjectileInfo
        {
            [Tooltip("Prefab used for the projectile")]
            public GameObject ProjectilePrefab;
            [Tooltip("Projectile's speed in meters/second")]
            public float Speed_m_s;
            [Tooltip("Maximum range of the Projectile")]
            public float Range;
            [Tooltip("Damage of the Projectile on hit")]
            public int Damage;
            [Tooltip("Max number of enemies this projectile can hit before disappearing")]
            public int MaxVictims;
        }
        
        [Tooltip("If this Action spawns a projectile, describes it. (\"Charged\" projectiles can list multiple possible shots, ordered from weakest to strongest)")]
        public ProjectileInfo[] Projectiles;
        // [SerializeField] int ComboPriorty = 0; //the more complicated the move the higher the Priorty

    }
}



using System;
using UnityEngine;


namespace LF2{
    /// <summary>
    /// Data description of a single State of sigle Player (For exemple Attack of David), including the information to visualize it (animations etc), and the information
    /// to play it back on the server.
    /// </summary>
    [CreateAssetMenu(fileName = "AttackSkills", menuName = "GameData/AttackSkills")]
    [System.Serializable]
    public class SO_AttackDetails : SkillsDescription
    {
        public AttackDetails[] AttackDetails;
    }
}


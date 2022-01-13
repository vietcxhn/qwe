using UnityEngine;

namespace LF2
{
    /// <summary>
    /// Describes how a specific character visualization should be animated.
    /// </summary>
    [CreateAssetMenu]
    public class VisualizationConfiguration : ScriptableObject
    {
        
    // [SerializeField] string m_SpeedVariable = "Speed";
    [SerializeField] [HideInInspector] 
    public int SpeedVariableID = Animator.StringToHash("Speed");


    int idle = Animator.StringToHash("Idle_anim");
    public int Idle {get => idle;}

    int walk = Animator.StringToHash("Walk_anim");
    public int Walk {get => walk;}


    int jump = Animator.StringToHash("Jump_anim");
    public int Jump {get => jump;}


    int doubleJump = Animator.StringToHash("DoubleJump_anim");
    public int DoubleJump {get => doubleJump;}

    int doubleJump2 = Animator.StringToHash("DoubleJump2_anim");
    public int DoubleJump2 {get => doubleJump2;}


    int land = Animator.StringToHash("Land_anim");
    public int Land {get => land;}

    [HideInInspector]
    public int Air = Animator.StringToHash("Air_anim");
    [HideInInspector]
    public int Run = Animator.StringToHash("Run_anim");
    [HideInInspector]
    public int Sliding = Animator.StringToHash("Sliding_anim");
    private int attack1=  Animator.StringToHash("Attack1_anim") ;
    public int Attack1 {
        get {return attack1;}
    }

    int attack2 = Animator.StringToHash("Attack2_anim") ;
    public int Attack2 {get {return attack2;}}

    int attack3 = Animator.StringToHash("Attack3_anim") ;
    public int Attack3 {get {return attack3;}}

    int attack4 = Animator.StringToHash("Attack4_anim") ;
    public int Attack4 {get {return attack4;}}
    int attack5 = Animator.StringToHash("Attack5_anim") ;
    [HideInInspector]
    public int Attack5 {get {return attack5;}}

    [HideInInspector]
    public int Defense = Animator.StringToHash("Defense_anim") ;
    [HideInInspector]
    public int Rolling = Animator.StringToHash("Rolling_anim") ;

    [HideInInspector]
    public int Hurt1 = Animator.StringToHash("Hurt1_anim");
    [HideInInspector]
    public int Hurt2 = Animator.StringToHash("Hurt2_anim");
    [HideInInspector]
    public int Hurt3 = Animator.StringToHash("Hurt3_anim");
    [HideInInspector]
    public int Hurt3Contre = Animator.StringToHash("Hurt3Control_anim");

    }
}

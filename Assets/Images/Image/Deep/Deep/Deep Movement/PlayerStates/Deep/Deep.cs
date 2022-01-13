// using UnityEngine;
// public class Deep : Player  , IPlayerPosition
// {
//     [SerializeField] private SkillsData skillsData;
//     public DeepDDAstate DDAstate{get; private set;}
//     public DeepDUAstate DUAstate{get; private set;}

//     public DeepDDJstate DDJstate{get; private set;}
//     public DeepDUJstate DUJstate{get; private set;}

//     // Name of Animtion is the same with LF2 original only (David , or Deep ) 
//     // because lazy to change the name 

//     // But in my game we change "hashID" to apdapte for mobile 
//     int DDA = Animator.StringToHash("D_L_A_anim");

//     int DDJ = Animator.StringToHash("D_L_J_anim");

    
//     int DUA = Animator.StringToHash("D_U_A_anim");
//     int DUJ = Animator.StringToHash("D_U_J_anim");

//     [SerializeField] private bool _isTargetable = true;

//     protected override void Awake()
//     {
//         base.Awake();

//     }

    
//     protected override void Start()
//     {
//         base.Start();

//         // nhay chem
//         DUAstate = new DeepDUAstate(this , StateMachine,PlayerData,DUA,skillsData);
//         // ban chuong
//         DDAstate = new DeepDDAstate(this , StateMachine,PlayerData,DDA,skillsData);
//         // Lon :)) 
//         DUJstate = new DeepDUJstate(this , StateMachine,PlayerData,DUJ,skillsData);
//         // chem lien hoan
//         DDJstate = new DeepDDJstate(this , StateMachine,PlayerData,DDJ,skillsData);

//         // StateMachine.Initialize(IdleState);

//         // Listen Combo Event from input 

//         InputHandler.ComboTrigger += HandleComboTrigger;
//         statsHealthSysteme.DeadEvent += Dead; 
        
//     }

//     protected override void Update()
//     {
//         base.Update();
//     }
    
//     protected override void FixedUpdate()
//     {
//         base.FixedUpdate();
//     }

//     private void HandleComboTrigger(TypeSkills StateCombo)
//     {
//         // current mana < 0 
//         // if (statsHealthSysteme.currentMana <= 0) return;
//         // current mana < mana necessaire
//         // if (statsHealthSysteme.currentMana < skillsData.SkillDataByType[StateCombo].ManaCost) return;
//         Debug.Log(StateMachine);
//         switch (StateCombo){
//         case TypeSkills.DefUpJump:
            
//             StateMachine.ChangeState(DUJstate, skillsData.SkillDataByType[StateCombo]);
//             // statsHealthSysteme.SpendMana(skillsData.SkillDataByType[StateCombo].ManaCost);
//             break;
//         case TypeSkills.DefDownJump:
//             StateMachine.ChangeState(DDJstate,skillsData.SkillDataByType[StateCombo]);
//             // statsHealthSysteme.SpendMana(skillsData.SkillDataByType[StateCombo].ManaCost);
//             break;
        
//         case TypeSkills.DefUpAttack:
//             StateMachine.ChangeState(DUAstate,skillsData.SkillDataByType[StateCombo]);
//             // statsHealthSysteme.SpendMana(skillsData.SkillDataByType[StateCombo].ManaCost);
//             break;
        
//         case TypeSkills.DefDownAttack:
//             StateMachine.ChangeState(DDAstate, skillsData.SkillDataByType[StateCombo]);
//             // statsHealthSysteme.SpendMana(skillsData.SkillDataByType[StateCombo].ManaCost);
//             break;
//         }
        
//     }

 

//     // public CharacterTypeEnum WhoReciveHP()
//     // {
//     //     return CharacterTypeEnum.Deep;
//     // }

//     public Vector3 GetPlayerPosition()
//     {
//         return transform.position;
//     }

//     public void Dead(){
//         _isTargetable = false;
//         StateMachine.ChangeState(DieState);
//     }

//     public bool IsTargetable()
//     {
//         return _isTargetable;
//     }
// }

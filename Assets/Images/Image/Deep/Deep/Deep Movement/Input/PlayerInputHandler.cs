// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.UI;

// public enum KeyPress{
//     Attack,
//     Jump,
//     Defense,
//     Left,
//     Right,
//     Up,
//     Down
// } //All the Avilable Moves
// public class PlayerInputHandler : MonoBehaviour
// {
//     #region Movement
         
//     public Vector2 RawMovementInput { get ; private set;}
//     public int NormInputX{get ; private set;}
//     public int NormInputZ{get; private set;}
//     #endregion
//     // JUMP
//     public bool JumpInput{get;private set;}
//     [SerializeField] private float inputHoldTime = 0.2f;
//     private float jumpInputStartTime;
//     // JUMP

//     //RUN
//     private float lastClickRightTime;
//     private float lastClickLeftTime;
//     bool isRun;
//     private int countTime;
//     //RUN


//     public bool AttackInput{get;private set;}

//     public bool DefenseInput{get;private set;}

//     //COMBO  
//     public KeyPress currentKeyPress{get;private set;}
//     public List<KeyPress> currentCombo = new List<KeyPress>();
//     public List<ComboAttack> avilableSkills;
 
//     public bool ActivedCombo ;

//     [SerializeField] Text controlsTestText;

//     public event Action<TypeSkills> ComboTrigger;
//     // COMBO
    
    
//     private void Start(){
//         ActivedCombo = false ;
//     }
//     private void Update()
//     {
//         CheckJumpInutHoldTime();
//         CheckCombo();
//         // PrintControls();

//     }

//     private void CheckCombo()
//     {
//         if (currentCombo.Count == 3)
//         {
//             int i = 0;
//             ActivedCombo = false;
//             foreach (ComboAttack combo in avilableSkills)
//             {
//                 // Debug.Log("true");
//                 i++;
//                 if (combo.isTheSame(currentCombo))
//                 {

//                     currentCombo.Clear();
//                     // Debug.Log(combo.GetTypeOfState());
//                     ComboTrigger?.Invoke(combo.GetTypeOfCombo());
//                     return;
//                 }
//                 else
//                 {
//                     if (avilableSkills.Count == i)
//                     {
//                         currentCombo.Clear();
//                     }
//                 }
//             }
//         }
//     }


//     public void OnMoveInput(InputAction.CallbackContext context){
//         RawMovementInput = context.ReadValue<Vector2>();
//         // Debug.Log(RawMovementInput);
        
//         NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
//         NormInputZ = (int)(RawMovementInput * Vector2.down).normalized.y;
        
//         if (context.started)
//         {
//             DoubleClickedToRun();
//             if (ActivedCombo) {
//                 if (NormInputX >0) currentCombo.Add(KeyPress.Right);
//                 else if (NormInputX <0) currentCombo.Add(KeyPress.Left);
                
                
//                 if(NormInputZ>0) currentCombo.Add(KeyPress.Up);
//                 else if(NormInputZ<0) currentCombo.Add(KeyPress.Down);
//             }

//         }


//     }

//     public void OnJumpInput(InputAction.CallbackContext context){
//         if (context.started){
//             JumpInput = true;
//             jumpInputStartTime = Time.time;
//             if(ActivedCombo) {
//                 currentCombo.Add(KeyPress.Jump);
//             }
//         }
//     }
    
//     public void OnAttackInput(InputAction.CallbackContext context){
//         if (context.started){
//             AttackInput = true;
//             if(ActivedCombo) {
//                 currentCombo.Add(KeyPress.Attack);
//             }
//         }
//         if (context.canceled){
//             AttackInput = false;
//         }
//     }

//     public void OnDefenseInput(InputAction.CallbackContext context){
//         if (context.started){
//             DefenseInput = true;
//             ActivedCombo = true;
//             currentKeyPress = KeyPress.Defense;
//             currentCombo.Add(currentKeyPress);

//         }
//         if (context.canceled){
//             DefenseInput = false;
//         }
//     }
//     private void DoubleClickedToRun()
//     {
//         if (NormInputX > 0)
//         {
//             if (Time.time - lastClickRightTime <= inputHoldTime) countTime++;
//             else countTime = 1;
//             lastClickRightTime = Time.time;


//         }
//         else if (NormInputX < 0)
//         {
//             if (Time.time - lastClickLeftTime <= inputHoldTime) countTime++;

//             else countTime = 1;
//             lastClickLeftTime = Time.time;
//         }
//         else countTime = 0;
        
//     }

//     public void UseJumpInput() => JumpInput = false;
//     public bool IsAvailableToRun() {
//         if (countTime >=2 ){
//             countTime = 0;
//             return true;
//         }
//         else {
//             return false;
//         }

//     }

//     private void CheckJumpInutHoldTime(){
//         if ( Time.time >= jumpInputStartTime + inputHoldTime){
//             JumpInput = false;
//         }
//     }

//     public void ResetRun(){
//         countTime = 0;
//     }

//     public void PrintControls() {
//         controlsTestText.text = " Keys Pressed :";
//         foreach (KeyPress kcode in currentCombo)
//             controlsTestText.text += kcode + ",";
//     }


// }

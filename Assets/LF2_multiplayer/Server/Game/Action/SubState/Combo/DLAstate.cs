// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DLAstate : PlayerState
// {
//     private bool attackInput ;
//     public DLAstate(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
//     {
//     }

//     public override void Enter()
//     {
//         base.Enter();
//         // DeepDLA.Create(player.AttackTransform.position  ,core.SetMovement.FacingDirection*Vector3.right);
//     }

//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//         attackInput = player.InputHandler.AttackInput;
//         // if (isFinishedAnimation()){
//         //     stateMachine.ChangeState(player.IdleState);
//         // }

//     }
// }

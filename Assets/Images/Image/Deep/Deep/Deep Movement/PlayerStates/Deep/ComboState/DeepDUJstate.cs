// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DeepDUJstate : DUJstate
// {
//     // // Lon :)) 
//     SkillsData deep;
//     int hashID;
//     public DeepDUJstate(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID , SkillsData deep) : base(player, stateMachine, playerData, hashID)
//     {
//         this.deep = deep;
//         this.hashID = hashID;;
//     }

//     public override void Enter()
//     {
//         base.Enter();
//         Vector3 DUJ = stateMachine.CurrentState.skillDescription.velocity;
//         player.Core.SetMovement.SetVelocityY(DUJ);
//     }

//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//         if (player.CheckrGounded() && Mathf.Abs(player.currentVelocity.y) < 0.01f){
//             stateMachine.ChangeState(player.LandState);
//         }
//         else{
//             core.SetMovement.SetFallingDown();
//         }
//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//     }


// }

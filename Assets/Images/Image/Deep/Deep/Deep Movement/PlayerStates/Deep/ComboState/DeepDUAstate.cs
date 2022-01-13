// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DeepDUAstate : DUAstate
// {
//     // nhay chem
//     SkillsData deep;
//     int hashID;
//     public DeepDUAstate(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID , SkillsData deep) : base(player, stateMachine, playerData, hashID)
//     {
//         this.deep = deep;
//         this.hashID = hashID;
//     }

//     public override void Enter()
//     {
//         base.Enter();
//         Vector3 DUA = stateMachine.CurrentState.skillDescription.velocity;
//         player.Core.SetMovement.SetVelocityY(DUA);
//     }

//     public override void Exit()
//     {
//         base.Exit();
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

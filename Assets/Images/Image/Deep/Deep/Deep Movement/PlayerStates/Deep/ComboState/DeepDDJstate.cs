// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DeepDDJstate : DDJstate
// {
//     // chem lien hoan
//     SkillsData deep;
//     private int hashID;

//     float DDJ ;

//     public DeepDDJstate(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID , SkillsData deep) : base(player, stateMachine, playerData, hashID)
//     {
//         this.deep = deep;
//         this.hashID = hashID;
//     }

//     public override void Enter()
//     {
//         base.Enter();
//         DDJ = stateMachine.CurrentState.skillDescription.velocity.x;
//         // player.Core.SetMovement.SetVelocityY(DUA);
//     }



//     public override void Exit()
//     {
//         base.Exit();
//     }



//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//         if (isFinishedAnimation()){
//             stateMachine.ChangeState(player.IdleState);
//         }
//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//         player.Core.SetMovement.SetVelocityRun(DDJ);
//     }


// }

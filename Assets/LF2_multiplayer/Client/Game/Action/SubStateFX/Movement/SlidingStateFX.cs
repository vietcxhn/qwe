using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  LF2.Visual{

    public class SlidingStateFX : StateFX
    {
        private float _runSpeed;
        private float _gainDecreaseRunSpeed;

        public SlidingStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
            _runSpeed = mPlayerMachineFX.m_ClientVisual.m_NetState.CharacterClass.Speed;
            _gainDecreaseRunSpeed = 4f;
        }


        // _gainDecreaseRunSpeed = playerData.GainDecreaseRunSpeed;



        public override void Exit()
        {
            base.Exit();
            ResetRunVelocity();
        }

        public void ResetRunVelocity(){
            _runSpeed = MPlayerMachineFX.m_ClientVisual.m_NetState.CharacterClass.Speed;
        }

        public override StateType GetId(){
            return StateType.Sliding;
        }



        public override void Enter()
        {
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Sliding_anim");

            base.Enter();
        }

        public override void OnAnimEvent(string id)
        {
            base.OnAnimEvent(id);
        }

        public override void PlayAnim(StateType currentState, int nbAniamtion = 0)
        {
            base.PlayAnim(currentState, nbAniamtion);

        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }

        public override void AnticipateState(ref StateRequestData requestData)
        {
            base.AnticipateState(ref requestData);
        }



        public override bool LogicUpdate()
        {
            _runSpeed -= _gainDecreaseRunSpeed*Time.deltaTime;
            MPlayerMachineFX.m_ClientVisual.coreMovement.SetRun(_runSpeed);

            return _runSpeed > 0;
        }
    }
}
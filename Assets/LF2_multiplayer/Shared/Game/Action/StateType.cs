using LF2.Visual;

namespace LF2{
    public enum StateType
    {
        Idle,
        Move,
        Run,
        Jump,

        DoubleJump,

        Land,
        Defense,
        Attack,
        AttackJump1,
        Air,
        DUA,
        DDA,

        Ground,
        Hurt,

        Sliding,
        Rolling
        
    }

    static class GetStateFX
    {
        public static StateFX getStateFX(this StateType state, PlayerStateMachineFX mPlayerMachineFX)
        {
            switch (state)
            {
                case StateType.DDA:
                    return new PlayerDDAStateFX(mPlayerMachineFX);
                case StateType.AttackJump1:
                    return new PlayerAttackJump1FX(mPlayerMachineFX);
                case StateType.Attack:
                    return new PlayerAttackStateFX(mPlayerMachineFX);
                case StateType.Defense:
                    return new PlayerDefenseStateFX(mPlayerMachineFX);
                case StateType.Hurt:
                    return new PlayerHurtStateFX(mPlayerMachineFX);
                case StateType.DoubleJump:
                    return new PlayerDoubleJumpStateFX(mPlayerMachineFX);
                case StateType.Idle:
                    return new PlayerIdleStateFX(mPlayerMachineFX);
                case StateType.Jump:
                    return new PlayerJumpStateFX(mPlayerMachineFX);
                case StateType.Land:
                    return new PlayerLandStateFX(mPlayerMachineFX);
                case StateType.Move:
                    return new PlayerMoveStateFX(mPlayerMachineFX);
                case StateType.Rolling:
                    return new PlayerRollingStateFX(mPlayerMachineFX);
                case StateType.Run:
                    return new PlayerRunStateFX(mPlayerMachineFX);
                case StateType.Sliding:
                    return new SlidingStateFX(mPlayerMachineFX);
                case StateType.Air:
                    return new PlayerAirStateFX(mPlayerMachineFX);
                default:
                    return new PlayerIdleStateFX(mPlayerMachineFX);
            }
        }
    }
}
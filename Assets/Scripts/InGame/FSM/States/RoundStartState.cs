
namespace InGameState
{
    public class RoundStartState : InGameStateBase
    {
        public RoundStartState() : base(STATE.ROUND_START) { }

        public override void OnEnterState(InGame controller)
        {
            base.OnEnterState(controller);

            ChangeState(STATE.PLAYER_TURN);
        }
    }
}

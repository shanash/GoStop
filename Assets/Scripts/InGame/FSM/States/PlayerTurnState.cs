
namespace InGameState
{
    public class PlayerTurnState : InGameStateBase
    {
        public PlayerTurnState() : base(STATE.PLAYER_TURN) { }

        public override void OnEnterState(InGame controller)
        {
            base.OnEnterState(controller);
            controller.OnEnterPlayer();
        }
    }
}

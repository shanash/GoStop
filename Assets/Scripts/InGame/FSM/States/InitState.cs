
namespace InGameState
{
    public class InitState : InGameStateBase
    {
        public InitState() : base(STATE.INIT) { }

        public override void OnEnterState(InGame controller)
        {
            base.OnEnterState(controller);
            controller.OnEnterInit();

            ChangeState(STATE.ROUND_START);
        }
    }
}

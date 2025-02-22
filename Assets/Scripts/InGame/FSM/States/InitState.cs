
namespace InGameState
{
    public class InitState : InGameStateBase
    {
        public InitState() : base(STATE.INIT) { }

        public override void OnEnterState(InGame controller)
        {
            base.OnEnterState(controller);
            controller.Init();

            ChangeState(STATE.ROUND_START);
        }
    }
}

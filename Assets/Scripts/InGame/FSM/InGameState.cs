using ToyLets.GenericPatterns.State;
using UnityEngine;

namespace InGameState
{
    public enum STATE
    {
        NONE = 0,
        INIT,
        ROUND_START,
        PLAYER_TURN,
        SCORE_CALCULATION,
        GO_OR_STOP,
        ROUND_END,
        GAME_END,
    }

    public abstract class InGameStateBase : StateBase<STATE, InGame>
    {
        public InGameStateBase(STATE trans)
        {
            TransID = trans;
        }

        public override void OnEnterState(InGame controller)
        {
            Debug.Log($"**OnEnterState : {TransID}");
        }

        public override void OnUpdateState(InGame controller)
        {
        }

        public override void OnExitState(InGame controller)
        {
        }
    }
}


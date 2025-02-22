using System.Collections.Generic;
using ToyLets.GenericPatterns.State;

namespace InGameState
{
    public class InGameFSM : StateContextBase<STATE, InGame>
    {
        public InGameFSM(List<InGameStateBase> states, InGame controller, STATE initState)
        : base(states.ConvertAll(x => (StateBase<STATE, InGame>)x), controller, initState)
        {

        }
    }
}

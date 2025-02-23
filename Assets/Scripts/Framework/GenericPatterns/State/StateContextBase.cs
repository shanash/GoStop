using UnityEngine;
using System;
using System.Collections.Generic;

namespace ToyLets.GenericPatterns.State
{
    public abstract class StateContextBase<S, T> where S : Enum where T : class
    {
        const bool STATE_SYSTEM_DEBUG_MODE = true;

        public IState<S, T> CurrentState { get; protected set; }
        protected T _controller { get; private set; }
        private List<StateBase<S, T>> _states { get; set; }
        private Queue<S> _queueStates { get; set; }

        private List<S> _stateHistory = new List<S>();

        public StateContextBase(List<StateBase<S, T>> states, T controller, S initState)
        {
            var list = states.FindAll(x => x.TransID.Equals(initState));
            if (list.Count != 1)
            {
                if (list.Count == 0)
                {
                    Debug.LogError($"Not Exist Init State : {initState}");
                }
                else
                {
                    Debug.LogError($"Many Exist Init State : {initState} : {list.Count}");
                }
                return;
            }

            CurrentState = null;
            _controller = controller;
            _states = states;

            foreach (var state in _states)
            {
                state.SetFSM(this);
            }

            _queueStates = new Queue<S>();

            ChangeState(initState, true);
        }

        public StateBase<S, T> GetFindStateByTrans(S trans)
        {
            var s = _states.Find(x => x.TransID.Equals(trans));
            return s;
        }

        public void ChangeState(S trans, bool isInit = false)
        {
            if (isInit)
            {
                RealChangeState(trans);
            }
            else
            {
                _queueStates.Enqueue(trans);
            }
        }

        private void RealChangeState(S trans)
        {
            if (trans.Equals(default(S)))
            {
                Debug.LogError("GameStateSystem.ChangeState() WARNING : Transition is NONE");
                return;
            }

            var newState = GetFindStateByTrans(trans);

            if (CurrentState == newState)
            {
                Debug.LogWarningFormat("GameStateSystem.ChangeState() WARNING : Already Current Transition ID => {0}", trans.ToString());
                return;
            }

            if (newState == null)
            {
                Debug.LogWarningFormat("GameStateSystem.ChangeState() WARNING : {0} Not found transition id.", trans.ToString());
                return;
            }

            if (STATE_SYSTEM_DEBUG_MODE)
            {
                if (_stateHistory.Count >= 20)
                {
                    _stateHistory.RemoveAt(0);
                }
                _stateHistory.Add(trans);
            }

            try
            {
                CurrentState?.OnExitState(_controller);
                newState.OnEnterState(_controller);
                CurrentState = newState;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public void UpdateState()
        {
            CurrentState.OnUpdateState(_controller);

            if (_queueStates.Count > 0)
            {
                var trans = _queueStates.Dequeue();
                RealChangeState(trans);
            }
        }

        public void PrintStatesHistory()
        {
            string logHistory = string.Empty;
            foreach (var state in _stateHistory)
            {
                logHistory += $"{state}\n";
            }

            Debug.Log(logHistory);
        }
    }
}

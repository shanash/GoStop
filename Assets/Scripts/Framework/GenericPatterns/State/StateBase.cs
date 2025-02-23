using System;
using UnityEngine;

namespace ToyLets.GenericPatterns.State
{
    public abstract class StateBase<S, T> : IState<S, T> where S : Enum where T : class
    {
        public virtual S TransID { get; protected set; }
        protected float _deltaTime { get; set; }
        private StateContextBase<S, T> _stateContextBase { get; set; }

        public abstract void OnEnterState(T controller);
        public abstract void OnUpdateState(T controller);
        public abstract void OnExitState(T controller);

        public void SetFSM(StateContextBase<S, T> fsm)
        {
            _stateContextBase = fsm;
        }

        protected void ChangeState(S trans)
        {
            _stateContextBase.ChangeState(trans);
        }
    }
}

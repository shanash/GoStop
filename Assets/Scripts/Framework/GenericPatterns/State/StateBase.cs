using System;

namespace ToyLets.GenericPatterns.State
{
    public abstract class StateBase<S, T> : IState<S, T> where S : Enum where T : class
    {
        public S TransID { get; protected set; }
        protected float _deltaTime { get; set; }

        public abstract void OnEnterState(T controller);
        public abstract void OnUpdateState(T controller);
        public abstract void OnExitState(T controller);
    }
}

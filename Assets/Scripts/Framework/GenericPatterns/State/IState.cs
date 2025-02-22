using System;

namespace ToyLets.GenericPatterns.State
{
    /// <summary>
    /// 상태 인터페이스
    /// </summary>
    /// <typeparam name="S">상태 Enum 값</typeparam>
    /// <typeparam name="T">상태의 실체 클래스</typeparam>
    public interface IState<S, T> where S : Enum where T : class
    {
        public S TransID { get; }

        void OnEnterState(T controller);
        void OnUpdateState(T controller);
        void OnExitState(T controller);
    }
}

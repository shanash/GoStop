using UnityEngine;

namespace InGameState
{
    public class ScoreCalculationState : InGameStateBase
    {
        public ScoreCalculationState() : base(STATE.SCORE_CALCULATION) { }

        public override void OnEnterState(InGame controller)
        {
            base.OnEnterState(controller);

            // 패를 가져오기 전의 점수를 확인
            int lastScore = controller.CurrentPlayer.GetScore();
            // 패를 플레이어 점수영역으로 가져온다
            var list = controller.Area.PopEarnedCards();

            if (list.Count > 0)
            {
                controller.CurrentPlayer.AddScore(list);

                int score = controller.CurrentPlayer.GetScore();

                Debug.Log("score : " + score);
                if (score >= 3 && score > lastScore)
                {
                    // 고 or 스톱
                    // 일단은 승리로 처리하자
                }
            }
        }
    }
}

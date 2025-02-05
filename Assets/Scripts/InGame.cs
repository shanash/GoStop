using UnityEngine;
using System.Collections.Generic;
using InGameState;

public class InGame : MonoBehaviour
{
    [SerializeField]
    InputController inputController = null;

    [SerializeField]
    Transform localPlayer = null;

    [SerializeField]
    Transform leftPlayer = null;

    [SerializeField]
    Transform rightPlayer = null;

    [SerializeField]
    PlayAreaView playArea = null;

    protected InGameFSM _fsm { get; set; } = null;

    void Start()
    {
        _ = GameManager.Instance;
        GameManager.I.InitPlayers(localPlayer, leftPlayer, rightPlayer);
        GameManager.I.initPlayArea(playArea);
        GameManager.I.SetDel(inputController);

        List<InGameStateBase> list = new List<InGameStateBase>
        {
            new InitState(),
            new RoundStartState(),
            new PlayerTurnState(),
            new ScoreCalculationState(),
            new GoOrStopState(),
            new RoundEndState(),
            new GameEndState(),
        };

        _fsm = new InGameFSM(list, this, STATE.INIT);
    }

    void Update()
    {
        _fsm?.UpdateState();
    }

    public void OnClickFlipFirst()
    {
        GameManager.I.Deck.FlipFirst();
    }

    public HwatuCard OnClickPop()
    {
        return GameManager.I.Deck.Pop();
    }

    public void OnClickShuffle()
    {
        GameManager.I.Deck.Shuffle();
    }

    public void OnClickDraw()
    {
        //GameManager.I.Draw();
    }
}

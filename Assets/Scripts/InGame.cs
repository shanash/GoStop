using UnityEngine;
using System.Collections.Generic;
using InGameState;
using ToyLets.GenericPatterns;

public class InGame : MonoSingleton<InGame>
{
    [SerializeField]
    private InputController inputController = null;

    [SerializeField]
    private Transform trLocalPlayer = null;

    [SerializeField]
    private Transform trLeftPlayer = null;

    [SerializeField]
    private Transform trRightPlayer = null;

    [SerializeField]
    private PlayAreaView playAreaView = null;

    public HwatuDeck Deck { get; private set; } = null;
    public Player LocalPlayer { get; private set; } = null;
    public Player RemotePlayerLeft { get; private set; } = null;
    public Player RemotePlayerRight { get; private set; } = null;

    public PlayArea Area { get; private set; } = null;

    public InGameFSM FSM { get; protected set; } = null;

    public Player CurrentPlayer { get; private set; } = null;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void CallInstance()
    {
        _ = Instance;
    }

    protected override void Initialize()
    {
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

        FSM = new InGameFSM(list, this, STATE.INIT);
    }

    void Update()
    {
        FSM?.UpdateState();
    }

    public void OnClickFlipFirst()
    {
        Deck.FlipFirst();
    }

    public HwatuCard OnClickPop()
    {
        return Deck.Pop();
    }

    public void OnClickShuffle()
    {
        Deck.Shuffle();
    }

    public void OnClickDraw()
    {
        //GameManager.I.Draw();
    }

    public void OnEnterInit()
    {
        CreateDeck();
        InitPlayers(trLocalPlayer, trLeftPlayer, trRightPlayer);
        InitPlayArea(playAreaView);
        LocalPlayer.OnCardPlayed += Area.OnCardPlayed;
        inputController.Initialize(LocalPlayer);
    }

    public void OnEnterPlayer()
    {
        CurrentPlayer = LocalPlayer;
    }

    public void InitPlayers(Transform local, Transform left, Transform right)
    {
        LocalPlayer = new Player(local, true);
        LocalPlayer.Draw(Deck, 7);

        RemotePlayerLeft = new Player(left);
        RemotePlayerLeft.Draw(Deck, 7);

        RemotePlayerRight = new Player(right);
        RemotePlayerRight.Draw(Deck, 7);
    }

    public void InitPlayArea(PlayAreaView view)
    {
        Area = new PlayArea(view);
        Area.DisplayCards(Deck, 6);
    }

    public void CreateDeck()
    {
        List<HwatuCard> Cards = new List<HwatuCard>();
        // 각 월별로 카드 생성
        for (int month = 1; month <= 12; month++)
        {
            int index = 0;
            switch (month)
            {
                case 1:
                case 3:
                case 8:
                case 11:
                case 12:
                    Cards.Add(new HwatuCard(month, CardType.Kwang, $"{month:D2}_{index}"));
                    index++;
                    break;
            }

            switch (month)
            {
                case 2:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 12:
                    Cards.Add(new HwatuCard(month, CardType.Yeolggot, $"{month:D2}_{index}"));
                    index++;
                    break;
            }

            switch (month)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 9:
                case 10:
                case 12:
                    Cards.Add(new HwatuCard(month, CardType.Tti, $"{month:D2}_{index}"));
                    index++;
                    break;
            }

            switch (month)
            {
                case 11:
                case 12:
                    Cards.Add(new HwatuCard(month, CardType.SsangPi, $"{month:D2}_{index}"));
                    index++;
                    break;
            }

            if (month != 12)
            {
                Cards.Add(new HwatuCard(month, CardType.Pi, $"{month:D2}_{index}"));
                index++;

                Cards.Add(new HwatuCard(month, CardType.Pi, $"{month:D2}_{index}"));
            }
        }

        Deck = new HwatuDeck(Cards);

        Deck.Shuffle();
        Deck.Show = true;
    }
}

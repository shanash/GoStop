using UnityEngine;
using System.Collections.Generic;
using InGameState;

public class InGame : MonoBehaviour
{
    [SerializeField]
    InputController inputController = null;

    [SerializeField]
    Transform trLocalPlayer = null;

    [SerializeField]
    Transform trLeftPlayer = null;

    [SerializeField]
    Transform trRightPlayer = null;

    [SerializeField]
    PlayAreaView playAreaView = null;

    public HwatuDeck Deck = null;
    public Player LocalPlayer = null;
    public Player RemotePlayerLeft = null;
    public Player RemotePlayerRight = null;

    public PlayArea Area = null;

    protected InGameFSM _fsm { get; set; } = null;

    void Start()
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

        _fsm = new InGameFSM(list, this, STATE.INIT);
    }

    void Update()
    {
        _fsm?.UpdateState();
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

    public void Init()
    {
        CreateDeck();
        InitPlayers(trLocalPlayer, trLeftPlayer, trRightPlayer);
        InitPlayArea(playAreaView);
        LocalPlayer.OnCardPlayed += Area.OnCardPlayed;
        inputController.Initialize(LocalPlayer);
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

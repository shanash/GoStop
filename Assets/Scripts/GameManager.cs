using System.Collections.Generic;
using ToyLets.Util;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public HwatuDeck Deck = null;
    public Player LocalPlayer = null;
    public Player RemotePlayerLeft = null;
    public Player RemotePlayerRight = null;

    public PlayArea Area = null;

    GameManager() { }

    protected override void Initialize()
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

    public void InitPlayers(Transform local, Transform left, Transform right)
    {
        LocalPlayer = new Player(local, true);
        LocalPlayer.Draw(7);

        RemotePlayerLeft = new Player(left);
        RemotePlayerLeft.Draw(7);

        RemotePlayerRight = new Player(right);
        RemotePlayerRight.Draw(7);
    }

    public void initPlayArea(PlayAreaView view)
    {
        Area = new PlayArea(view);
        Area.DisplayCards(6);
    }

    public void SetDel(InputController input)
    {
        LocalPlayer.OnCardPlayed += Area.OnCardPlayed;
        input.Initialize(LocalPlayer);
    }
}

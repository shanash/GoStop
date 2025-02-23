using System.Collections.Generic;

public class PlayerModel
{
    // 플레이하는 자신인지 아닌지
    public bool IsLocalPlayer { get; set; } = false;

    // 손에 든 화투패
    public List<HwatuCard> HandCards { get; private set; } = null;

    // 포인터로 가리킨 화투패
    public HwatuCard SelectedCard { get; private set; } = null;

    // 내 점수 패
    private Dictionary<CardType, List<HwatuCard>> _scoreCards { get; set; } = null;

    public PlayerModel()
    {
        Reset();
    }

    public void Reset()
    {
        _scoreCards = new Dictionary<CardType, List<HwatuCard>>
        {
            { CardType.Kwang, new List<HwatuCard>() },
            { CardType.BiKwang, new List<HwatuCard>() },
            { CardType.Yeolggot, new List<HwatuCard>() },
            { CardType.Tti, new List<HwatuCard>() },
            { CardType.Hongdan, new List<HwatuCard>() },
            { CardType.Chodan, new List<HwatuCard>() },
            { CardType.Cheongdan, new List<HwatuCard>() },
            { CardType.SsangPi, new List<HwatuCard>() },
            { CardType.Pi, new List<HwatuCard>() },
        };

        if (HandCards == null)
        {
            HandCards = new List<HwatuCard>();
        }
        
        HandCards.Clear();
        SelectedCard = null;
    }

    public void Add(HwatuCard card)
    {
        HandCards.Add(card);
    }

    public void Select(HwatuCard card)
    {
        SelectedCard = card;
    }

    public void AddScoreCard(HwatuCard card)
    {
        foreach (var kvCardList in _scoreCards)
        {
            if ((card.Model.Type & kvCardList.Key) == kvCardList.Key)
            {
                kvCardList.Value.Add(card);
            }
        }
    }

    public List<HwatuCard> GetScoreCards(CardType type)
    {
        return _scoreCards[type];
    }
}

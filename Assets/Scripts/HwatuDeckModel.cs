using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HwatuDeckModel
{
    const int NUMBER = 7;

    public List<HwatuCard> Cards { get; set; }
    HwatuDeckView view = null;

    public Vector3 Position
    {
        get
        {
            return position;
        }
        set
        {
            if (view != null)
            {
                view.transform.position = value;
            }
            position = value;
        }
    }
    Vector3 position = Vector3.zero;

    public bool Show
    {
        get => show;
        set
        {
            if (value)
            {
                if (view == null)
                {
                    var go = new GameObject("HwatuDeck");
                    view = go.AddComponent<HwatuDeckView>();
                    view.transform.position = position;
                    view.Initialize(Cards);

                }
            }

            view.gameObject.SetActive(value);
            show = value;
        }
    }
    bool show = false;

    public HwatuDeckModel(List<HwatuCard> cards)
    {
        Cards = cards;
    }

    public void FlipFirst()
    {
        if (Cards.Count > 0)
        {
            Cards[0].Flip();
        }
    }

    public HwatuCard Pop()
    {
        if (Cards.Count == 0)
        {
            return null;
        }

        HwatuCard topCard = Cards[0];
        Cards.RemoveAt(0);

        return topCard;
    }

    public void Shuffle()
    {
        System.Random rng = new System.Random();
        for (int i = 0; i < NUMBER; i++)
        {
            int shuffleMethod = rng.Next(0, 2); // 0 또는 1을 랜덤으로 선택

            if (shuffleMethod == 0)
            {
                SevenCardShuffle();
            }
            else
            {
                HinduShuffle();
            }
        }

        view?.UpdateView(Cards);
    }

    void SevenCardShuffle()
    {
        // 일곱 개의 더미를 저장할 리스트의 리스트
        List<List<HwatuCard>> piles = new List<List<HwatuCard>>();

        // 초기화
        for (int i = 0; i < NUMBER; i++)
        {
            piles.Add(new List<HwatuCard>());
        }

        // 카드 덱이 떨어질 때까지 반복
        while (Cards.Any())
        {
            for (int i = 0; i < NUMBER && Cards.Any(); i++)
            {
                piles[i].Add(Cards[0]);
                Cards.RemoveAt(0);
            }
        }

        // 첫 번째, 세 번째, 다섯 번째, 일곱 번째 더미 합치기
        for (int i = 0; i < NUMBER; i += 2)
        {
            Cards.AddRange(piles[i]);
        }
        // 두 번째, 네 번째, 여섯 번째 더미 합치기
        for (int i = 1; i < NUMBER; i += 2)
        {
            Cards.AddRange(piles[i]);
        }
    }

    public void HinduShuffle()
    {
        System.Random random = new System.Random();

        for (int i = 0; i < NUMBER; i++)
        {
            // tempDeck에서 임의의 수의 카드를 선택하여 원본 덱의 아래로 옮김
            int count = random.Next(0, Cards.Count);
            List<HwatuCard> tempDeck = Cards.Skip(count).ToList();
            Cards = Cards.Take(count).ToList();

            int j = 0;
            while (tempDeck.Any() && j < 100)
            {
                // tempDeck에서 임의의 수의 카드를 선택하여 원본 덱의 아래로 옮김
                count = random.Next(1, tempDeck.Count+1);
                List<HwatuCard> selectedCards = tempDeck.Take(count).ToList();
                tempDeck = tempDeck.Skip(count).ToList();

                // 선택된 카드들을 원본 덱의 앞에 추가
                Cards.InsertRange(0, selectedCards);
                j++;
            }
        }
    }

    public override string ToString()
    {
        string result = string.Empty;
        foreach (var c in Cards)
        {
            result += $"{c.Model.Month} {c.Model.Type}\n";
        }

        return result;
    }
}

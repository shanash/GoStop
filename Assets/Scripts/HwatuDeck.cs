using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HwatuDeck
{
    const int NUMBER = 7;

    public List<HwatuCard> Cards { get; set; }
    List<Action> shuffles = new List<Action>();

    public HwatuDeck()
    {
        Cards = new List<HwatuCard>();

        // 각 월별로 카드 생성
        for (int month = 1; month <= 12; month++)
        {
            switch (month)
            {
                case 1: case 3: case 8: case 11: case 12:
                    Cards.Add(new HwatuCard(month, CardType.Kwang));
                    break;
            }

            switch (month)
            {
                case 2: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 12:
                    Cards.Add(new HwatuCard(month, CardType.Yeolggot));
                    break;
            }

            switch (month)
            {
                case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 9: case 10: case 12:
                    Cards.Add(new HwatuCard(month, CardType.Tti));
                    break;
            }

            Cards.Add(new HwatuCard(month, CardType.Pi));

            if (month != 12)
            {
                Cards.Add(new HwatuCard(month, CardType.Pi));
            }
        }

        shuffles.Add(SevenCardShuffle);
        shuffles.Add(HinduShuffle);
    }

    public void Shuffle()
    {
        System.Random rng = new System.Random();
        for (int i = 0; i < NUMBER; i++)
        {
            int index = rng.Next(0, shuffles.Count);
            shuffles[index]();
        }
    }

    void SevenCardShuffle()
    {
        Debug.Log("SevenCardShuffle");

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
        Debug.Log("HinduShuffle");
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
}

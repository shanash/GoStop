using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 바닥(플레이 영역)
public class PlayAreaModel
{
    public Dictionary<int, List<HwatuCard>> Cards = new Dictionary<int, List<HwatuCard>>();

    // 바닥에 놓을 수 있는 포인트들
    private int _countPlayAreaPoint = 0;

    public PlayAreaModel(int countPlayAreaPoint)
    {
        _countPlayAreaPoint = countPlayAreaPoint;
    }

    /// <summary>
    /// 패 한장을 빈 바닥에 던져놓는다
    /// </summary>
    /// <param name="card"></param>
    public void AddNew(HwatuCard card)
    {
        // 현재 빈곳이 몇군데인지 확인
        int realCount = _countPlayAreaPoint - Cards.Count;

        // 빈곳중에서 랜덤으로 픽
        int random = Random.Range(0, realCount);

        // 패 놓을 곳의 인덱스 지역변수
        int index = 0;

        // 패 인덱스가 포인트 갯수를 넘어가면 안된다
        while (index < _countPlayAreaPoint)
        {
            // 해당 인덱스가 비어있으면
            if (!Cards.ContainsKey(index))
            {
                // 그 인덱스를 랜덤이 지정했으면
                if (random <= 0)
                {
                    break;
                }
                // 아직 남았다면
                else
                {
                    // 랜덤을 깎아줌
                    random--;
                }
            }
            index++;
        }

        if (index == _countPlayAreaPoint)
        {
            Debug.LogError("더 이상 카드를 추가할 공간이 없습니다");
            return;
        }

        // 찾은 인덱스에 카드를 리스트로 새로 추가
        Cards.Add(index, new List<HwatuCard>() { card });
    }

    public bool IsSameMonth(HwatuCard card)
    {
        return default(List<HwatuCard>) != Cards.Values.FirstOrDefault(list => list.Find(c => c.Model.Month == card.Model.Month) != null);
    }

    public void AttackCard(HwatuCard card)
    {
        var list = Cards.Values.FirstOrDefault(list => list.Find(c => c.Model.Month == card.Model.Month) != null);
        list.Add(card);
    }
}

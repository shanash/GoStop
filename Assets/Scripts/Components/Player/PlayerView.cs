using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    GameObject Hands = null;

    List<HwatuCardView> views = null;

    public void UpdateView(PlayerModel model)
    {
        List<HwatuCard> cards = model.Cards;
        int cardCount = cards.Count;
        float fanAngle = Mathf.Min(50f, cardCount * 5f); // 부채꼴의 전체 각도 제한 (예: 최대 60도)
        float angleStep = fanAngle / Mathf.Max(1, cardCount - 1); // 각 카드 간의 각도 차이
        float startAngle = -fanAngle / 2f; // 첫 번째 카드의 시작 각도

        float radius = 2.0f; // 부채꼴의 반지름 증가 (예: 1.0f)

        for (int i = 0; i < cardCount; i++)
        {
            HwatuCard card = cards[i];
            HwatuCardView view = cards[i].View;

            card.SetParent(Hands.transform);

            // 각 카드의 각도 계산
            float angle = startAngle + i * angleStep;
            float radian = angle * Mathf.Deg2Rad;

            // 각 카드의 위치 계산
            float x = radius * Mathf.Sin(radian);
            float z = radius * Mathf.Cos(radian);

            card.LocalPosition = new Vector3(Hands.transform.localPosition.x + x, Hands.transform.localPosition.y + 0.02f * i, Hands.transform.localPosition.z + z);
            card.LocalRotation = Quaternion.Euler(0, angle, 0); // 카드의 Y축 회전 각도 설정
            card.State = model.IsLocalPlayer ? CardState.FaceUp : CardState.FaceDown;
            card.Show = true;

            // Hierarchy 순서를 List 순서에 맞게 조정
            view.transform.SetSiblingIndex(i);
        }
    }

    public PlayerView Instantiate(Transform parent = null)
    {
        return Instantiate(this, parent);
    }
}

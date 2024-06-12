using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    GameObject Hands = null;

    List<HwatuCardView> views = null;

    public void UpdateView(List<HwatuCard> cards)
    {
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

            card.Position = new Vector3(Hands.transform.position.x + x, Hands.transform.position.y + 0.01f * i, Hands.transform.position.z + z);
            card.Rotation = Quaternion.Euler(0, angle, 0); // 카드의 Y축 회전 각도 설정
            card.State = CardState.FaceUp;
            card.Show = true;

            // Hierarchy 순서를 List 순서에 맞게 조정
            view.transform.SetSiblingIndex(i);

            // 카드 뷰 위치 및 회전 설정
            //view.transform.localPosition = card.Position;
            //view.transform.localRotation = card.Rotation;
        }
    }

    public PlayerView Instantiate(Transform parent = null)
    {
        return Instantiate(this, parent);
    }
}

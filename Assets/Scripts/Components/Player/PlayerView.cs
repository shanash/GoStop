using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerView : MonoBehaviour
{
    public Vector3 enlargedScale = new Vector3(1.2f, 1.2f, 1.2f); // 커졌을 때의 크기
    const float zOffset = 0.1f; // 커질 때 Z축으로 이동할 거리 (반대로 이동)
    const float EnlargeDuration = 0.2f; // 크기 변환 지속 시간

    [SerializeField]
    private Transform hands = null;
    [SerializeField]
    private Transform score = null;
    private Dictionary<CardType, Transform> scores = null;

    private List<HwatuCardView> handViews { get; set; }
    private List<HwatuCardView> scoreViews { get; set; }

    private Dictionary<GameObject, Vector3> originalScales = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Coroutine> resizeCoroutines = new Dictionary<GameObject, Coroutine>();

    private HwatuCardView selectedCard = null;

    void Awake()
    {
        handViews = new List<HwatuCardView>();
        scoreViews = new List<HwatuCardView>();

        scores = new Dictionary<CardType, Transform>();

        foreach (CardType value in Enum.GetValues(typeof(CardType)))
        {
            string name = value.ToString();
            Transform child = score.Find(name);
            if (child != null)
            {
                scores.Add(value, child);
            }
        }
    }

    public void UpdateView(PlayerModel model)
    {
        handViews.Clear();

        List<HwatuCard> handCards = model.HandCards;
        int cardCount = handCards.Count;
        float fanAngle = Mathf.Min(50f, cardCount * 5f); // 부채꼴의 전체 각도 제한 (예: 최대 60도)
        float angleStep = fanAngle / Mathf.Max(1, cardCount - 1); // 각 카드 간의 각도 차이
        float startAngle = -fanAngle / 2f; // 첫 번째 카드의 시작 각도

        float radius = 3.0f; // 부채꼴의 반지름 증가 (예: 1.0f)

        for (int i = 0; i < cardCount; i++)
        {
            HwatuCard card = handCards[i];
            HwatuCardView view = handCards[i].View;
            handViews.Add(view);

            card.SetParent(hands);

            // 각 카드의 각도 계산
            float angle = startAngle + i * angleStep;
            float radian = angle * Mathf.Deg2Rad;

            // 각 카드의 위치 계산
            float x = radius * Mathf.Sin(radian);
            float z = radius * Mathf.Cos(radian);

            card.LocalPosition = new Vector3(x, 0.02f * i, z);
            card.LocalRotation = Quaternion.Euler(0, angle, 0); // 카드의 Y축 회전 각도 설정
            card.State = model.IsLocalPlayer ? CardState.FaceUp : CardState.FaceDown;
            card.Show = true;

            // Hierarchy 순서를 List 순서에 맞게 조정
            view.transform.SetSiblingIndex(i);
        }

        scoreViews.Clear();
        List<CardType> keyCardTypes = new List<CardType>
        {
            CardType.Kwang,
            CardType.Yeolggot,
            CardType.Tti,
            CardType.Pi,
        };

        foreach (CardType cardType in keyCardTypes)
        {
            if (!scores.ContainsKey(cardType))
            {
                continue;
            }
        
            foreach (HwatuCard card in model.ScoreCards[cardType])
            {
                int count = scores[cardType].childCount;
                HwatuCardView view = card.View;
                scoreViews.Add(view);

                card.SetParent(scores[cardType]);
                card.LocalPosition = new Vector3(count * 0.24f, count * 0.001f, 0);
            }
        }
    }

    public PlayerView Instantiate(Transform parent = null)
    {
        return Instantiate(this, parent);
    }

    void StartResize(GameObject target, Vector3 targetScale, Vector3 targetPosition, float duration)
    {
        if (resizeCoroutines.ContainsKey(target))
        {
            StopCoroutine(resizeCoroutines[target]);
        }
        resizeCoroutines[target] = StartCoroutine(SmoothResizeAndMove(target, targetScale, targetPosition, duration));
    }

    void ResetCardSize(GameObject target)
    {
        if (resizeCoroutines.ContainsKey(target))
        {
            StopCoroutine(resizeCoroutines[target]);
        }
        StartResize(target, originalScales[target], originalPositions[target], EnlargeDuration);
    }

    IEnumerator SmoothResizeAndMove(GameObject target, Vector3 targetScale, Vector3 targetPosition, float duration)
    {
        Vector3 initialScale = target.transform.localScale;
        Vector3 initialPosition = target.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            target.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            target.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.transform.localScale = targetScale;
        target.transform.position = targetPosition;
        resizeCoroutines.Remove(target);
    }

    public void Select(HwatuCardView view)
    {
        if (selectedCard == view)
        {
            return;
        }

        if (selectedCard != null)
        {
            ResetSelected();
        }

        selectedCard = view;

        GameObject selectedGo = selectedCard.gameObject;

        // 원래 위치와 크기를 저장합니다.
        if (!originalScales.ContainsKey(selectedGo))
        {
            originalScales[selectedGo] = selectedGo.transform.localScale;
            originalPositions[selectedGo] = selectedGo.transform.position;
        }

        Vector3 targetPosition = originalPositions[selectedGo] + new Vector3(0, 0, zOffset);
        StartResize(selectedGo, enlargedScale, targetPosition, EnlargeDuration);
    }

    public void ResetSelected()
    {
        if (selectedCard == null)
            return;

        ResetCardSize(selectedCard.gameObject);
        selectedCard = null;
    }

    public void Remove(HwatuCardView view)
    {
        selectedCard = null;
        GameObject target = view.gameObject;
        if (resizeCoroutines.ContainsKey(target))
        {
            StopCoroutine(resizeCoroutines[target]);
            resizeCoroutines.Remove(target);
        }
        handViews.Remove(view);
        originalScales.Remove(target);
        originalPositions.Remove(target);
    }
}

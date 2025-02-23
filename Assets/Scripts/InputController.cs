using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public LayerMask cardLayerMask; // 손패 카드가 할당된 레이어
    public Vector3 enlargedScale = new Vector3(1.2f, 1.2f, 1.2f); // 커졌을 때의 크기
    public float enlargeDuration = 0.2f; // 크기 변환 지속 시간
    public float zOffset = -0.1f; // 커질 때 Z축으로 이동할 거리 (반대로 이동)

    InputAction clickAction;
    InputAction pointAction;
    Dictionary<GameObject, Vector3> originalScales = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Coroutine> resizeCoroutines = new Dictionary<GameObject, Coroutine>();

    private Player _localPlayer;

    void Awake()
    {
        var inputActionAsset = GetComponent<PlayerInput>().actions;
        clickAction = inputActionAsset["Click"];
        pointAction = inputActionAsset["Point"];

        clickAction.performed += OnMouseClick;
    }

    void OnEnable()
    {
        pointAction.Enable();
    }

    void OnDisable()
    {
        pointAction.Disable();
    }

    void Update()
    {
        if (InGame.I.FSM.CurrentState.TransID != InGameState.STATE.PLAYER_TURN)
        {
            return;
        }

        if (!InGame.I.CurrentPlayer.IsLocal)
        {
            return;
        }

        if (_localPlayer.PlayedCard)
        {
            return;
        }

        Vector2 mousePosition = pointAction.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, cardLayerMask))
        {
            GameObject hitObject = hit.collider.gameObject;
            _localPlayer.SelectCard(hitObject);
        }
        else
        {
            _localPlayer.ResetSelectedCard();
        }
    }

    public void Initialize(Player playerInstance)
    {
        _localPlayer = playerInstance;
    }

    void OnMouseClick(InputAction.CallbackContext context)
    {
        if (InGame.I.FSM.CurrentState.TransID != InGameState.STATE.PLAYER_TURN)
        {
            return;
        }

        if (!InGame.I.CurrentPlayer.IsLocal)
        {
            return;
        }

        if (_localPlayer.IsSelectedCard())
        {
            _localPlayer.PlayCard();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUIController : MonoBehaviour
{
    private UIDocument _doc;  // 요 스크립트와 같은 게임 오브젝트에 있는 UI Document 컴포넌트 할당용
    private PlayerInput playerInput;
    private PlayerController playerController;

    private VisualElement _leftBtn;
    private VisualElement _rightBtn;
    private VisualElement _settingBtn;
    private VisualElement _skill1Btn;
    private VisualElement _skill2Btn;
    private VisualElement _skill3Btn;
    private VisualElement _skill4Btn;
    private VisualElement _skill5Btn;
    private VisualElement _attackArea;
    private void Awake()
    {
        _doc = GetComponent<UIDocument>();

        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        playerController = playerInput.GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        // 각 버튼의 가져옴
        _leftBtn = _doc.rootVisualElement.Q<VisualElement>("LeftBtn");
        _rightBtn = _doc.rootVisualElement.Q<VisualElement>("RightBtn");
        _skill1Btn = _doc.rootVisualElement.Q<VisualElement>("skill1Btn");
        _skill2Btn = _doc.rootVisualElement.Q<VisualElement>("skill2Btn");
        _skill3Btn = _doc.rootVisualElement.Q<VisualElement>("skill3Btn");
        _skill4Btn = _doc.rootVisualElement.Q<VisualElement>("skill4Btn");
        _skill5Btn = _doc.rootVisualElement.Q<VisualElement>("skill5Btn");

        SetPlayerButton();
    }

    private void SetPlayerButton()
    {
        _leftBtn.RegisterCallback<FocusEvent>(e =>
        {
            Debug.Log(2);
            playerInput.MoveInput(Vector2.left);
        });

        _leftBtn.RegisterCallback<MouseUpEvent>(e =>
        {
            Debug.Log(3);
            playerInput.MoveInput(Vector2.zero);
        });

        _rightBtn.RegisterCallback<FocusEvent>(e =>
        {
            Debug.Log(2);
            playerInput.MoveInput(Vector2.right);
        });

        _rightBtn.RegisterCallback<MouseUpEvent>(e =>
        {
            playerInput.MoveInput(Vector2.zero);
        });

        _skill1Btn.RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log(1);
            playerInput.TeleportationInput();
        });

        _skill4Btn.RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log(1);
            playerController.ChangeState(PlayerStateType.Dash);
            playerInput.DashInput();
        });

        _skill5Btn.RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log(1);
            playerInput.JumpInput();
        }); 
    }
}

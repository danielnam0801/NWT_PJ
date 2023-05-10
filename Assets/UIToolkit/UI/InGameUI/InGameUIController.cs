using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUIController : MonoBehaviour
{
    public GameObject settingUI;
    private PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput = GameManager.instance.Target.GetComponent<PlayerInput>();

        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;
        Button setting = root.Q<Button>("settingBtn");
        Button upBtn = root.Q<Button>("UpBtn");
        Button rightBtn = root.Q<Button>("RightBtn");
        Button leftBtn = root.Q<Button>("LeftBtn");

        setting.RegisterCallback<ClickEvent>(e =>
        {
            settingUI.SetActive(true);
        });

        upBtn.AddManipulator(new ClickManipulator(() =>
        {
            playerInput.JumpInput();
        }));

        rightBtn.AddManipulator(new ClickManipulator(() =>
        {
            Debug.Log(10);
            playerInput.MoveInput(Vector2.right);
            playerInput.OnMouseUpAction += playerInput.StopMoveInput;
        }, () =>
        {

        }));
    }
    //private UIDocument _doc;  // 요 스크립트와 같은 게임 오브젝트에 있는 UI Document 컴포넌트 할당용

    //private Button _leftBtn;
    //private Button _rightBtn;
    //private Button _settingBtn;
    //private Button _skill1Btn;
    //private Button _skill2Btn;
    //private Button _skill3Btn;
    //private Button _skill4Btn;
    //private Button _skill5Btn;
    //private void Awake()
    //{
    //    _doc = GetComponent<UIDocument>();

    //    // 각 버튼의 가져옴
    //    _leftBtn = _doc.rootVisualElement.Q<Button>("LeftBtn");
    //    _rightBtn = _doc.rootVisualElement.Q<Button>("RightBtn");
    //    _skill1Btn = _doc.rootVisualElement.Q<Button>("");
    //    _skill2Btn = _doc.rootVisualElement.Q<Button>("");
    //    _skill3Btn = _doc.rootVisualElement.Q<Button>("");
    //    _skill4Btn = _doc.rootVisualElement.Q<Button>("");
    //    _skill5Btn = _doc.rootVisualElement.Q<Button>("");
    //    _leftBtn.clicked += LeftBtnClick;
    //    _rightBtn.clicked += RightBtnClick;
    //}

    //private void LeftBtnClick()
    //{
    //    Debug.Log("LeftButtonClicked");
    //}
    //private void RightBtnClick()
    //{
    //    Debug.Log("RightBtnClick");
    //}

}

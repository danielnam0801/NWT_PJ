using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUIController : MonoBehaviour
{
    public GameObject settingUI;
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GameManager.instance.Target.GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;
        VisualElement setting = root.Q<VisualElement>("settingBtn");
        VisualElement upBtn = root.Q<VisualElement>("UpBtn");
        VisualElement rightBtn = root.Q<VisualElement>("RightBtn");
        VisualElement leftBtn = root.Q<VisualElement>("LeftBtn");

        //setting.RegisterCallback<ClickEvent>(e =>
        //{
        //    //settingUI.SetActive(true);
        //    Debug.Log(10);
        //});

        setting.AddManipulator(new ClickManipulator(() =>
        {
            Debug.Log(1);
        }, () =>
        {
            Debug.Log(2);
        }));

        //upBtn.AddManipulator(new ClickManipulator(() =>
        //{
        //    playerInput.JumpInput();
        //}));

        //rightBtn.AddManipulator(new ClickManipulator(() =>
        //{
        //    Debug.Log(10);
        //    playerInput.MoveInput(Vector2.right);
        //    playerInput.OnMouseUpAction += playerInput.StopMoveInput;
        //}, () =>
        //{

        //}));
    }
    //private UIDocument _doc;  // �� ��ũ��Ʈ�� ���� ���� ������Ʈ�� �ִ� UI Document ������Ʈ �Ҵ��

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

    //    // �� ��ư�� ������
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

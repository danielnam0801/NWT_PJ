using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUIController : MonoBehaviour
{
    private UIDocument _doc;  // �� ��ũ��Ʈ�� ���� ���� ������Ʈ�� �ִ� UI Document ������Ʈ �Ҵ��

    private Button _leftBtn;
    private Button _rightBtn;
    private Button _settingBtn;
    private Button _skill1Btn;
    private Button _skill2Btn;
    private Button _skill3Btn;
    private Button _skill4Btn;
    private Button _skill5Btn;
    private void Awake()
    {
        _doc = GetComponent<UIDocument>();

        // �� ��ư�� ������
        _leftBtn = _doc.rootVisualElement.Q<Button>("LeftBtn");
        _rightBtn = _doc.rootVisualElement.Q<Button>("RightBtn");
        _skill1Btn = _doc.rootVisualElement.Q<Button>("");
        _skill2Btn = _doc.rootVisualElement.Q<Button>("");
        _skill3Btn = _doc.rootVisualElement.Q<Button>("");
        _skill4Btn = _doc.rootVisualElement.Q<Button>("");
        _skill5Btn = _doc.rootVisualElement.Q<Button>("");
        _leftBtn.clicked += LeftBtnClick;
        _rightBtn.clicked += RightBtnClick;
    }

    private void LeftBtnClick()
    {
        Debug.Log("LeftButtonClicked");
    }
    private void RightBtnClick()
    {
        Debug.Log("RightBtnClick");
    }

}

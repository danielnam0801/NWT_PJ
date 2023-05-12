using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUIController : MonoBehaviour
{
    public GameObject settingUI;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();    
    }

    private void OnEnable()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;
        Button setting = root.Q<Button>("settingBtn");
        setting.RegisterCallback<ClickEvent>(e =>
        {
            settingUI.SetActive(true);
        });

        //Slider slider = root.Q<Slider>("Slider");
        //slider = GetComponentInChildren<Slider>();

        VisualElement leftBtn = root.Q<VisualElement>("LeftBtn");
        leftBtn.AddManipulator(new ClickManipulator(() =>
        {
            playerInput.MoveInput(Vector2.left);
        }));

        VisualElement rightBtn = root.Q<VisualElement>("RightBtn");
        rightBtn.AddManipulator(new ClickManipulator(() =>
        {
            playerInput.MoveInput(Vector2.right);
        }));

        VisualElement drawPanel = root.Q<VisualElement>("DrawPanel");
        drawPanel.AddManipulator(new ClickManipulator(() =>
        {
            Debug.Log(1);
            DrawManager.Instance.StartDraw = true;
        }));

        Button jumpBtn = root.Q<Button>("skill3Btn");
        jumpBtn.RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log(1);
            playerInput.JumpInput();
        });

        Button teloportationBtn = root.Q<Button>("skill1Btn");
        teloportationBtn.RegisterCallback<ClickEvent>(e =>
        {
            playerInput.TeleportationInput();
        });

        Button settingBtn = root.Q<Button>("settingBtn");
        settingBtn.RegisterCallback<ClickEvent>(e =>
        {
            settingUI.SetActive(true);
            TimeManager.Instance.SetTimeScale(0);
            gameObject.SetActive(false);
        });
    }
}

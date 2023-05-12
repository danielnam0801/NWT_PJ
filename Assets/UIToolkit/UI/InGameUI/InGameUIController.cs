using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUIController : MonoBehaviour
{

    public GameObject settingUI;
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
        leftBtn.AddManipulator(new ClickManipulator(() => Debug.Log(1), () => Debug.Log(2)));

    }
}

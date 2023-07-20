using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUIController : MonoBehaviour
{

    public GameObject settingUI;
    public GameObject hpTestSlider;
    UnityEngine.UI.Slider hpSlider;

    TimeValue tv = new TimeValue();
    float timeValue;
    public float value = 50;



    private void Start()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;

        Button setting = root.Q<Button>("settingBtn");
        setting.RegisterCallback<ClickEvent>(e =>
        {
            settingUI.SetActive(true);
        });

        Label timer = root.Q<Label>("Timer");
        //timer.text = "Time: " + tv.ToString();



        //var csharpField = new IntegerField("C# Field");
        //csharpField.SetEnabled(true);
        //csharpField.AddToClassList("some-styled-field");
        //csharpField.value = uxmlField.value;
        //container.Add(csharpField);
        VisualElement hpSlider = root.Q<VisualElement>("HealthBar");
        hpSlider.RegisterCallback<ChangeEvent<int>>(e =>
        {
            value = e.newValue;
            Debug.Log($"newvalue{e.newValue}");
            Debug.Log(value);
            hpSlider.style.width = value;
        });
        //slider.RegisterValueChangedCallback(v =>
        //{

        //});

        //leftBtn.AddManipulator(new ClickManipulator(() => Debug.Log(1), () => Debug.Log(2)));
    }

    //IEnumerator TimeTick()
    //{
    //    yield return ;
    //}

    private void Update()
    {
        value = hpSlider.value;
    }

    public void SetHPSlider(float value)
    {
        //
    }
}

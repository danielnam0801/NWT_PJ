using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Document : MonoBehaviour
{
    private void OnEnable()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;
        VisualElement background = root.Q("Background");
        Button startBtn = root.Q<Button>("StartBtn");

        startBtn.RegisterCallback<ClickEvent>(e =>
        {
            background.style.backgroundColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            Debug.Log("startBtnClick");
        });
    }
}

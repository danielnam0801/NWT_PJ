using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    private void OnEnable()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;
        
        VisualElement background = root.Q("Background");
        Button returnBtn = root.Q<Button>("returnBtn");
        Button newGameBtn = root.Q<Button>("newGameBtn");
        Button resetCutSceneBtn = root.Q<Button>("resetCutSceneBtn");
        Button exitBtn = root.Q<Button>("exitBtn");
        returnBtn.RegisterCallback<ClickEvent>(e =>
        {
            gameObject.SetActive(false);
        });

        newGameBtn.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene("");
        });

        resetCutSceneBtn.RegisterCallback<ClickEvent>(e =>
        {

        });

        exitBtn.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }
}

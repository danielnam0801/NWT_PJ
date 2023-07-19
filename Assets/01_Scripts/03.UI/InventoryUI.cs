using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[Serializable]
public enum InventoryClickEventType
{
    Create = 1,
    Remove
}

public class InventoryUI : MonoBehaviour
{
    private UIDocument document;

    private PlayerAttack attack;
    private PlayerInventory inventory;

    private UnityEvent<ShapeType> clickAction;

    public UnityEvent<ShapeType> CreateShapeEvent;
    public UnityEvent<ShapeType> RemoveShapeEvent;
    public List<Sprite> ShapeImages = new List<Sprite>();

    private void Awake()
    {
        document = transform.Find("UI").GetComponent<UIDocument>();

        attack = FindObjectOfType<PlayerAttack>();
        inventory = FindObjectOfType<PlayerInventory>();
    }

    public void SetActive(bool value)
    {
        document.gameObject.SetActive(value);

        if(value)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            SetElementInfo();
            DrawManager.Instance.isUIMouse = true;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            DrawManager.Instance.isUIMouse = false;
        }
    }

    public void SetClickEvent(int eventType)
    {
        switch (eventType)
        {
            case (int)InventoryClickEventType.Create:
                clickAction = CreateShapeEvent;
                break;
            case (int)InventoryClickEventType.Remove:
                clickAction= RemoveShapeEvent;
                break;
        }
    }

    public UnityEvent<ShapeType> SetClickAction(UnityEvent<ShapeType> action)
    {
        UnityEvent<ShapeType> beforeAction = clickAction;
        clickAction = action;
        return beforeAction;
    }

    private void SetElementInfo()
    {
        VisualElement root = document.rootVisualElement;
        List<VisualElement> items = root.Query<VisualElement>("item").ToList();
        Debug.Log(items.Count);
        Debug.Log(inventory.HaveShapes.Count);

        for(int i = 0; i < items.Count; i++)
        {
            ShapeType shape = inventory.HaveShapes[i];

            if (shape == ShapeType.Default)
            {
                VisualElement image = items[i].Q<VisualElement>("image");
                StyleBackground background = new StyleBackground();
                image.style.backgroundImage = background;
            }
            else
            {
                items[i].Q<VisualElement>("image").style.backgroundImage = new StyleBackground(ShapeImages[(int)shape]);    

                items[i].RegisterCallback<ClickEvent>(e =>
                {
                    clickAction?.Invoke(shape);
                    SetActive(false);
                });
            }
        }
    }
}

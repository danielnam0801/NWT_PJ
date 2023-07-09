using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    private UIDocument document;

    private PlayerAttack attack;
    private PlayerInventory inventory;

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
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
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
                continue;

            //item.Q<VisualElement>("image").style.backgroundImage.value.sprite = 

                
            items[i].RegisterCallback<ClickEvent>(e =>
            {
                Debug.Log(shape);
                attack.CreateShpae(shape);
                SetActive(false);
            });
        }
    }
}

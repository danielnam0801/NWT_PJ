using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public List<ShapeType> HaveShapes = new List<ShapeType>();
    public UnityEvent HaveShapesOnChanged;

    public void AddShape(ShapeType shape)
    {
        HaveShapes.Add(shape);
        HaveShapesOnChanged?.Invoke();
    }

    public void RemoveShape(ShapeType type)
    {
        if(HaveShapes.Contains(type))
        {
            HaveShapes.Remove(type);
            HaveShapesOnChanged?.Invoke();
        }
        else
        {
            Debug.Log($"{type} : 보유하지 않은 도형");
        }
    }
}

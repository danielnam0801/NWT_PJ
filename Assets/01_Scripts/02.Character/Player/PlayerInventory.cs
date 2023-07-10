using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private int maxShape = 3;
    public List<ShapeType> HaveShapes = new List<ShapeType>();

    public UnityEvent FullCountShapeAction;

    public void AddShape(ShapeType shape)
    {
        if(HaveShapes.Count < maxShape)
        {
            HaveShapes.Add(shape);
        }
        else
        {
            FullCountShapeAction?.Invoke();
            HaveShapes.Add(shape);
        }
    }

    public void RemoveShape(ShapeType type)
    {
        if(HaveShapes.Contains(type))
        {
            HaveShapes.Remove(type);
        }
        else
        {
            Debug.Log($"{type} : 보유하지 않은 도형");
        }
    }
}

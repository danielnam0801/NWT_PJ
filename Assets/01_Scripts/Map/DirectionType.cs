using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirType
{
    Left,
    Right,
    Up,
    Down
}

public abstract class DirectionType : MonoBehaviour
{
    public DirType directionType;

    protected Vector3 repeatDir;

    protected virtual void Awake()
    {
        if (directionType == DirType.Left)
            repeatDir = new Vector3(-1, 0, 0);
        else if (directionType == DirType.Right)
            repeatDir = new Vector3(1, 0, 0);
        else if (directionType == DirType.Up)
            repeatDir = new Vector3(0, 1, 0);
        else if (directionType == DirType.Down)
            repeatDir = new Vector3(0, -1, 1);
    }
}

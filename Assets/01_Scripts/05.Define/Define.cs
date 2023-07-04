public enum PoolType
{
    GameObject,
    SFX,
    VFX
}

public enum PlayerStateType
{
    Movement = 0,
    Dash,
    Teleportation,
}

[System.Serializable]
public enum ShapeType
{
    Circle,
    Triangle,
    Square,
    Pentagon,
    FourStar,
    FiveStar,
    SixStar,
    UpArraw,
    DownArraw,
    LeftArraw,
    RightArraw,
    Default,
}


public enum AttackCheckType
{
    BOX_COLLISION,
    CIRCLE_COLLISION,
    BOX_COLLIDER,
    CIRCLE_COLLIDER,
    CIRCLE_OVERLAP,
    BOX_OVERLAP,
    LINE_RAYCAST,
    BOX_RAYCAST,
    CIRCLE_RAYCAST,
}
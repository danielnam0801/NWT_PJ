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

public enum ShapeType
{
    Circle,
    Triangle,
    Square,
}


public enum AttackCheckType
{
    COLLISION,
    COLLIDER,
    CIRCLE_OVERLAP,
    BOX_OVERLAP,
    LINE_RAYCAST,
    BOX_RAYCAST,
    CIRCLE_RAYCAST,
}
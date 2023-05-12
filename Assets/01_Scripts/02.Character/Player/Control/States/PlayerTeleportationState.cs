using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportationState : PlayerState
{
    private SpriteRenderer render;

    public override void Init(Transform root)
    {
        base.Init(root);

        render = root.transform.Find("Visual").GetComponent<SpriteRenderer>();
    }

    public override void EnterState()
    {
        StartCoroutine(Teleportation());
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }

    private IEnumerator Teleportation()
    {
        movement.ApplyGravity = false;
        Color color = render.color;
        color.a = 0;
        render.color = color;

        yield return new WaitForSeconds(status.TeleportationTime);

        controller.transform.position = attack.Weapon.transform.position;
        color.a = 1;
        render.color = color;
        attack.Weapon.StopStay();
        movement.ApplyGravity = true;
        controller.ChangeState(PlayerStateType.Movement);
        DrawManager.Instance.SetDelayDraw(attack.Weapon.Info.attackDelayTime);
    }
}

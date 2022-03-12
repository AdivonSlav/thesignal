using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.animator.SetBool("Walking", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            ExitState(movement, movement.Run);
        else if (Input.GetKeyDown(KeyCode.LeftControl))
            ExitState(movement, movement.Crouch);
        else if (movement.direction.magnitude < 0.1f)
            ExitState(movement, movement.Idle);

        if (movement.vInput < 0)
            movement.currentMoveSpeed = movement.walkBackSpeed;
        else
            movement.currentMoveSpeed = movement.walkSpeed;
    }

    private void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.animator.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.animator.SetBool("Crouching", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (movement.sprintAction.triggered)
            ExitState(movement, movement.Run);
        else if (movement.crouchAction.triggered)
        {
            if (movement.direction.magnitude < 0.1f)
                ExitState(movement, movement.Idle);
            else
                ExitState(movement, movement.Walk);
        }
        
        if (movement.vInput < 0)
            movement.currentMoveSpeed = movement.crouchBackSpeed;
        else
            movement.currentMoveSpeed = movement.crouchSpeed;
    }
    
    private void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.animator.SetBool("Crouching", false);
        movement.SwitchState(state);
    }
}

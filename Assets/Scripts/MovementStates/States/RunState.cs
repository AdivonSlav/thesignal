using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.animator.SetBool("Running", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (movement.sprintAction.triggered)
            ExitState(movement, movement.Walk);
        else if (movement.direction.magnitude < 0.1f)
            ExitState(movement, movement.Idle);
        
        if (movement.vInput < 0)
            movement.currentMoveSpeed = movement.runBackSpeed;
        else
            movement.currentMoveSpeed = movement.runSpeed;
    }
    
    private void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.animator.SetBool("Running", false);
        movement.SwitchState(state);
    }
}

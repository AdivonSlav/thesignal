using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;

    private int horizontal;
    private int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("hInput");
        vertical = Animator.StringToHash("vInput");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        // Animation snapping
        float snappedHorizontal = SnappedMovement(horizontalMovement);
        float snappedVertical = SnappedMovement(verticalMovement);

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

    private float SnappedMovement(float movement)
    {
        if (movement > 0.0f && movement < 0.55f)
            return 0.5f;
        if (movement > 0.55f)
            return 1.0f;
        if (movement < 0.0f && movement > -0.55f)
            return -0.5f;
        if (movement < -0.55f)
            return -1.0f;

        return 0.0f;
    }

}

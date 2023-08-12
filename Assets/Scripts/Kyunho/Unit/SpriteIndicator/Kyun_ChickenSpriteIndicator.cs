using UnityEngine;

public class Kyun_ChickenSpriteIndicator : MonoBehaviour, Kyun_ISpriteIndicator
{
    private Animator animator;
    private Kyun_DirectionType lastDirection;

    private void Awake()
    {
        TryGetComponent(out animator);
    }

    public void Initialize(Kyun_DirectionType direction, Kyun_Coordinate coordinate)
    {
        coordinate -= direction.ToCoordinate();
        transform.position = coordinate.ToVector();
    }

    public void UpdateSprite(Kyun_DirectionType direction, Kyun_Coordinate coordinate)
    {
        if (direction != Kyun_DirectionType.Down) coordinate -= direction.ToCoordinate();
        transform.position = coordinate.ToVector();
        if (direction != lastDirection)
        {
            animator.Play(direction.ToAnimatorString());
            lastDirection = direction;
        }
        animator.SetTrigger("Walk");
        
        //animator.SetTrigger("Walk");
        //animator.InitializeAnimator(direction);
        //animator.SetBool(direction.ToAnimatorString(), true);
        //animator.SetTrigger(direction.ToAnimatorString());
    }

    public void DestroySprite()
    {
        Destroy(gameObject);
    }
}
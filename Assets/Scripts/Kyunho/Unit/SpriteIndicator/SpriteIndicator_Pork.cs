using UnityEngine;

public class SpriteIndicator_Pork : MonoBehaviour, ISpriteIndicator
{
    private Animator animator;
    private DirectionType lastDirection;

    private void Awake()
    {
        TryGetComponent(out animator);
    }

    public void Initialize(DirectionType direction, Coordinate coordinate)
    {
        coordinate -= direction.ToCoordinate();
        transform.position = coordinate.ToVector();
    }

    public void UpdateSprite(DirectionType direction, Coordinate coordinate)
    {
        if (direction != DirectionType.Down) coordinate -= direction.ToCoordinate();
        transform.position = coordinate.ToVector();
        if (direction != lastDirection)
        {
            animator.Play(direction.ToAnimatorString());
            lastDirection = direction;
        }
        animator.SetTrigger("Walk");
    }

    public void DestroySprite()
    {
        Destroy(gameObject);
    }
}
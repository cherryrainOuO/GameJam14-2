using UnityEngine;

public class SpriteIndicator_Chick : MonoBehaviour, ISpriteIndicator
{
    private Animator animator;

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
        transform.position = coordinate.ToVector();
        animator.Play(direction.ToAnimatorString());
    }

    public void DestroySprite()
    {
        Destroy(gameObject);
    }
}
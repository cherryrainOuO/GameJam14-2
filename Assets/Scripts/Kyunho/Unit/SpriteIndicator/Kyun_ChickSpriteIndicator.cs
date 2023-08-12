using UnityEngine;

public class Kyun_ChickSpriteIndicator : MonoBehaviour, Kyun_ISpriteIndicator
{
    private Animator animator;

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
        transform.position = coordinate.ToVector();
        animator.Play(direction.ToAnimatorString());
    }

    public void DestroySprite()
    {
        Destroy(gameObject);
    }
}
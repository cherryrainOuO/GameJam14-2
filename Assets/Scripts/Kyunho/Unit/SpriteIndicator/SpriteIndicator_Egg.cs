using UnityEngine;

public class SpriteIndicator_Egg : MonoBehaviour, ISpriteIndicator
{
    public void UpdateSprite(DirectionType direction, Coordinate coordinate)
    {
        transform.position = coordinate.ToVector();
    }

    public void DestroySprite()
    {
        Destroy(gameObject);
    }
}
using UnityEngine;

public class Kyun_EggSpriteIndicator : MonoBehaviour, Kyun_ISpriteIndicator
{
    public void UpdateSprite(Kyun_DirectionType direction, Kyun_Coordinate coordinate)
    {
        transform.position = coordinate.ToVector();
    }

    public void DestroySprite()
    {
        Destroy(gameObject);
    }
}
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    [SerializeField] private SpriteIndicator_Chick chickSpriteIndicatorPrefab;
    [SerializeField] private SpriteIndicator_Chicken chickenSpriteIndicatorPrefab;
    [SerializeField] private SpriteIndicator_ChickBone chickBoneSpriteIndicatorPrefab;
    [SerializeField] private SpriteIndicator_NatureEgg natureEggSpriteIndicatorPrefab;
    [SerializeField] private SpriteIndicator_Egg eggSpriteIndicatorPrefab;
    [SerializeField] private SpriteIndicator_Pork porkSpriteIndicatorPrefab;

    public IUnit CreateUnit(UnitType type)
    {
        ISpriteIndicator spriteIndicator = CreateSpriteIndicator(type);
        switch (type)
        {
            case UnitType.Chicken:
                return new Unit_Chicken(spriteIndicator);
            case UnitType.Chick:
                return new Unit_Chick(spriteIndicator);
            case UnitType.ChickBone:
                return new Unit_ChickBone(spriteIndicator);
            case UnitType.Egg:
                return new Unit_Egg(spriteIndicator);
            case UnitType.NatureEgg:
                return new Unit_NatureEgg(spriteIndicator);
            case UnitType.Pork:
                return new Unit_Pork(spriteIndicator);
            case UnitType.Boss:
                break;
            case UnitType.None:
                break;
        }
        return null;
    }

    public ISpriteIndicator CreateSpriteIndicator(UnitType type)
    {
        switch (type)
        {
            case UnitType.Chicken:
                return Instantiate(chickenSpriteIndicatorPrefab);
            case UnitType.Chick:
                return Instantiate(chickSpriteIndicatorPrefab);
            case UnitType.ChickBone:
                return Instantiate(chickBoneSpriteIndicatorPrefab);
            case UnitType.Egg:
                return Instantiate(eggSpriteIndicatorPrefab);
            case UnitType.NatureEgg:
                return Instantiate(natureEggSpriteIndicatorPrefab);
            case UnitType.Pork:
                return Instantiate(porkSpriteIndicatorPrefab);
        }
        return null;
    }

}
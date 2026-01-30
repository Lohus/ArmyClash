using UnityEngine;

public class Property : ScriptableObject
{
    [SerializeField] int HP = 0;
    [SerializeField] int ATK = 0;
    [SerializeField] int SPEED = 0;
    [SerializeField] float ATKSPD = 0;
    public void ApplyProperty(BaseUnit unit)
    {
        unit.HP += HP;
        unit.ATK += ATK;
        unit.SPEED += SPEED;
        unit.ATKSPD += ATKSPD;
        ApplyAdditProperty(unit);
    }
    public virtual void ApplyAdditProperty(BaseUnit unit)
    {
        // Wite in inherited classes
    }
}
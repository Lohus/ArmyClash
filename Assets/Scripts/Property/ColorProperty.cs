using UnityEngine;

[CreateAssetMenu(fileName = "New Color Property", menuName = "Game/Property/Color")]
public class ColorProperty : Property
{
    [SerializeField] Color color;
    public override void ApplyAdditProperty(BaseUnit unit)
    {
        unit.gameObject.GetComponent<Renderer>().material.color = color;
    }
}
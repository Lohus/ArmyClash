using UnityEngine;

[CreateAssetMenu(fileName = "New Size Property", menuName = "Game/Property/Size")]
public class SizeProperty : Property
{
    [SerializeField] float size = 1;
    public override void ApplyAdditProperty(BaseUnit unit)
    {
        unit.transform.localScale = Vector3.one * size;
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "New Mesh Property", menuName = "Game/Property/Mesh")]
public class MeshProperty : Property
{
    [SerializeField] Mesh mesh;
    public override void ApplyAdditProperty(BaseUnit unit)
    {
        unit.gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }
}
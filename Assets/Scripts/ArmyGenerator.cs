using UnityEngine;

 public class ArmyGenerator : MonoBehaviour
{
    public static ArmyGenerator instance;
    private float sizeX = 2;
    private float sizeZ = 2;
    private int countX = 5;
    private int countZ = 4;
    private float height = 0.5f;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void GenerateArmy(Army army, GameObject prefabUnit, Transform transform)
    {
        Vector3 startPosition = transform.position - new Vector3(2 * sizeX, 0, 0);
        startPosition = transform.position - new Vector3(2 * sizeX, 0, 0);
        GameObject _unit;
        for (int x = 0; x < countX; x++)
        {
            for (int z = 0; z < countZ; z++ )
            {
                Vector3 cellPosition = startPosition + new Vector3(x * sizeX, height, z * sizeZ);
                _unit = Instantiate(prefabUnit, cellPosition, transform.rotation, transform);
                _unit.GetComponent<BaseUnit>().SetArmy(army);
                _unit.GetComponent<BaseUnit>().ApplyProperty();
                army.AddUnit(_unit);
            }
        }
    }
}
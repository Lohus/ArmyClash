using System.Collections.Generic;
using UnityEngine;
public class Army: MonoBehaviour
{
    [SerializeField] GameObject prefabUnit;
    private static int _count = 0;
    public int id { get; private set;} = 0;
    public string nameArmy;
    public int countUnits {get; private set;}
    public int maxUnitInArmy {get; private set;} = 20;
    public List<Property> properties = new List<Property>();
    public static bool BattleRage = false;
    List<GameObject> units = new List<GameObject>();
    public Army()
    {
        _count ++;
       id = _count;
       nameArmy = $"Army {id}";
    }

    public void OnEnable()
    {
        EventsA.StartButtle.AddListener(GenerateArmy);
        EventsA.ArmyDeath.AddListener(ArmyDeath);
    }
    public void OnDestroy()
    {
        EventsA.StartButtle.RemoveListener(GenerateArmy);
        EventsA.ArmyDeath.RemoveListener(ArmyDeath);
    }
    public void GenerateArmy()
    {
        ArmyGenerator.instance.GenerateArmy(this, prefabUnit, transform);
    }
    public void AddUnit(GameObject unit)
    {
        if (units.Count < maxUnitInArmy) units.Add(unit);
    }
    public void RemoveUnit(GameObject unit)
    {
        units.Remove(unit);
    }
    public void KillUnit(GameObject unit)
    {
        units.Remove(unit);
        if (units.Count == 0)
        {
            EventsA.ArmyDeath?.Invoke();
        }
        else if (units.Count <= maxUnitInArmy/2 && !BattleRage)
        {
            BattleRage = true;
            foreach(GameObject liveUnit in units)
            {
                liveUnit.GetComponent<BaseUnit>().BattleRage();
            }
        }
    }
    void ArmyDeath()
    {
        BattleRage = false;
        if (units.Count > 0)
        {
            EventsA.ArmyWin?.Invoke(nameArmy);
        }
        foreach(GameObject unit in units)
        {
            Destroy(unit);
        }
        units.Clear();
    }
}
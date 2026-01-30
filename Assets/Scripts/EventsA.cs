using UnityEngine.Events;

public static class EventsA
{
    public static UnityEvent ArmyDeath = new UnityEvent();
    public static UnityEvent<string> ArmyWin = new UnityEvent<string>();
    public static UnityEvent StartButtle = new UnityEvent();
    public static UnityEvent ChangeProperty = new UnityEvent();
}
using TMPro;
using UnityEngine;

public class UIELements : MonoBehaviour
{
    [SerializeField] GameObject winsWindow;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] Property[] meshProperties;
    [SerializeField] Property[] colorProperties;
    [SerializeField] Property[] sizeProperties;
    MeshProperty meshProperty;
    ColorProperty colorProperty;
    SizeProperty sizeProperty;
    public void OnEnable()
    {
        EventsA.ArmyWin.AddListener((nameArmy) => ActiveWinWindow(nameArmy));
    }
    public void OnDestroy()
    {
        
    }
    public void Start()
    {
        meshProperties = Resources.LoadAll<Property>("Property/Mesh");
        colorProperties = Resources.LoadAll<Property>("Property/Color");
        sizeProperties = Resources.LoadAll<Property>("Property/Size");
    }
    public void StartBattle()
    {
        EventsA.StartButtle?.Invoke();
    }
    public void RandomButton(Army army)
    {
        if (army.properties.Count != 0)
        {
            army.properties.Clear();
        }
        army.properties.Add(meshProperties[Random.Range(0, meshProperties.Length)]);
        army.properties.Add(colorProperties[Random.Range(0, colorProperties.Length)]);
        army.properties.Add(sizeProperties[Random.Range(0, sizeProperties.Length)]);
        EventsA.ChangeProperty?.Invoke();
    }
    void ActiveWinWindow(string armyName)
    {
        message.text = $"{armyName} is win!";
        winsWindow.SetActive(true);
    }
}
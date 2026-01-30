using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class SumpleDropdown : MonoBehaviour
{
    [SerializeField] string folder;
    [SerializeField] Army army;
    private TMP_Dropdown dropdown;
    private TextMeshProUGUI label;
    private Property[] properties;
    
    public void Start()
    {
        dropdown = gameObject.GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
        EventsA.ChangeProperty.AddListener(Refresh);
        label = transform.Find("Label").GetComponent<TextMeshProUGUI>();
        properties = Resources.LoadAll<Property>($"Property/{folder}");        
        Refresh();
    }
    public void SelectProperty(Army army, Property property)
    {
        if (army.properties.Count == 0 )
        {
            army.properties.Add(property);
        }
        else
        {
            for(int i = 0; i < army.properties.Count; i++)
            {
                if(army.properties[i].GetType() == property.GetType())
                {
                    army.properties[i] = property;
                    return;
                } 
            }
            army.properties.Add(property);
        }
    }
    void Refresh()
    {
        dropdown.ClearOptions();
        foreach (Property property in properties)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(property.name));
        }
        dropdown.value = -1;
    }
    void OnDropdownChanged(int index)
    {
    if (index >= 0 && index < properties.Length && properties[index] != null)
    {
        SelectProperty(army, properties[index]);
    }
    }
    public void OnDestroy()
    {
        EventsA.ChangeProperty.RemoveListener(Refresh);
        dropdown.onValueChanged.RemoveListener(OnDropdownChanged);
    }
}
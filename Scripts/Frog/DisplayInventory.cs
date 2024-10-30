using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _strawberryText;
    [SerializeField] private Inventory _inventory;

    private void OnEnable()
    {
        _inventory.StrawberryValueChanged += DisplayStrawberryValue;
    }

    private void OnDisable()
    {
        _inventory.StrawberryValueChanged -= DisplayStrawberryValue;
    }

    private void DisplayStrawberryValue(int value)
    {
        _strawberryText.text = value.ToString();
    }
}

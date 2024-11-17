using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _strawberryText;

    public void DisplayStrawberryValue(int value)
    {
        _strawberryText.text = value.ToString();
    }
}

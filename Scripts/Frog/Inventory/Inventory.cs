using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<int> StrawberryValueChanged;

    public int StrawberryValue { get; private set; } = 0;

    public void OnMedkitFound(Medkit medkit)
    {
        medkit.Deactivate();
    }

    public void OnStrawberryFound(Strawberry strawberry)
    {
        if (strawberry.CanCollect)
        {
            strawberry.DeactivateWithEffect();
            StrawberryValue++;
            StrawberryValueChanged?.Invoke(StrawberryValue);
        }
    }
}

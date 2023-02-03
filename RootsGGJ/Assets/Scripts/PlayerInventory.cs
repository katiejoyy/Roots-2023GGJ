using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ITEMS
{
    FIREWOOD,
    TEMP_2
}

public class PlayerInventory : MonoBehaviour
{
    private HashSet<ITEMS> _inventory = new HashSet<ITEMS>();

    public void Collect(ITEMS item) => _inventory.Add(item);
    public bool Has(ITEMS item) => _inventory.Contains(item);
    public void Print() => Debug.Log(_inventory.Select(item => $"{item.ToString()}, "));
}

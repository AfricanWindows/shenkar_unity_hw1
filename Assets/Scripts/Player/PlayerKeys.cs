using System;
using UnityEngine;

/// <summary>
/// Remembers whether Mario picked up the level key.
/// Implements ICounter so the key can be shown in the GUI for free.
/// </summary>
public class PlayerKeys : MonoBehaviour, ICounter
{
    private int keys = 0;

    public int Value
    {
        get { return keys; }
    }

    public bool HasKey
    {
        get { return keys > 0; }
    }

    public event Action<int> OnValueChanged;

    public void AddKey(int amount)
    {
        if (amount <= 0)
            return;

        keys += amount;

        if (OnValueChanged != null)
            OnValueChanged(keys);
    }
}

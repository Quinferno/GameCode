using System;
using UnityEngine;

public interface IDie
{
    event Action<int, int> OnHealthChanged;
    event Action<IDie> OnDied;
    GameObject gameObject { get; }

}

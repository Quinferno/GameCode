using UnityEngine;

public interface IAttack
{
    int Damage { get; }
    Transform transform { get; }
}
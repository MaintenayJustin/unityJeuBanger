using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Trap", fileName = "NewTrap")]
public class TrapData : ScriptableObject
{
    public int DamageAmount;
    public float KnockbackX;
    public float KnockbackY;
}

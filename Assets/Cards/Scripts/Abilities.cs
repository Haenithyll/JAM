using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Abilities
{
    public string Name;
    public Sprite AbilitySprite;
    public SpriteRenderer AbilitySpriteRenderer;
    public int HpDamage;
    public int ArmorDamage;
    public bool Heal;
}

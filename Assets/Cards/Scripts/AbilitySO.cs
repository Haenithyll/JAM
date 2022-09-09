using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName ="Ability_XXX", menuName ="Capacité")]
public class AbilitySO : ScriptableObject
{
    public string Name;
    public Sprite AbilitySprite;
    public SpriteRenderer AbilitySpriteRenderer;
    public int HpDamage;
    public int ArmorDamage;
    public bool Heal;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterAttributes
{
    public string Name;
    public Sprite CharacterSprite;
    public SpriteRenderer CharacterSpriteRenderer;
    public int Life;
    public int MaxLife;
    public int Armor;
    public int MaxArmor;
    public Teams Team;
    public int enemyId;

    public enum Teams
    {
        Player,
        Enemy
    }
}

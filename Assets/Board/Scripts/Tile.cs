using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomEffect {
        None,
        Bonus_1HP,
        Bonus_1Card,
        Bonus_2Cards,
        Malus_1HP,
        Malus_1Card,
        Boss
    }

public class Tile : MonoBehaviour
{
    [SerializeField] private CustomEffect _effect;
}

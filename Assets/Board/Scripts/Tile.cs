using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CustomEffect
{
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
    public void DisplayText(TextMeshProUGUI textHolder)
    {
        string text;

        switch (_effect)
        {
            case CustomEffect.None:
                text = "Nothing Happens";
                break;
            case CustomEffect.Bonus_1HP:
                text = "Bonus ! +1 HP";
                break;
            case CustomEffect.Bonus_1Card:
                text = "Bonus ! +1 Card";
                break;
            case CustomEffect.Bonus_2Cards:
                text = "Bonus ! +2 Cards";
                break;
            case CustomEffect.Malus_1Card:
                text = "Malus ! -1 Card";
                break;
            case CustomEffect.Malus_1HP:
                text = "Malus ! -1 HP";
                break;
            default:
                text = "Nothing Happens!";
                break;
        }

        textHolder.text = text;
    }
}

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
    [SerializeField] private string _customText;

    public void DisplayText(TextMeshProUGUI textHolder)
    {
        string text;

        if (_customText == string.Empty)
        {
            switch (_effect)
            {
                case CustomEffect.None:
                    text = "Nothing Happens";
                    break;
                case CustomEffect.Bonus_1HP:
                    GameManager.instance.AddHealth();
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
                    GameManager.instance.RemoveHealth();
                    text = "Malus ! -1 HP";
                    break;
                default:
                    text = "Nothing Happens!";
                    break;
            }
        }
        else
            text = _customText;

        textHolder.text = text;
    }
}

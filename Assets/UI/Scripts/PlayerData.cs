using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Structure de données pour toutes les cartes que le joueur possède.<br/>
/// Placeholder par Nathan pour qu'il puisse faire les UI qui affiche des cartes, à remplacer par la version complète.
/// </summary>
public static class PlayerData
{
    public static List<CardsAttributes> Inventory = new List<CardsAttributes>();
    public static List<CardsAttributes> Hand = new List<CardsAttributes>(MAX_CARDS_IN_HAND);

    public const int MAX_CARDS_IN_INVENTORY = 10;
    public const int MAX_CARDS_IN_HAND = 5;

    public static bool SwapCards(CardsAttributes card1, CardsAttributes card2)
    {
        int i1 = -1;
        int i2 = -1;
        List<CardsAttributes> list1 = null;
        List<CardsAttributes> list2 = null;
        if (Inventory.Contains(card1))
        {
            list1 = Inventory;
            i1 = Inventory.IndexOf(card1);
        }
        else if (Hand.Contains(card1))
        {
            list1 = Hand;
            i1 = Hand.IndexOf(card1);
        }
        
        if (Inventory.Contains(card2))
        {
            list2 = Inventory;
            i2 = Inventory.IndexOf(card2);
        }
        else if (Hand.Contains(card2))
        {
            list2 = Hand;
            i2 = Hand.IndexOf(card2);
        }

        if (i1 == -1 || i2 == -1)
        {
            Debug.Log($"{i1}, {i2}");
            return false;
        }
        
        list2[i2] = card1;
        list1[i1] = card2;
        return true;
    }

    public static void GenerateCards(int handNumber)
    {
        for (int i = 0; i < MAX_CARDS_IN_INVENTORY; i++)                   
        {                                              
            Inventory.Add(new CardsAttributes());
        }
        for (int i = 0; i < handNumber; i++)
        {
            Hand.Add(new CardsAttributes());
        }
    }
}

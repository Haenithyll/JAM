using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public List<Card> Deck = new List<Card>();
    public int CardInHandsAtBeg;
    public GameObject PlayerManager;
    
    public void Start()
    {
        for (int i = 0; i < CardInHandsAtBeg; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if(Deck.Count >= 1)
        {
            Card randomCard = Deck[Random.Range(0, Deck.Count)];
            randomCard.gameObject.SetActive(true);
            Deck.Remove(randomCard);
        }
    }

    public void PickCard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit);
            if (hit.transform != null && hit.transform.TryGetComponent<Card>(out Card card))
            {
                int abilityIndex = card.Attributes.CardAbility;
                PlayerManager.GetComponent<PlayerManager>().hasPickedCard = true;
            }
        }
    }

}

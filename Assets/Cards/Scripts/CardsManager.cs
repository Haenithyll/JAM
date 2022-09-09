using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CardsManager : MonoBehaviour
{
    public List<CardGameObject> Deck = new List<CardGameObject>();
    public int CardInHandsAtBeg;
    public int SpaceBetweenHandsCards;
    [Header("References")]
    public GameObject PlayerManager;
    public Transform CardsTf;
    public Transform DeckTf;
    public Transform HandTf;
    [Header("Prefabs")]
    public GameObject CardPrefab;
    
    public void Start()
    {
        PlayerData.GenerateCards(CardInHandsAtBeg); //TEMPORAIRE ?
        SpawnAllCards();
        // for (int i = 0; i < CardInHandsAtBeg; i++)
        // {
        //     DrawCard();
        // }
    }

    void SpawnAllCards()
    {
        int cardNb = -1;
        foreach (CardsAttributes attributes in PlayerData.Hand)
        {
            cardNb++;
            CardGameObject cardGO = InstantiateCard(attributes, HandTf.position + Vector3.right * SpaceBetweenHandsCards * cardNb, HandTf.rotation);
        }
    }
    
    public void DrawCard()
    {
        if(Deck.Count >= 1)
        {
            CardGameObject randomCard = Deck[Random.Range(0, Deck.Count)];
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

    CardGameObject InstantiateCard(CardsAttributes attributes, Vector3 position, Quaternion rotation)
    {
        CardGameObject cardGO = Instantiate(CardPrefab, position, rotation, CardsTf).GetComponent<CardGameObject>();
        cardGO.Init(attributes);
        
        return cardGO;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CardList : MonoBehaviour
{
    enum Orientation
    {
        Horizontal,
        Vertical
    }
    
    [Header("Parameters")]
    [SerializeField] private Orientation _orientationHand;
    [FormerlySerializedAs("_cardsPerRow")] [SerializeField] private int _cardsPerRowInventory;
    [SerializeField] private Vector2 _distanceBetweenCardsInventory;
    [SerializeField] private Vector2 _distanceBetweenCardsHand;
    [SerializeField] private float _distributeDelay = 0.3f;
    [SerializeField] private float _putAwayDelay = 0.1f;
    [Header("Prefabs")]
    [SerializeField] private GameObject _cardPrefab;
    [Header("References")]
    [SerializeField] private Transform _inventory;
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _cardsSpawnpoint;

    private Vector3 _inventoryOrigin = Vector3.zero;
    private List<CardGameObject> _cardGameObjects = new List<CardGameObject>();
    private Coroutine _showCardsInvRoutine;
    private Coroutine _showCardsHandRoutine;
    private int _interactability = 0;
    private CardGameObject _selectedCard;
    private bool _cardsShown;

    private void Awake() // TEMPORAIRE
    {
        __PlayerData.GenerateCards();
    }

    private void Start()
    {
        _inventoryOrigin = _inventory.transform.position;
        _inventoryOrigin.x -= (_cardsPerRowInventory-1) * _distanceBetweenCardsInventory.x / 2.0f;
    }

    private void Update()
    {
        if (IsInteractable() && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit);
            if (hit.transform != null && hit.transform.TryGetComponent<CardGameObject>(out CardGameObject clickedCard))
            {
                CardGameObjectClicked(clickedCard);
            }
        }
    }

    bool IsInteractable()
    {
        return _interactability == 0;
    }

    public void ToggleCards()
    {
        if (_cardsShown)
            PutAwayCards();
        else
            ShowCards();
    }
    
    public void ShowCards()
    {
        if (IsInteractable() == false) return;
        _cardsShown = true;
        _showCardsInvRoutine = StartCoroutine(ShowCardsInventory());
        _showCardsHandRoutine = StartCoroutine(ShowCardsHand());
    }

    public void PutAwayCards()
    {
        if (IsInteractable() == false) return;
        _cardsShown = false;
        StopCoroutine(_showCardsInvRoutine);
        StopCoroutine(_showCardsHandRoutine);
        _showCardsInvRoutine = null;
        _showCardsHandRoutine = null;
        StartCoroutine(PutAwayCardsAll());
    }
    
    IEnumerator ShowCardsInventory()
    {
        _interactability++;
        Vector3 destinationPos;
        int cardRow = -1;
        int cardColumn = 0;
        foreach (CardData c in __PlayerData.Inventory)
        {
            cardRow++;
            if (cardRow > _cardsPerRowInventory-1)
            {
                cardRow = 0;
                cardColumn++;
            }
            destinationPos = _inventoryOrigin + new Vector3(cardRow * _distanceBetweenCardsInventory.x, cardColumn * -_distanceBetweenCardsInventory.y);
            InstantiateCard(c, destinationPos);
            yield return new WaitForSeconds(_distributeDelay);
        }
        _interactability--;
    }

    IEnumerator ShowCardsHand()
    {
        _interactability++;
        int cardRow = -1;
        Vector3 destinationPos;
        foreach (CardData c in __PlayerData.Hand)
        {
            cardRow++;
            if (_orientationHand == Orientation.Horizontal)
                destinationPos = _hand.transform.position + new Vector3(cardRow * _distanceBetweenCardsHand.x, 0.0f);
            else
                destinationPos = _hand.transform.position + new Vector3(0.0f, cardRow * -_distanceBetweenCardsHand.y);
            InstantiateCard(c, destinationPos);
            yield return new WaitForSeconds(_distributeDelay);
        }

        _interactability--;
    }
    
    IEnumerator PutAwayCardsAll()
    {
        _interactability++;
        foreach (CardGameObject cGO in _cardGameObjects)
        {
            cGO.MoveShrink(_cardsSpawnpoint.position, true);
            yield return new WaitForSeconds(_putAwayDelay);
        }
        _cardGameObjects.Clear();
        _interactability--;
    }

    void InstantiateCard(CardData card, Vector3 destination)
    {
        CardGameObject cardGO = Instantiate(_cardPrefab, _cardsSpawnpoint.position, Quaternion.identity).GetComponent<CardGameObject>();
        cardGO.Init(card);
        cardGO.MoveGrow(destination);
        _cardGameObjects.Add(cardGO);
    }

    void CardGameObjectClicked(CardGameObject card)
    {
        if (_selectedCard == null || _selectedCard == card)
        {
            _selectedCard = card;
            _selectedCard.LittleDance();
        }
        else if (__PlayerData.SwapCards(_selectedCard.CardData, card.CardData))
        {
            Vector3 pos1 = _selectedCard.ShouldBePosition;
            Vector3 pos2 = card.ShouldBePosition;
            card.Move(pos1);
            _selectedCard.Move(pos2);
            _selectedCard = null;
        }
        else
        {
            Debug.Log($"y'a un blème avec {_selectedCard.gameObject.name} et {card.gameObject.name}");
            _selectedCard = null;
        }
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Prévisu inventaire
        Gizmos.color = Color.white;
        Vector3 origin = _inventory.transform.position;
        origin.x -= (_cardsPerRowInventory-1) * _distanceBetweenCardsInventory.x / 2.0f;
        Vector3 destinationPos;
        int cardRow = -1;
        int cardColumn = 0;
        for (int i = 0; i < 30; i++)
        {
            cardRow++;
            if (cardRow > _cardsPerRowInventory-1)
            {
                cardRow = 0;
                cardColumn++;
            }
            destinationPos = origin + new Vector3(cardRow * _distanceBetweenCardsInventory.x, cardColumn * -_distanceBetweenCardsInventory.y);
            GizmoDrawCard();
        }
        
        //Prévisu main
        cardRow = -1;
        for (int i = 0; i < __PlayerData.MAX_CARDS_IN_HAND; i++)
        {
            cardRow++;
            if (_orientationHand == Orientation.Horizontal)
                destinationPos = _hand.transform.position + new Vector3(cardRow * _distanceBetweenCardsHand.x, 0.0f);
            else
                destinationPos = _hand.transform.position + new Vector3(0.0f, cardRow * -_distanceBetweenCardsHand.y);
            GizmoDrawCard();
        }

        //Prévisu point d'apparition des cartes;
        if (_cardsSpawnpoint != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(_cardsSpawnpoint.position, 0.3f);
        }

        void GizmoDrawCard()
        {
            if (_cardPrefab == null)
                Gizmos.DrawCube(destinationPos, new Vector3(0.5f, 0.5f, 0.5f));
            else
                Gizmos.DrawCube(destinationPos, _cardPrefab.GetComponent<BoxCollider>().size);
        }
    }
#endif
}
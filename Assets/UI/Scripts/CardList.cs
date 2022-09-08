using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardList : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private int _cardsPerRow;
    [SerializeField] private Vector2 _distanceBetweenCards;
    [SerializeField] private float _distributeDelay = 0.3f;
    [SerializeField] private float _putAwayDelay = 0.1f;
    [Header("Prefabs")]
    [SerializeField] private GameObject _cardPrefab;
    [Header("References")]
    [SerializeField] private Transform _cardsSpawnpoint;

    private Vector3 _origin = Vector3.zero;
    private List<CardGameObject> _cardGameObjects = new List<CardGameObject>();
    
    private List<Card> __cards = new List<Card>(); //Liste de cartes temporaires, à remplacer par une référence à la liste des cartes du joueur;

    private void Awake()
    {
        for (int i = 0; i < 30; i++)        // Temporaire
        {                                   // |
            __cards.Add(new Card());    // |
        }                                   // <
    }

    private void Start()
    {
        _origin = transform.position;
        _origin.x -= (_cardsPerRow-1) * _distanceBetweenCards.x / 2.0f;

        StartCoroutine(ShowCards());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PutAwayCards());
        }
    }

    IEnumerator ShowCards()
    {
        Vector3 destinationPos;
        int cardRow = -1;
        int cardColumn = 0;
        foreach (Card c in __cards)
        {
            cardRow++;
            if (cardRow > _cardsPerRow-1)
            {
                cardRow = 0;
                cardColumn++;
            }
            destinationPos = _origin + new Vector3(cardRow * _distanceBetweenCards.x, cardColumn * -_distanceBetweenCards.y);
            CardGameObject cardGO = Instantiate(_cardPrefab, _cardsSpawnpoint.position, Quaternion.identity).GetComponent<CardGameObject>();
            cardGO.Init(c);
            cardGO.MoveGrow(destinationPos);
            _cardGameObjects.Add(cardGO);
            yield return new WaitForSeconds(_distributeDelay);
        }
    }

    IEnumerator PutAwayCards()
    {
        foreach (CardGameObject cGO in _cardGameObjects)
        {
            cGO.MoveShrink(_cardsSpawnpoint.position, true);
            yield return new WaitForSeconds(_putAwayDelay);
        }
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector3 origin = transform.position;
        origin.x -= (_cardsPerRow-1) * _distanceBetweenCards.x / 2.0f;
        Vector3 destinationPos;
        int cardRow = -1;
        int cardColumn = 0;
        for (int i = 0; i < 30; i++)
        {
            cardRow++;
            if (cardRow > _cardsPerRow-1)
            {
                cardRow = 0;
                cardColumn++;
            }
            destinationPos = origin + new Vector3(cardRow * _distanceBetweenCards.x, cardColumn * -_distanceBetweenCards.y);
            if (_cardPrefab == null)
                Gizmos.DrawCube(destinationPos, new Vector3(0.5f, 0.5f, 0.5f));
            else
                Gizmos.DrawCube(destinationPos, _cardPrefab.GetComponent<BoxCollider>().size);
        }
        
        if (_cardsSpawnpoint != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(_cardsSpawnpoint.position, 0.3f);
        }
    }
#endif
}
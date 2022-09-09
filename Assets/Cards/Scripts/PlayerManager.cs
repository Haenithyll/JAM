using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public States State;
    public GameObject CardsManager;
    public GameObject CharacterList;
    public Player Player;
    [HideInInspector] public bool hasPickedCard = false;
    [HideInInspector] public bool hasPickedEnemy = false;

    public enum States
    {
        Draw,
        CardPick,
        EnemyPick,
        EndTurn,
        EnemyTurn
    }

    // Start is called before the first frame update
    void Start()
    {
        State = States.CardPick;
    }

    // Update is called once per frame
    void Update()
    {
        if (State == States.Draw)
        {
            CardsManager.GetComponent<CardsManager>().DrawCard();
            State = States.CardPick;
        }

        if (State == States.CardPick)
        {
            if(Input.GetMouseButtonDown(0))
                CardsManager.GetComponent<CardsManager>().PickCard();
            if (hasPickedCard == true)
                State = States.EnemyPick;
        }

        if (State == States.EnemyPick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out RaycastHit hit);
                if (hit.transform != null && hit.transform.TryGetComponent<Enemy>(out Enemy attributes))
                {
                    CharacterAttributes.Teams team = attributes.Attributes.Team;
                    if (gameObject.GetComponent<CharacterAttributes>().Team != team)
                    {
                        int enemyID = attributes.Attributes.enemyId;
                        BattleUtils.Instance.DamageUtil(enemyID);
                        State = States.EndTurn;
                    }
                }
            }
        }

        if (State == States.EndTurn)
        {
            
        }
    }
}

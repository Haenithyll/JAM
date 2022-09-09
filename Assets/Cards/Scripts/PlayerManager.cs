using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public States State;
    public GameObject CardsManager;
    public GameObject CharacterList;
    public Player Player;

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
        State = States.Draw;
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
            State = States.EnemyPick;
        }

        if (State == States.EnemyPick)
        {
           // CharacterList.GetComponent<CharactersList>()
            State = States.EndTurn;
        }

        if (State == States.EndTurn)
        {
            CardsManager.GetComponent<CardsManager>().DrawCard();
            State = States.EnemyTurn;
        }
    }

    public void clickOnEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit);
          //  if (hit.transform != null && hit.transform.TryGetComponent<CharactersList>(out CharactersList charactersList))
            //{
            //    int team = charactersList
            //}
        }
    }
}

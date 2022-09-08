using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager _BoardManager;
    [SerializeField] private CharacterManager _CharManager;

    public static GameManager instance;

    private bool bossTile;

    private void Awake()
    {
        instance = this;
        bossTile = false;

        _BoardManager.InitBoard(0);
        _CharManager.InitCharacter();
    }

    private void Update()
    {
        if (bossTile)
        {
            FadeOut();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 destinationPos = _BoardManager.GetDestinationTilePosition();
            _CharManager.MoveCharacter(destinationPos);
        }
    }

    public void QueueBoss()
    {
        bossTile = true;
    }

    private void FadeOut()
    {
        //TODO
    }
}

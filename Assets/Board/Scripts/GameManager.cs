using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager _BoardManager;
    [SerializeField] private CharacterManager _CharManager;
    [SerializeField] private HealthBarManager _HealthManager;

    public static GameManager instance;

    private bool bossTile;
    private bool gameOver;

    private void Awake()
    {
        instance = this;
        bossTile = false;
        gameOver = false;

        _BoardManager.InitBoard(0);
        _CharManager.InitCharacter();
        _HealthManager.Init();
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
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _HealthManager.RemoveHalfHeart();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _HealthManager.AddHalfHeart();
        }
    }

    public void QueueBoss()
    {
        bossTile = true;
    }
    public void QueueGameOver()
    {
        gameOver = true;
    }

    private void FadeOut()
    {
        //TODO
    }
}

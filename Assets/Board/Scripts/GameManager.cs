using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager _BoardManager;

    private void Awake(){
        _BoardManager.InitBoard(0);
    }
}

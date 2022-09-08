using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUtils : MonoBehaviour
{

    public static BattleUtils Instance;
    public GameObject PlayerManager;
    public GameObject EnemyManager;

    void Awake()
    {
        Instance = this;
    }

    public void DamageUtils(string attackerName, string defenderName, int damage)
    {
        
        
    }
}

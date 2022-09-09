using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUtils : MonoBehaviour
{

    public static BattleUtils Instance;
    public GameObject PlayerManager;
    public GameObject EnemyManager;
    public GameObject AbilitiesList;

    int hpDamage;
    int armorDamage;

    void Awake()
    {
        Instance = this;
    }

    public void DamageUtil(int id)
    {
        
        
    }

    public void ApplyAbilitiesUtil(int id)
    {
        hpDamage = AbilitiesList.GetComponent<AbilitiesList>().ListOfAbilities[id].HpDamage;
        armorDamage = AbilitiesList.GetComponent<AbilitiesList>().ListOfAbilities[id].ArmorDamage;
    }
}

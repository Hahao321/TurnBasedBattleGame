using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class BattleData1
{
    public BattleData1(Sprite sprite, List<BaseMonster> enemies, List<Vector3> vecs, int levels)
    {
        this.Background = sprite;
        this.EnemyList = enemies;
        this.level = levels;
        this.EnemylocationListofSpawningEnemies = vecs;
    }

    public GameObject enemy;


    public int level; 

    //Background Image
    public Sprite Background;

    //list of enemies
    public List<GameObject> EnemyList1 = new List<GameObject>();
    public List<BaseMonster> EnemyList = new List<BaseMonster>();
    //Enemy Locations
    public List<Vector3> EnemylocationListofSpawningEnemies = new List<Vector3>();
}

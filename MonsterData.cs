using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    // Start is called before the first frame update
    public List<BaseAttack> MoveData = new List<BaseAttack>();
    public List<GameObject> MonsterDataH = new List<GameObject>();
    public List<GameObject> MonsterDataE = new List<GameObject>();

    public GameObject hero;
    public GameObject enemy;
}

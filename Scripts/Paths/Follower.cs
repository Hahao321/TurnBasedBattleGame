using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class Follower: MonoBehaviour
{
    public PathCreator pathcreator;
    public float speed = 10;
    public float distancetraveled=0;


    private void Awake()
    {
        pathcreator = GameObject.Find("Path").GetComponent<PathCreator>();
    }
    private void Update()
    {
        speed = 10;
        distancetraveled += speed* Time.deltaTime;
        transform.position = pathcreator.path.GetPointAtDistance(distancetraveled, EndOfPathInstruction.Stop);

        


    }
}

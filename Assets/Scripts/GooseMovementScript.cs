//from: https://www.reddit.com/r/Unity3D/comments/19cqoea/how_can_i_have_a_gameobject_move_randomly/ 
//https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/index.html
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Apple;

public class GooseMovementScript : MonoBehaviour
{

    public NavMeshAgent agent;
    int coordX;
    int coordZ;
    public Vector3 destination;
    public int frequency = 3;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(Destination), 1, Random.Range(5, 10)); //method, startTime, repeatRate (seconds)
    } 

    void Destination()
    {
        coordX = Random.Range(-4, 4);
        coordZ = Random.Range(-4, 4);
        destination = new Vector3(transform.position.x + coordX, 0, transform.position.z + coordZ);
        agent.SetDestination(destination); 
    }
}

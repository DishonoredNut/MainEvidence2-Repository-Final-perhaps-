using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour //Fuuuu...heck yeah i recycled this script from scripting 2 these things are hard to make
{
    public float patrolRadius = 10f; //area it will patrol
    public float chaseRadius = 5f; //detection radius
    public float chaseDuration = 5f; //time it will chase before going back to patrol
    public Transform target; // object to chase
    private NavMeshAgent agent;
    private Vector3 startPosition;
    private float elapsedTime = 0f; 
    private bool isChasing = false;
    private Coroutine patrolCoroutine; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //grabs navmesh agent component
        startPosition = transform.position; 
        patrolCoroutine = StartCoroutine(Patrol()); //calls patrol function (ATTTT THHEEEEE BOOOOTTTTTTOOOOM)
    }

    void Update()
    {
        // Find all objects with the "Player" tag.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            float shortestDistance = float.MaxValue;
            Transform closestPlayer = null;

            // Find the  player.
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestPlayer = player.transform;
                }
            }

            if (isChasing)
            {
                if (shortestDistance > chaseRadius)
                {
                    elapsedTime += Time.deltaTime;

                    if (elapsedTime >= chaseDuration || closestPlayer == null)
                    {
                        isChasing = false;
                        elapsedTime = 0f;
                        agent.SetDestination(startPosition);

                        // Start patrolling again.
                        if (patrolCoroutine == null)
                            patrolCoroutine = StartCoroutine(Patrol());
                    }
                }
                else
                {
                    elapsedTime = 0f;
                    agent.SetDestination(closestPlayer.position);
                }
            }
            else
            {
                if (shortestDistance <= chaseRadius)
                {
                    isChasing = true;
                    elapsedTime = 0f;
                    StopCoroutine(patrolCoroutine);
                    patrolCoroutine = null;
                }
            }
        }
        else
        {
            // If there is not a player in radius, stop chasing and return to patrol.
            isChasing = false;
            elapsedTime = 0f;
            agent.SetDestination(startPosition);

            // Start patrolling again.
            if (patrolCoroutine == null)
                patrolCoroutine = StartCoroutine(Patrol());
        }
    }

    IEnumerator Patrol() //function for patrolling 
    {
        while (true) //use while so that it can be ovveridden once chase starts
        {
            Vector3 randomPoint = Random.insideUnitSphere * patrolRadius; //utilises a random point it can be places on to then factor in where to patrol 
            randomPoint += startPosition;//start position = whereever it lands up

            NavMeshHit hit; 
            NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, 1); //when making contact with Navmesh it will then picka position

            agent.SetDestination(hit.position); //sets next navmesh destination on hits on navmesh positions in a random randge

            yield return new WaitForSeconds(Random.Range(3, 8)); //wait for a few seconds before moving again.
        }
    }
}

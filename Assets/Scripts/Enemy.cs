using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Player player;
    bool playerInSight;
    private bool playerLocated = false;
    private Vector3 playerLocation;

    ArrayList enemies = new ArrayList();

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }


    private void Update()
    {
        // for (int i = 0; i < enemies.Count; ) {
        //     Enemy e = (Enemy) enemies[i];
        //     if (Vector3.Distance(transform.position, player.transform.position) > 50) {
        //         enemies.Remove(e);
        //         Debug.Log("Old Friend");
        //     }
        //     else i++;
        // }

        // if (playerLocated) {
        //     navMeshAgent.isStopped = false;
        //     navMeshAgent.SetDestination(playerLocation);
        //     if (Vector3.Distance(transform.position, playerLocation) < 15) playerLocated = false;
        //     return;
        // }

        if (!playerInSight) {
            // transform.position += transform.forward * 5 * Time.deltaTime;
            // transform.rotation *= Quaternion.Euler(0, 5 * Time.deltaTime, 0);
            return;
        }

        if (Vector3.Distance(transform.position, player.transform.position) > 50)
        { 
            playerInSight = false;
            navMeshAgent.isStopped = true;
            return;
        }

        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = null;

        if (other.GetComponent<Player>())
        {
            playerInSight = true;
            // playerLocated = false;
        } 
        // else enemy = other.GetComponent<Enemy>();
        
        // if (enemy) {
        //     enemies.Add(enemy);
        //     Debug.Log("New Friend");
        // }
    }

    // public void playerFound(Vector3 v) {
    //     playerLocated = true;
    //     playerLocation = v;
    //     Debug.Log("Called");
    // }
}
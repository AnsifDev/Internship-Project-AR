using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Player player;
    bool playerInSight;
    [SerializeField] private Transform checkpost1, checkpost2;
    [SerializeField] private Gun gun;
    private float time = 0;
    // private bool playerLocated = false;
    // private Vector3 playerLocation;

    ArrayList enemies = new ArrayList();

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = false;
        player = FindObjectOfType<Player>();
    }

    private void Start() {
        navMeshAgent.SetDestination(checkpost1.position);
        Debug.Log("Launched");
    }


    private void Update()
    {

        if (!playerInSight) {

            if (Vector3.Distance(transform.position, checkpost1.position) < 10) 
                navMeshAgent.SetDestination(checkpost2.position);

            if (Vector3.Distance(transform.position, checkpost2.position) < 10) 
                navMeshAgent.SetDestination(checkpost1.position);

            return;
        }

        if (Vector3.Distance(transform.position, player.transform.position) > 50)
        { 
            playerInSight = false;
            navMeshAgent.SetDestination(checkpost1.position);
            return;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        { 
            navMeshAgent.isStopped = true;
            // Debug.Log("Game Over");
            return;
        }

        time += Time.deltaTime;
        if (time >= 1) {
            time = 0;
            gun.shoot();
        }

        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        // if (other.GetComponent<Player>()) playerInSight = true;
    }
}
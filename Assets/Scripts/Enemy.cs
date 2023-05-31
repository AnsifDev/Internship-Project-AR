using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Player player;
    bool playerInSight;
    [SerializeField] private Transform checkpost1, checkpost2;
    [SerializeField] private Gun gun;
    [SerializeField] private Slider slider;
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

        time += Time.deltaTime;
        if (time >= 1) {
            time = 0;
            gun.Shoot(false);
        }

        if (Vector3.Distance(transform.position, player.transform.position) < navMeshAgent.stoppingDistance)
        { 
            navMeshAgent.isStopped = true;
            transform.LookAt(player.transform, Vector3.up);
            // Debug.Log("Game Over");
            return;
        }

        // Debug.Log(Vector3.Distance(transform.position, player.transform.position));

        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) playerInSight = true;
    }

    public void Damage() {
        slider.value -= 1;
        Debug.Log(slider.value);
        if (slider.value <= 0) Destroy(gameObject);
    }
}
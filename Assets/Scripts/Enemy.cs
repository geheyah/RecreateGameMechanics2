using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(gameObject);
        }
    }
}

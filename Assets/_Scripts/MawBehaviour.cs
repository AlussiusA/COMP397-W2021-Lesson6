using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MawState
{ 
    IDLE,
    RUN,
    JUMP
}

public class MawBehaviour : MonoBehaviour
{
    [Header("Line of Sight")]
    public bool HasLOS;

    public GameObject player;

    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLOS)
        {
            agent.SetDestination(player.transform.position);
            animator.SetInteger("AnumState", (int)MawState.RUN);

            if (Vector3.Distance(transform.position, player.transform.position) < 2.5)
            {
                // could be an attack
                animator.SetInteger("AnumState", (int)MawState.IDLE);
            }
        }
        else 
        {
            animator.SetInteger("AnumState", (int)MawState.IDLE);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.transform.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    NavMeshAgent agent;
    Transform playerTarget;
    Animator animator;


    float timer = 0;

    [SerializeField]
    float attackCooldown = 2f;

    [SerializeField]
    float damage = 2f;

    [SerializeField]
    float attackDistance = 10;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.FindWithTag("Cam").GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTarget = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Vector3.Distance(playerTarget.position, transform.position) < attackDistance)
        {
            animator.SetBool("Chasing", false);

            if (timer >= attackCooldown)
            {
                timer = 0;
                Attack();
            }
        }
        else
        {
            agent.SetDestination(playerTarget.position);
            animator.SetBool("Chasing", true);
        }
    }

     void OnDestroy()
    {
        audioSource.pitch = 1f;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        playerTarget.gameObject.GetComponent<HealthController>().TakeDamage(damage);
    }
}


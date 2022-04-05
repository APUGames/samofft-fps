using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Target will be Player
    [SerializeField] Transform target;

    // How close can Player get to Enemy before chased
    [SerializeField] float chaseRange = 5.0f;

    NavMeshAgent nMA;

    // How far the Enemy AI can look for Player
    float distanceToTarget = Mathf.Infinity;

    bool isProvoked = false;

    // Start is called before the first frame update
    void Start()
    {
        nMA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Measure the distance between Enemy and Player
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        if(distanceToTarget >= nMA.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= nMA.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("run");
        nMA.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
       // Temp for now
       // print(name + " is attacking " + target.name);
    }

    private void OnDrawGizmosSelected()
    {
        // Display the chase range when selected
        Gizmos.color = new Color(1,0,0,1.0f); // Choose color
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}

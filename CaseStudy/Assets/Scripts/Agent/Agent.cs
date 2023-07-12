using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude=10f;

    [SerializeField]
    private float timer;

    [SerializeField]
    private AgentManager agentManager;

    private NavMeshAgent agent;
    private float timerStartValue;
    private Rigidbody rb;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        agent=transform.GetComponent<NavMeshAgent>();
        timerStartValue=timer;
    }

    void Update()
    {
        timer-=Time.deltaTime;
        if(timer<=0)
        {
            agent.SetDestination(agentManager.FindClosestAgent(transform).localPosition);
            timer=timerStartValue;

        }
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.GetComponent<Agent>() || other.gameObject.GetComponent<PlayerMovement>())
        {
            Vector3 direction = transform.localPosition - other.transform.localPosition;
            rb.AddForce(direction.normalized * forceMagnitude, ForceMode.Impulse);
            

        }


    }
}

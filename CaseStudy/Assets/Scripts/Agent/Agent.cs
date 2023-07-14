using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour,IKillable
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
    private Animator _animation;

    public float ForceMagnitude { get{return forceMagnitude;} set{forceMagnitude=value;} }

    private void Awake() 
    {
        GameManager.Instance.GameStateChanged+=OnGameStateChanged;
        _animation=GetComponent<Animator>();
        rb=GetComponent<Rigidbody>();
        agent=transform.GetComponent<NavMeshAgent>();
    }
    void Start()
    {
       
        timerStartValue=timer;
    }

    void Update()
    {
       if(GameManager.Instance.CurrentGameState==GameState.InGame)
       {
            timer-=Time.deltaTime;
            if(timer<=0)
            {
                agent.SetDestination(agentManager.FindClosestAgent(transform).localPosition);
                timer=timerStartValue;

            }
       }
    }

    public void Die()
    {
        agentManager.DecreaseAgentCount(gameObject);
    }

    private void OnCollisionEnter(Collision other) 
    {
        IKillable ikillable=other.gameObject.GetComponent<IKillable>();
        
        if(ikillable!=null)
        {
            _animation.SetTrigger("OnKick");
            Vector3 directionCollider=other.transform.localPosition-transform.localPosition;
            Vector3 objectForward=transform.forward;
            float angle=Vector3.Angle(directionCollider,objectForward);
            if(angle<180)
            {
                Vector3 direction = transform.localPosition - other.transform.localPosition;
                rb.AddForce(direction.normalized * forceMagnitude, ForceMode.Impulse);
            }
            else
            {
                Vector3 direction = transform.localPosition - other.transform.localPosition;
                rb.AddForce(direction.normalized * forceMagnitude*.75f, ForceMode.Impulse);
            }
            
            
            

        }


    }
    private void OnGameStateChanged(GameState state)
    {
         switch (state)
        {
            
            case GameState.Start:
                break;
            case GameState.InGame:
                _animation.SetTrigger("OnRun");
                break;
            case GameState.Fail:
                break;
            case GameState.Succes:
                break;
        }
    }
}

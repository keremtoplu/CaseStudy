using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IKillable
{
    [SerializeField]
    private float forceMagnitude=10f;
    private Rigidbody rb;

    private Animator _animation;
    private void Awake() 
    {
        GameManager.Instance.GameStateChanged+=OnGameStateChanged;
        rb=GetComponent<Rigidbody>();
        _animation=GetComponent<Animator>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        _animation.SetTrigger("OnFall");
        GameManager.Instance.UpdateGameState(GameState.Fail);
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
                rb.AddForce(direction.normalized * forceMagnitude*0.75f, ForceMode.Impulse);
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

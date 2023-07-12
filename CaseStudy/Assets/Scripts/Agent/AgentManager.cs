using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> agentList=new List<GameObject>();
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public Transform FindClosestAgent(Transform agent)
    {
        agentList.Remove(agent.gameObject);
        Transform nearObject=agentList[0].transform;
        var nearDifference=Vector3.Distance(agent.localPosition,agentList[0].transform.position);
        for (int i = 0; i < agentList.Count; i++)
        {
            if(Vector3.Distance(agent.localPosition,agentList[i].transform.localPosition)<nearDifference)
            {
                nearObject=agentList[i].transform;
            }
        }
        agentList.Add(agent.gameObject);
        return nearObject;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AgentManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> agentList=new List<GameObject>();
    [SerializeField]
    private int agentDieScore=5;
    private int totalAgentDieScore=0;
    private int currentAgentCount;

    private List<GameObject> removedAgentList=new List<GameObject>();
    
    private void Awake() 
    {
        GameManager.Instance.GameStateChanged+=OnGameStateChanged;
    }
    void Start()
    {
        if(removedAgentList.Count>0)
        {
            for (int i = 0; i < removedAgentList.Count; i++)
            {
                agentList.Add(removedAgentList[i]);
            }
        }
        currentAgentCount=agentList.Count-1;
    }
    void Update()
    {
        
    }
    
    public void DecreaseAgentCount(GameObject agent)
    {
        currentAgentCount--;
        totalAgentDieScore+=agentDieScore;
        UIManager.Instance.ScoreText.text=totalAgentDieScore.ToString();
        
        if(currentAgentCount<=0)
        {
            GameManager.Instance.UpdateGameState(GameState.Succes);
        }
        agentList.Remove(agent);
        removedAgentList.Add(agent);
        agent.GetComponent<Animator>().SetTrigger("OnFall");
        for (int i = 0; i < agentList.Count; i++)
        {
            if(agentList[i].gameObject.GetComponent<Agent>())
                agentList[i].GetComponent<Agent>().ForceMagnitude+=0.3f;
        }
        LeanTween.delayedCall(.75f,()=>{agent.SetActive(false);});
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

    
    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            
            case GameState.Start:
                break;
            case GameState.InGame:
                break;
            case GameState.Fail:
                break;
            case GameState.Succes:
                break;
        }
    }
}

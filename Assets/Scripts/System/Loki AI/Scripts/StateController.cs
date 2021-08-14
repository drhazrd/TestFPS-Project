using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{

    public State currentState;                     
    public EnemyStats enemyStats;                  
    public Transform eyes;                         
    public State remainState;                      

    [HideInInspector]
    public NavMeshAgent navMeshAgent;
   
    [HideInInspector]
    public List<Transform> wayPointList;            
    [HideInInspector]
    public int nextWayPoint;                     
    [HideInInspector]
    public Transform chaseTarget;                 
    [HideInInspector]
    public float stateTimeElapsed;                 

    private bool aiActive;                        
    private State startState;                       
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        startState = currentState;
    }

    private void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);             //更新状态
    }

    private void OnEnable()
    {
        currentState = startState;                  //复活的时候重置状态
        nextWayPoint = Random.Range(0, wayPointList.Count);     //随即巡逻点
    }

    //设置巡逻点还有是否设置AI并且是否激活导航
    public void SetupAI(bool aiActivationFromTankManager, List<Transform> wayPointsFromGameManager)
    {
        wayPointList = wayPointsFromGameManager;
        aiActive = aiActivationFromTankManager;
        if (aiActive)
            navMeshAgent.enabled = true;
        else
            navMeshAgent.enabled = false;
    }
    private void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
        }
    }

    //转换到下一个状态
    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    //返回是否过了时间间隔
    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }

}
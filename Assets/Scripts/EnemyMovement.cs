using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public bool targetLocated,chasing;
    public float moveSpeed, rotateSpeed, distanceToChase = 6f,distanceToLose = 10f, distanceToStop = 2.5f;
    //public Rigidbody enemyRB;
    public Transform target;
    Vector3 targetPoint, startPoint;
    public Vector3[] navPoints;
    public int navPointID;
    public NavMeshAgent enemyAgent;
    public float keepChaseTimer = 5f;
    private float chaseCounter;
    // Use this for initialization
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        //target = PlayerController.instance.transform;
        targetPoint.y = transform.position.y;


        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;
                targetLocated = true;
            }
            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;
                if (chaseCounter <= 0)
                {
                    enemyAgent.destination = startPoint;
                }
            }
        }
        else
        {
            //transform.LookAt(targetPoint);

            //enemyRB.velocity = transform.forward * moveSpeed;
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                enemyAgent.destination = targetPoint;
            }
            else
            {
                enemyAgent.destination = transform.position;
            }

            enemyAgent.destination = targetPoint;

            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                chasing = false;
                targetLocated = false;
                //enemyAgent.destination = startPoint;
                chaseCounter = keepChaseTimer;
            }
        }
    }
}

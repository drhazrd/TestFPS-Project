using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public bool targetLocated, chasing;
    public float rotateSpeed, distanceToChase = 6f, distanceToLose = 10f, distanceToStop = 2.5f;
    public Transform target;
    Vector3 targetPoint, startPoint;
    public Vector3[] navPoints;
    public int navPointID;
    public NavMeshAgent enemyAgent;
    public Animator anim;
    public float keepChaseTimer = 5f;
    private float chaseCounter;
    private bool wasShot;
    public GameObject bullet;
    public Transform firepoint;
    public float fireRate, waitBetweenShot = 2f, timeToShoot, firstFireWait = 1f;
    float fireRateTimer, shotWaitTimer, shootTimer, aimPoint = .5f;


    // Use this for initialization
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        startPoint = transform.position;
        shootTimer = timeToShoot;
        shotWaitTimer = waitBetweenShot;
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
                shootTimer = timeToShoot;
                shotWaitTimer = waitBetweenShot;
            }
            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
                {
                    enemyAgent.destination = startPoint;
                }
            }

            if (enemyAgent.remainingDistance < .25)
                anim.SetBool("isMoving", false);
            else
                anim.SetBool("isMoving", true);

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


            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                if (!wasShot)
                {
                    chasing = false;
                    chaseCounter = keepChaseTimer;
                }
                targetLocated = false;
                //enemyAgent.destination = startPoint;
            }
            else
            {

            }
            if (shotWaitTimer > 0)
            {
                shotWaitTimer -= Time.deltaTime;

                if (shootTimer <= 0)
                {
                    shootTimer = timeToShoot;
                }
                anim.SetBool("isMoving", true);
            }
            else
            {
                if (PlayerController.instance.gameObject.activeInHierarchy)
                {


                    shootTimer -= Time.deltaTime;


                    if (shootTimer > 0)
                    {


                        fireRateTimer -= Time.deltaTime;

                        if (fireRateTimer <= 0)
                        {
                            fireRateTimer = fireRate;

                            firepoint.LookAt(targetPoint + new Vector3(0, aimPoint, 0));

                            //Check player angle

                            Vector3 targetDir = targetPoint - transform.position;
                            float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);
                            if (Mathf.Abs(angle) < 30f)
                            {
                                Instantiate(bullet, firepoint.position, firepoint.rotation);
                                anim.SetTrigger("fireShot");
                            }
                            else
                            {
                                shotWaitTimer = waitBetweenShot;
                            }
                        }
                        enemyAgent.destination = transform.position;
                    }
                    else
                    {
                        shotWaitTimer = waitBetweenShot;
                    }
                }
                anim.SetBool("isMoving", false);
            }
        }
    }
    public void GetShot()
    {
        wasShot = true;
        chasing = true;
        //enemyAgent.destination = PlayerController.instance.transform.position;
    }
}

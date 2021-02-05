using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //public Camera cam;

    public NavMeshAgent agent;

    public Vector3 centre;
    public int MaxDistance;

    // Update is called once per frame

    /*void Start()
    {
        MaxX = (MazeSize - 1) * 4 + BoundX;
        MaxZ = (MazeSize - 1) * 4 + BoundZ;

    }*/

    public float lookRadius = 10f;

    Transform target;
    private float nextActionTime = 0.0f;
    public float period = 1f;
    public int Damage = 1;

    HealthBar play;
    //HealthBar play = PlayerManager.instance.player.GetComponentInChildren<Canvas>().instance.GetComponentInChildren<HealthBar>().GetComponent<HealthBar>();
    //CharacterCombat combatManager;

    void Start()
    {
        target = PlayerList.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        play = PlayerList.instance.player.GetComponent<PlayerMovement>().healthBar;
        //combatManager = GetComponent<CharacterCombat>();
    }

    void Update()
    {
        /*if (agent.remainingDistance < 0.1f)
        {
            agent.SetDestination(GetRandomPoint(centre,MaxDistance));
        }*/

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    agent.SetDestination(GetRandomPoint(centre, MaxDistance));
                }
            }
        }

        /*if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }*/

        // Get the distance to the player
        float distance = Vector3.Distance(target.position, transform.position);

        // If inside the radius
        if (distance <= lookRadius)
        {
            // Move towards the player
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                // Attack
               // InvokeRepeating("attackPlayer()", 0, 1.0f);

                
                //combatManager.Attack(Player.instance.playerStats);
                FaceTarget();
                if (Time.time > nextActionTime)
                {
                    //nextActionTime += period;
                    nextActionTime = Time.time + period;
                    attackPlayer();
                }
            }
        }

    }

    public static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        // Get Random Point inside Sphere which position is center, radius is maxDistance
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        //randomPos.y = center.y;

        NavMeshHit hit; // NavMesh Sampling Info Container

        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);
        //UnityEngine.Debug.Log(hit.position);
        return hit.position;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void attackPlayer()
    {
        int current = play.GetHealth();
        play.SetHealth(current - Damage);
    }
}

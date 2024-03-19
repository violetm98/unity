using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MinionAI : MonoBehaviour
{
    private Animator anim;

    private NavMeshAgent navMeshAgent;
    private NavMeshHit hit;

    public GameObject[] waypoints;
    private int currWaypoint = -1;
    private int counter = 0;

    public Transform movingWaypoint;
    private float captureDistance = 0.5f;

    enum AIState
    {
        
        MoveToStaticTarget,
        MoveToMovingTarget
    }
    private AIState aiState;

    public GameObject tracker;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        anim = GetComponent<Animator>();
        
        aiState = AIState.MoveToStaticTarget;

        setNextWaypoint();
    }

    
    void Update()
    {
        anim.SetFloat("vely", navMeshAgent.velocity.magnitude / navMeshAgent.speed);

        switch (aiState)
        {
            case AIState.MoveToStaticTarget:
                
                //the agent finish caculating & reached the target waypoint
                if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    setNextWaypoint();
                    counter++;
                    
                }
                if (counter >= 5)
                {
                    aiState = AIState.MoveToMovingTarget;
                    //reset
                    counter = 0;
                    currWaypoint = -1;
                }

                break;

            case AIState.MoveToMovingTarget:

                float distToMovingWaypoint = Vector3.Distance(movingWaypoint.position, transform.position);
                
                float lookaheadTime = Mathf.Clamp(distToMovingWaypoint / navMeshAgent.speed, 0f, 8f);
             
                
                Vector3 futureWaypointPosition = movingWaypoint.position + movingWaypoint.GetComponent<VelocityReporter>().velocity * lookaheadTime;
                

                if(NavMesh.Raycast(movingWaypoint.position, futureWaypointPosition, out hit, NavMesh.AllAreas))
                {
                    
                    futureWaypointPosition = hit.position;
                }

                navMeshAgent.SetDestination(futureWaypointPosition);

                tracker.transform.position = futureWaypointPosition;
                Debug.Log(distToMovingWaypoint);
                if (!navMeshAgent.pathPending && distToMovingWaypoint - captureDistance <= navMeshAgent.stoppingDistance)
                {
                    aiState = AIState.MoveToStaticTarget;
                    setNextWaypoint();
                }
                break;





        }
        
    }

    private void setNextWaypoint()
    {
        if(waypoints.Length == 0)
        {
            Debug.Log("Waypoints could not be found");
            return;
        }
        
      
        currWaypoint = (currWaypoint + 1) % waypoints.Length;
     

        navMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);



    }
}

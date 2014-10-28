using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour 
{
	public float patrolSpeed = 2f;                          // The nav mesh agent's speed when patrolling.
    public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
    public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
    public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
    public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.
    
    
    private EnemySight enemySight;                          // Reference to the EnemySight script.
    private NavMeshAgent nav;                               // Reference to the nav mesh agent.
    private Transform player;                               // Reference to the player's transform.
    private PlayerHealth playerHealth;                      // Reference to the PlayerHealth script.
    private LastPlayerSighting lastPlayerSighting;          // Reference to the last global sighting of the player.
    private float chaseTimer;                               // A timer for the chaseWaitTime.
    private float patrolTimer;                              // A timer for the patrolWaitTime.
    private int wayPointIndex;                              // A counter for the way point array.
    
    
    void Awake ()
    {
        // Setting up the references.
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

	void Update()
	{
		if( enemySight.playerInSight && playerHealth.health > 0f )
		{
			Shooting();
		}
		else if( enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0f )
		{
			Chasing ();
		}
		else
		{
			Patrolling();
		}
	}

	void Shooting() 
	{
		nav.Stop();

	}

	void Chasing() 
	{
		Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
		if( sightingDeltaPos.sqrMagnitude > 4f )
		{
			nav.destination = enemySight.personalLastSighting;
		}

		nav.speed = chaseSpeed;
		if( nav.remainingDistance < nav.stoppingDistance )
		{
			chaseTimer += Time.deltaTime;
			if( chaseTimer > chaseWaitTime )
			{
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
				chaseTimer = 0f;
			}
		}
		else 
		{
			chaseTimer = 0f;
		}
	}

	void Patrolling() 
	{
		nav.speed = patrolSpeed;

		if( nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance )
		{
			patrolTimer += Time.deltaTime;
			if( patrolTimer >= patrolWaitTime )
			{
				if( wayPointIndex == patrolWayPoints.Length - 1 )
				{
					wayPointIndex = 0;
				}
				else 
				{
					wayPointIndex++;
				}
			}
		}
		else
		{
			patrolTimer = 0f;
		}

		nav.destination = patrolWayPoints[wayPointIndex].position;
	}





}

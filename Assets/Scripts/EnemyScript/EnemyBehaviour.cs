using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourState { none, wander, pursue, attack, escape }

public class EnemyBehaviour : MonoBehaviour
{
    public BehaviourState initialState;
    private BehaviourState currentState = BehaviourState.none;
    public Bounds boundBox;
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 wanderPos;
    float wanderDistance;
    public GameObject player;
    float playerDistance;

    public bool attack; //Testing Attack Purpose

    //Temporary since will create State Machine later on
    //----- ----- ----- ----- State Management ----- ----- ----- -----
    public void SetState(BehaviourState s)
    {
        if (currentState != s)
        {
            currentState = s;
            switch (s)
            {
                case BehaviourState.wander:
                    FindWanderPosition();
                    break;
                case BehaviourState.pursue:
                    MoveToPlayer();
                    break;
                case BehaviourState.attack:
                    AttackPlayer();
                    break;
                case BehaviourState.escape:
                    break;
            }
        }
    }

    //----- ----- ----- ----- Wandering State Methods ----- ----- ----- -----
    private void FindWanderPosition()
    {
        wanderPos = GetRandomPoint();
        agent.SetDestination(wanderPos);
        agent.isStopped = false; //So Agent will no stop
    }

    private Vector3 GetRandomPoint()
    {
        float randomX = Random.Range(-boundBox.extents.x + agent.radius, boundBox.extents.x - agent.radius);
        float randomZ = Random.Range(-boundBox.extents.z + agent.radius, boundBox.extents.z - agent.radius);
        return new Vector3(randomX, transform.position.y, randomZ);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(wanderPos, 0.2f);
    }

    //----- ----- ----- ----- Pursuing State Methods ----- ----- ----- -----
    private void MoveToPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void CheckPlayerDistance()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (playerDistance <= 5)
            SetState(BehaviourState.attack);
    }

    //----- ----- ----- ----- Attacking State Methods ----- ----- ----- -----
    private void AttackPlayer()
    {
        GetComponent<EnemyMain>().AttackPlayer(); //Voir si ça marche ou si il faut aller avec EnemyMelee et EnemyRange
    }


    //----- ----- ----- ----- Escaping State Methods ----- ----- ----- -----
    private void EscapePlayer()
    {
        if (GetComponent<EnemyMain>().Hp <= 30)
        {
            SetState(BehaviourState.escape);
            FindWanderPosition();
            agent.speed += 20;
            agent.acceleration += 8;
        }
    }

    //----- ----- ----- ----- On Scene Methods ----- ----- ----- -----
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovements>())
        {
            SetState(BehaviourState.pursue);
            player = other.gameObject;
        }
    }

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        if(attack) //Testing Attack Purpose
        {
            AttackPlayer();
            attack = false;
        }

        switch (currentState)
        {
            case BehaviourState.wander:
                wanderDistance = Vector3.Distance(wanderPos, transform.position);
                if (wanderDistance <= agent.stoppingDistance)
                    FindWanderPosition();
                break;
            case BehaviourState.pursue:
                MoveToPlayer();
                break;
            case BehaviourState.attack:
                AttackPlayer();
                break;
            case BehaviourState.escape:
                playerDistance = Vector3.Distance(transform.position, player.transform.position);
                if (playerDistance >= 20)
                {
                    SetState(BehaviourState.wander);
                    agent.speed -= 20;
                    agent.acceleration -= 8;
                }
                break;
        }
    }
}

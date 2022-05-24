using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BehaviourState { none, wander, pursue, attack, escape } 

public class EnemyBehaviour : MonoBehaviour
{
   // public BehaviourState initialState; 
    //public BehaviourState currentState = BehaviourState.none; 
    public Bounds boundBox; 
    public NavMeshAgent agent; 
    //private Vector3 wanderPos; 
    //private float wanderDistance; 
    public GameObject player;
    //private float playerDistance;

    //public bool attack; //Testing Attack Purpose
    //public bool escape; //Testing Escape Purpose

    public Animator enemyAnim;
    public EnemyState currState;
    public StateWander stateWander;
    public bool canSeePlayer;


    //Temporary since will create State Machine later on
    //----- ----- ----- ----- State Management ----- ----- ----- -----
    /*public void SetState(BehaviourState s) 
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
    }*/

    //----- ----- ----- ----- Wandering State Methods ----- ----- ----- -----
    /*private void FindWanderPosition()
    {
        wanderPos = GetRandomPoint(); 
        agent.SetDestination(wanderPos); 
        agent.isStopped = false; 
    }

    private Vector3 GetRandomPoint() 
    {
        float randomX = Random.Range(-boundBox.extents.x + agent.radius, boundBox.extents.x - agent.radius); 
        float randomZ = Random.Range(-boundBox.extents.z + agent.radius, boundBox.extents.z - agent.radius); 
        return new Vector3(randomX, transform.position.y, randomZ); 
    }
    */

    //----- ----- ----- ----- Pursuing State Methods ----- ----- ----- -----
    /*private void MoveToPlayer() 
    {
        agent.SetDestination(player.transform.position); 
    }

    private void CheckPlayerDistance() 
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (playerDistance <= 5)
            SetState(BehaviourState.attack); 
    }*/

    //----- ----- ----- ----- Attacking State Methods ----- ----- ----- -----
    /*private void AttackPlayer() 
    {
        GetComponent<EnemyMain>().AttackPlayer(); 
    }*/

    //----- ----- ----- ----- Escaping State Methods ----- ----- ----- -----
    /*private void EscapePlayer() 
    {
        if (GetComponent<EnemyMain>().Hp <= 30) 
        {
            SetState(BehaviourState.escape);
            FindWanderPosition(); 
            agent.speed = 7; 
            agent.acceleration = 12; 
        }
    }*/

    //----- ----- ----- ----- On Scene Methods ----- ----- ----- -----
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovements>())
        {
            //SetState(BehaviourState.pursue);
            player = other.gameObject; 
            canSeePlayer = true; 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; 
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
    }

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        currState = stateWander;
        //SetState(initialState); 
    }

    // Update is called once per frame
    void Update()
    {
        /*if(attack) //Testing Attack Purpose
        {
            AttackPlayer(); 
            attack = false; 
        }*/
        /*if(escape) //Testing Escape Purpose
        {
            EscapePlayer(); 
            escape = false; 
        }*/

        /*switch (currentState) 
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
                if (playerDistance >= 10) 
                {
                    SetState(BehaviourState.wander); 
                    agent.speed = 3.5f; 
                    agent.acceleration = 8; 
                    GetComponent<EnemyMain>().Hp = 50; 
                }
                break;
        }*/

        RunStateMachine();
    }

    //Testing StateMachine
    private void RunStateMachine() 
    {
        EnemyState nextState = currState?.RunState(this); 

        if(nextState != null) 
        {
            SwitchNextState(nextState); 
        }
    }

    private void SwitchNextState(EnemyState nextState) 
    {
        currState = nextState;
    }
}

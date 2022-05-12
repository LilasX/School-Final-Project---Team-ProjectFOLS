using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//There is two types of State Management. One using only EnemyBehaviour (System #1), and one using multiple State Script (System #2)

public enum BehaviourState { none, wander, pursue, attack, escape } //Enum possessing the Different State of Enemy AI. System #1

public class EnemyBehaviour : MonoBehaviour
{
   // public BehaviourState initialState; //Enum possessing a State, change in the Inspector, that will be used at the beginning of the Scene
    //public BehaviourState currentState = BehaviourState.none; //Enum used for the Current State this gameObject is on. System #1
    public Bounds boundBox; //Used for Wander State. A Box Boundary.
    public UnityEngine.AI.NavMeshAgent agent; //NavMeshAgent to use the agent of this gameObject in this script
    //private Vector3 wanderPos; //Vector3 to place the target position for the enemy to move towards to
    //private float wanderDistance; //float variable to keep the distance between enemy and the wanderPos
    public GameObject player; //The Player so that it can be used in this scripts
    //private float playerDistance; //float variable to keep the distance between player and the enemy

    //public bool attack; //Testing Attack Purpose
    //public bool escape; //Testing Escape Purpose

    public EnemyState currState;//Testing State Machine. currentState of this enemy. System #2
    public bool canSeePlayer; //Bool to see if Enemy can see Player

    //Temporary since will create State Machine later on
    //----- ----- ----- ----- State Management ----- ----- ----- -----
    /*public void SetState(BehaviourState s) //Method to set this gameObject's current state. System #1
    {
        if (currentState != s) //if currentState isn't the same as the state given (s)
        {
            currentState = s; //currentState is now s
            switch (s) //switch case using s
            {
                case BehaviourState.wander: //if s is wander
                    FindWanderPosition(); //Call FindWanderPosition Method
                    break;
                case BehaviourState.pursue: //if s is pursue
                    MoveToPlayer(); //Call MoveToPlayer Method
                    break;
                case BehaviourState.attack: //if s is attack
                    AttackPlayer(); //Call AttackPlayer Method
                    break;
                case BehaviourState.escape: //if s is escape
                    break;
            }
        }
    }*/

    //----- ----- ----- ----- Wandering State Methods ----- ----- ----- -----
    /*private void FindWanderPosition() //Method to Set a new wanderPos. System #1
    {
        wanderPos = GetRandomPoint(); //Set wanderPos to return of GetRandomPoint Method
        agent.SetDestination(wanderPos); //set agent's destination to wanderPos
        agent.isStopped = false; //Set agent.isStopped to false so Agent will not stop
    }

    private Vector3 GetRandomPoint() //Method to Retrieve a new Vector3. System #1
    {
        float randomX = Random.Range(-boundBox.extents.x + agent.radius, boundBox.extents.x - agent.radius); //Retrieve a Random X from a range
        float randomZ = Random.Range(-boundBox.extents.z + agent.radius, boundBox.extents.z - agent.radius); //Retrieve a Random Z from a range
        return new Vector3(randomX, transform.position.y, randomZ); //return a Vector3 with the random X and Z Coordinates.
    }
    */

    //----- ----- ----- ----- Pursuing State Methods ----- ----- ----- -----
    /*private void MoveToPlayer() //Method to Move the Enemy to the Player. System #1
    {
        agent.SetDestination(player.transform.position); //set agent's destination to player's position
    }

    private void CheckPlayerDistance() //Method to Check the Distance between the Player and the Enemy. System #1
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position); //playerDistance is set to the distance between this gameObject's position and player's position

        if (playerDistance <= 5) //if playerDistance is equal or inferior to 5
            SetState(BehaviourState.attack); //Set this gameObject's state to Attack
    }*/

    //----- ----- ----- ----- Attacking State Methods ----- ----- ----- -----
    /*private void AttackPlayer() //Method to Attack the Player. System #1
    {
        GetComponent<EnemyMain>().AttackPlayer(); //Call AttackPlayer Method from the script EnemyMain of this gameObject
    }*/

    //----- ----- ----- ----- Escaping State Methods ----- ----- ----- -----
    /*private void EscapePlayer() //Method to Escape from Player when Health is Low. System #1
    {
        if (GetComponent<EnemyMain>().Hp <= 30) //If Hp of this gameObject is equal or inferior to 30
        {
            SetState(BehaviourState.escape); //Set this gameObject's state to escape
            FindWanderPosition(); //Call FindWanderPosition Method to receive a Random Coordinate to Escape 
            agent.speed = 7; //Increase agent's speed to 7
            agent.acceleration = 12; //Increase agent's acceleration to 12
        }
    }*/

    //----- ----- ----- ----- On Scene Methods ----- ----- ----- -----
    private void OnTriggerEnter(Collider other) //OnTriggerEnter Method
    {
        if (other.GetComponent<PlayerMovements>()) //if collider other is a gameObject possessing the script PlayerMovements
        {
            //SetState(BehaviourState.pursue); //Set this gameObject's state to pursue. System #1
            player = other.gameObject; //Set player to the gameObject of the collider
            canSeePlayer = true; //Set canSeePlayer to True; System #2
        }
    }

    private void OnDrawGizmos() //Method to Draw the BoundBox and the wanderPos in the Scene. Will Delete when not Necessary. System #1
    {
        Gizmos.color = Color.yellow; 
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
    }

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Set agent to this gameObject's NavMeshAgent
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetState(initialState); //Set Current State to Initial State set in Inspector. System #1
    }

    // Update is called once per frame
    void Update()
    {
        /*if(attack) //Testing Attack Purpose
        {
            AttackPlayer(); //Call AttackPlayer Method
            attack = false; //Set bool attack to false
        }*/
        /*if(escape) //Testing Escape Purpose
        {
            EscapePlayer(); //Call EscapePlayer Method
            escape = false; //Set bool escape to false
        }*/

        /*switch (currentState) //Switch Case using CurrentState. System #1
        {
            case BehaviourState.wander: //If CurrentState is wander
                wanderDistance = Vector3.Distance(wanderPos, transform.position); //Get Distance between wanderPos and this gameObject's position
                if (wanderDistance <= agent.stoppingDistance) //if wanderDistance is equal or inferior to this gameObject's agent's stoppingDistance
                    FindWanderPosition(); //Call FindWanderPosition Method
                break;
            case BehaviourState.pursue: //If CurrentState is pursue
                MoveToPlayer(); //Call MoveToPlayer Method
                break;
            case BehaviourState.attack: //If CurrentState is attack
                AttackPlayer(); //Call AttackPlayer Method
                break;
            case BehaviourState.escape: //If CurrentState is escape
                playerDistance = Vector3.Distance(transform.position, player.transform.position); //Get Distance between player's position and this gameObject's position
                if (playerDistance >= 10) //if playerDistance is equal or superior to 10
                {
                    SetState(BehaviourState.wander); //Set this gameObject's state to wander
                    agent.speed = 3.5f; //Set agent's speed to 3.5 (original speed)
                    agent.acceleration = 8; //Set agent's acceleration to 8 (original acceleration)
                    GetComponent<EnemyMain>().Hp = 50; //Heal this gameObject to 50
                }
                break;
        }*/

        RunStateMachine(); //Testing StateMachine. Call RunStateMachine Method. System #2
    }

    //Testing StateMachine
    private void RunStateMachine() //Method to Run the State Machine. System #2
    {
        EnemyState nextState = currState?.RunState(this); //if current state isn't null and is running this state, set it to next state

        if(nextState != null) //if nextState isn't null
        {
            SwitchNextState(nextState); //Call SwitchNextState Method and sending it nextState;
        }
    }

    private void SwitchNextState(EnemyState nextState) //Method to Switch the current State to Next State. System #2
    {
        currState = nextState; //current State is set to next state
    }
}

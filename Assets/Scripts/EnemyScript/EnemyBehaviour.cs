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
    public bool spawningBox = false;
    public NavMeshAgent agent; 
    //private Vector3 wanderPos; 
    //private float wanderDistance; 
    public GameObject player;
    public GameManager manager;
    //private float playerDistance;

    //public bool attack; //Testing Attack Purpose
    //public bool escape; //Testing Escape Purpose

    public Animator enemyAnim;
    public EnemyState currState;
    public StateWander stateWander;
    //public bool canSeePlayer;


    public void SetBoundBox (Bounds spawnBoundBox)
    {
        boundBox.center = spawnBoundBox.center;
        boundBox.extents = spawnBoundBox.extents;
        spawningBox = true;
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
        manager = GameManager.instance;
        currState = stateWander;
        if (!spawningBox)
        {
            //boundBox.center = gameObject.transform.position;
            //boundBox.extents = new Vector3(); //for BoundBox of Enemy in the arena //Have WaveSpawner Keep Vector3 for extents
        }
        player = manager.player;
        //SetState(initialState); 
    }

    // Update is called once per frame
    void Update()
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_ScoutDrone : MonoBehaviour
{
    #region VARIABLES
    // Declaration
    public enum State // The behaviour states of the enemy AI.
    {
        Patrol = 0,
        Seek = 1,
        Investigate = 2
    }

    [Header("Components")]
    //public NavMeshAgent agent;
    public Transform target; // Reference assigned target's Transform data (position/rotation/scale).
    public Transform waypointParent; // Reference one waypoint Parent (used to get children in array).
    public BossFoV_SearchLight fov; // Reference FieldOfView Script (used for line of sight player detection).

    [Header("SearchLight")]
    public Light searchLight; // Reference Light (child 'SearchLight').
    public Color colorPatrol = colp;
    public Color colorSeek = cols;

    [Header("Behaviours")]
    public State currentState = State.Patrol; // The default/start state set to Patrol.

    public float speedPatrol = 4f, speedSeek = 4f; // Movement speeds for different states (up to you).
    public float stoppingDistance = 1f; // Enemy AI's required distance to clear/'pass' a waypoint.

    public float pauseDuration; // Time to wait before going to the next waypoint.
    private float waitTime, lookTime; // Defined later as UnityEngine 'Time.time'.

    [Header("Animations")]
    public Animator anim;

    // Creates a collection of Transforms
    private Transform[] waypoints; // Transform of (child) waypoints in array.
    private int currentIndex = 1; // Counts sequential waypoints of array index.
    #endregion VARIABLES

    #region STATE - Patrol
    // The contained variables for the Patrol state (what rules the enemy AI follows when in 'Patrol').
    void Patrol()
    {
        // Transform(s) of each waypoint in the array.
        Transform point = waypoints[currentIndex];

        // Gets the distance between enemy and waypoint.
        float distance = Vector3.Distance(transform.position, point.position);
        #region if statement logic
        // if statement reads as:
        /*
         *  if the enemy AI's distance to the waypoint is less than 0.5...
         *      and (&& breaks in previous argument) if curTime's equality is 0...
         *          curTime = Time(using Unity Engine's time).time(get the time at beginning of this frame in seconds since the start of the game).
         *      
         *      if the time is greater than or equal to the pauseDuration...
         *          add +1 to currentIndex (move to next waypoint in array).
         *          reset curTime time to 0.
         *          
         *          if enemy AI clears the final waypoint in array...
         *              reset currentIndex to 1 (return/repeat cycle).
        */
        #endregion
        if (distance < .5f)
        {
            if (waitTime == 0)
                waitTime = Time.time;

            if ((Time.time - waitTime) >= pauseDuration)
            {
                currentIndex++;
                waitTime = 0;

                if (currentIndex >= waypoints.Length)
                {
                    currentIndex = 1;
                }
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, point.position, 0.1f);

        #region RotateTowards waypoint
        // Direction of point (waypoint) from current position.
        Vector3 pointDir = point.position - transform.position;

        float step = speedPatrol * Time.deltaTime;

        // Rotate front face of ScoutDrone towards pointDir.
        Vector3 newDir = Vector3.RotateTowards(transform.forward, pointDir, step, 0.0f);

        //float angle = Vector3.Angle(Vector3.right, pointDir.normalized);
        //Vector3 euler = transform.eulerAngles;
        //euler.y = angle;
        //transform.eulerAngles = euler;

        if (pointDir.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(pointDir.normalized, Vector3.up);
            transform.rotation *= Quaternion.Euler(90, 0, 0);
        }

        // Execute rotation using newDir.
        //transform.rotation = Quaternion.LookRotation(newDir);
        #endregion

        if (fov.visibleTargets.Count > 0)
        {
            currentState = State.Seek;
            target = fov.visibleTargets[0];
        }
    }
    #endregion STATE - Patrol

    #region STATE - Seek
    // The contained variables for the Seek state (what rules the enemy AI follows when in 'Seek').
    void Seek()
    {
        #region RotateTowards player
        // Direction of target (player) from current position.
        Vector3 targetDir = target.position - transform.position;

        float step = speedPatrol * Time.deltaTime;

        // Rotate front face of ScoutDrone towards targetDir.
        Vector3 newTarDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

        //float angle = Vector3.Angle(Vector3.right, targetDir.normalized);
        //Vector3 euler = transform.eulerAngles;
        //euler.y = angle;
        //transform.eulerAngles = euler;

        if (targetDir.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(targetDir.normalized, Vector3.up);
            transform.rotation *= Quaternion.Euler(0, 0, 0);
        }
        #endregion

        // Makes AI wait after losing line of sight of the player. 'lookTime' instead of 'waitTime' to ensure AI still waits at next waypoint.
        if (fov.visibleTargets.Count < 1)
        {
            if (lookTime == 0)
                lookTime = Time.time;

            if ((Time.time - lookTime) >= pauseDuration)
            {
                lookTime = 0;
                currentState = State.Patrol;

                if(fov.visibleTargets.Count > 0)
                    target = fov.visibleTargets[0];
            }
        }
        //fov.viewRadius = 10f; // FieldOfView arc radius during 'Seek'.
    }
    #endregion STATE - Seek

    #region STATE - Investigate
    public void Investigate(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, 0.1f);
        //agent.SetDestination(noisePos.position);
        currentState = State.Investigate;
    }
    #endregion

    #region Start
    // Use this for initialization
    void Start()
    {
        // Get children of waypointParent.
        waypoints = waypointParent.GetComponentsInChildren<Transform>();

        // Get Light component from child in GameObject.
        searchLight = GetComponentInChildren<Light>();
    }
    #endregion Start

    #region Update
    // Update is called once per frame
    void Update()
    {
        // Switch current state
        switch (currentState)
        {
            case State.Patrol:
                // Patrol state
                Patrol();
                searchLight.color = colorPatrol;
                anim.SetBool("hasTarget", false);
                break;
            case State.Seek:
                // Seek state
                Seek();
                searchLight.color = colorSeek;
                anim.SetBool("hasTarget", true);
                break;
            case State.Investigate:
                // Run this code while in investigate state
                // If the agent gets close to the investigate position
                if(stoppingDistance < 0.5f)
                {
                    // Note(Manny): Why not wait for 5 seconds here (timer)
                    // Switch to Patrol
                    currentState = State.Patrol;
                }

                // If the agent sees the player
                if (fov.visibleTargets.Count > 0)
                {
                    // Switch over to seek
                    currentState = State.Seek;
                    // Seek towards the visible target
                    target = fov.visibleTargets[0];
                }
                break;                
            default:
                break;
        }
        // If we are in Patrol State...
        // Call Patrol()
        // If we are in Seek State...
        // Call Seek()
    }
    #endregion Update

    public static Color colp = new Color(0.8039216f - 0 / 100, 0.4019608f - 0 / 100, 0);
    public static Color cols = new Color(0.8039216f - 0 / 100, 0, 0);
}

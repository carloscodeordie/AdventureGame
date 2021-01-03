using UnityEngine;
using RPG.Core;
using RPG.Combat;
using RPG.Movement;
using System;

namespace RPG.Control {

    public class AIController : MonoBehaviour
    {
        // Editor properties
        [SerializeField] public float chaseDistance = 5f;
        [SerializeField] public float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;

        // Private fields
        private GameObject player;
        private Fighter fighter;
        private Health health;
        private Mover mover;

        // State variables
        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int currentWaypointIndex = 0;

        // Start method
        private void Start()
        {
            // Finds the player by tag
            player = GameObject.FindGameObjectWithTag("Player");
            // Find the Fighter component
            fighter = GetComponent<Fighter>();
            // Find the Health component
            health = GetComponent<Health>();
            // Find the Mover component
            mover = GetComponent<Mover>();

            // Save initial guard location
            guardPosition = transform.position;
        }

        // Update each frame
        private void Update()
        {
            // If the enemy is dead, don't do anything
            if (health.IsDead()) { return; }
            // The enemy is alive and interacts with the player 
            InteractWithPlayerRange();
        }

        /**
         * This method allow to attack the player if it is in range
         */
        private void InteractWithPlayerRange()
        {
            // Validate if the player is in chase distance and can be attacked
            if (ValidateIsInRange() && fighter.CanAttack(player))
            {
                // Set Attack behavior
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                // Set Suspicion behavior
                Suspicionbehavior();
            }
            else
            {
                // Set Patrol behavior
                PatrolBehavior();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            // Increase time since last saw player
            timeSinceLastSawPlayer += Time.deltaTime;
            // Increase time since enemy arrived to waypoint
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        /**
         * This method indicates how the enemy attack behavior will be
         */
        private void AttackBehavior()
        {
            // Reset time since last saw player
            timeSinceLastSawPlayer = 0f;
            // Attack the player
            fighter.Attack(player);
        }

        /**
         * This method indicates how the enemy suspicion behavior will be
         */
        private void Suspicionbehavior()
        {
            // Suspicion state
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        /**
         * This method indicates how the enemy guard behavior will be
         */
        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0f;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                // Move the enemy to his guard location
                mover.StartMoveAction(nextPosition);
            }
            
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        /**
         * This method validates if the player is in the attack range
         */
        private bool ValidateIsInRange()
        {
            // Finds the distance from the enemy to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            // Returns if the player is in the weapon range to enemy
            return distanceToPlayer < chaseDistance;
        }

        /**
         * Called by Unity Editor
         */ 
        private void OnDrawGizmosSelected()
        {
            DisplayChaseRange();
        }

        /**
         * Display chase range in Unity Editor
         */ 
        private void DisplayChaseRange()
        {
            // Change the gizmo color to cyan
            Gizmos.color = Color.cyan;
            // Draw a sphere in the editor
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

}
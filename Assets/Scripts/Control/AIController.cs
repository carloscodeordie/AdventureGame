using UnityEngine;
using RPG.Core;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control {

    public class AIController : MonoBehaviour
    {
        // Editor properties
        [SerializeField] public float chaseDistance = 5f;
        [SerializeField] public float suspicionTime = 3f;

        // Private fields
        private GameObject player;
        private Fighter fighter;
        private Health health;
        private Mover mover;

        // State variables
        private Vector3 guardLocation;
        private float timeSinceLastSawPlayer = Mathf.Infinity;

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
            guardLocation = transform.position;
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
                // Reset time since last saw player
                timeSinceLastSawPlayer = 0f;
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
                GuardBehavior();
            }

            // Increase time since last saw player
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        /**
         * This method indicates how the enemy attack behavior will be
         */
        private void AttackBehavior()
        {
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
        private void GuardBehavior()
        {
            // Move the enemy to his guard location
            mover.StartMoveAction(guardLocation);
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
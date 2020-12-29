using UnityEngine;
using RPG.Combat;

namespace RPG.Control {

    public class AIController : MonoBehaviour
    {
        // Editor properties
        [SerializeField] float chaseDistance = 5f;

        // Private fields
        GameObject player;
        Fighter fighter;

        // Start method
        private void Start()
        {
            // Finds the player by tag
            player = GameObject.FindGameObjectWithTag("Player");
            // Find the Fighter component
            fighter = GetComponent<Fighter>();
        }

        // Update each frame
        private void Update()
        {
            InteractWithPlayerRange();
        }

        /**
         * Attack the player if it is in range
         */ 
        private void InteractWithPlayerRange()
        {
            // Validate if the player is in chase distance and can be attacked
            if (ValidateIsInRange() && fighter.CanAttack(player))
            {
                // Attack the player
                fighter.Attack(player);
            }
            else
            {
                // Stop the enemy movement
                fighter.Cancel();
            }
        }

        /**
         * Validate if the player is in the attack range
         */ 
        private bool ValidateIsInRange()
        {
            // Finds the distance from the enemy to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            // Returns if the player is in the weapon range to enemy
            return distanceToPlayer < chaseDistance;
        }
    }

}
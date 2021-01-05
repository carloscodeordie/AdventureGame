using UnityEngine;
using RPG.Core;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        // Private fields
        Health health;

        // Start method
        private void Start()
        {
            health = GetComponent<Health>();
        }

        // Update method
        private void Update()
        {
            // If the enemy is dead, don't do anything
            if (health.IsDead()) { return; }

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        /*
         * This method is used to get a ray from the camara to mouse position
         */
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        /**
         * This method allow the player to interact with the combat
         */
        private bool InteractWithCombat()
        {
            // throws a ray that cross everything in the mouse position
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            
            // Iterates all the hits
            foreach (RaycastHit hit in hits)
            {
                // Verify if the ray hits an enemy
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null || !GetComponent<Fighter>().CanAttack(target.gameObject)) { continue; }
                
                // Attack if the target is an enemy
                if (Input.GetMouseButton(0))
                {
                    // Attack the enemy
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        /**
         * This method method allows to interact with the character movement
         */
        private bool InteractWithMovement()
        {
            // Variable that contains the click position
            RaycastHit hit;

            // Triggers a ray in mouse position
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            // Validate if the ray hit some valid terrain
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    // Move player to new destination
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }
    }
}
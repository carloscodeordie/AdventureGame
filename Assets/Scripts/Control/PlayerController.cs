using System;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;


namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        // Update method
        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            // throws a ray that cross everything in the mouse position
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            
            // Iterates all the hits
            foreach (RaycastHit hit in hits)
            {
                // Verify if the ray hits an enemy
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                // Validates if the target is an enemy
                if (target == null) {
                    continue;
                }
                // Attack if the target is an enemy
                if (Input.GetMouseButtonDown(0))
                {
                    // Attack the enemy
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        // Method that allows to iteract with the character movement
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
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        // Method used to get a ray from the camara to mouse position
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
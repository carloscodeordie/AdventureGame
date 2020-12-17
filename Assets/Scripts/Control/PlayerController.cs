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
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            // throws a ray that cross everything in the mouse position
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            
            // Iterates all the hits
            foreach (RaycastHit hit in hits)
            {
                // Verify if the ray hits an enemy
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                // Validates if the target is an enemy
                if (target == null) continue;
                // Attack if the target is an enemy
                if (Input.GetMouseButtonDown(0))
                {
                    // Attack the enemy
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }

        // Method that allows to iteract with the character movement
        private void InteractWithMovement()
        {
            // Verify if the user click on mouse left button
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        // Move character where the player clicks
        private void MoveToCursor()
        {
            // Variable that contains the click position
            RaycastHit hit;

            // Triggers a ray in mouse position
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            // Validate if the ray hit some valid terrain
            if (hasHit)
            {
                // Move player to new destination
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

        // Method used to get a ray from the camara to mouse position
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
using UnityEngine;

using RPG.Control;

namespace RPG.Development
{
    public class EnemyRange : MonoBehaviour
    {
        // Private fields
        AIController enemyController;

        // Start method
        private void Start()
        {
            enemyController = GetComponent<AIController>();
        }

        /**
         * Display the chasing range when the enemy is selected in the editor
         */
        private void OnDrawGizmosSelected()
        {
            DisplayChaseRange();
        }

        private void DisplayChaseRange()
        {
            // Verify there is an enemy
            if (enemyController != null)
            {
                // Change the gizmo color to cyan
                Gizmos.color = Color.cyan;
                // Draw a sphere in the editor
                Gizmos.DrawWireSphere(transform.position, enemyController.chaseDistance);
            }
        }
    }
}
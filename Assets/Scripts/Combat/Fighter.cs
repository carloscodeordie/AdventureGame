using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // Distance from enemy when the player is attacking him
        [SerializeField] float weaponRange = 2f;

        // Private fields
        private Mover mover;

        // Enemy variable
        Transform target;

        // Start method
        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        // Update method
        private void Update()
        {
            if (target == null) return;

            InteractWithEnemyRange();
        }

        /**
         * Move the player close to enemy in order to attack him 
         */
        private void InteractWithEnemyRange()
        {
            // Verify if there is an enemy to attack and is in range
            if (validateIsInRange())
            {
                // Stop the movement
                mover.Cancel();

                // Trigger attack animation
                AttackBehaviour();
            }
            // The player is not in range to attack
            else
            {
                // Move towards the enemy
                mover.MoveTo(target.position);
            }
        }

        /**
         * Set Attack Animation
         */ 
        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }

        /**
         * Validates if the player is in weapon range for enemy
         */ 
        private bool validateIsInRange()
        {
            // Finds if the player is in the weapon range to enemy
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        /*
         * Set the target enemy to attack
         */ 
        public void Attack(CombatTarget combatTarget)
        {
            // Tells the action scheduler to make an attack
            GetComponent<ActionScheduler>().StartAction(this);
            // Set the new target enemy to attack him
            target = combatTarget.transform;
        }

        /*
         * Cancel the attack
         */
        void IAction.Cancel()
        {
            // Reset the target
            target = null;
        }

        /**
         * Animation event that is triggered when animation makes the hit 
         */
        public void Hit()
        {
            print("Makes a hit...");
        }
    }
}
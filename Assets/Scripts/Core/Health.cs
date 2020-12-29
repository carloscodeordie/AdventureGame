using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        // Private variables
        [SerializeField] float healthPoints = 100f;

        // State variables
        private bool isDead = false;

        // Getters for isDead state variable
        public bool IsDead()
        {
            return isDead;
        }

        /**
         * This method allows the player or enemy take damage 
         */
        public void TakeDamage(float damage)
        {
            // Reduce health
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            ValidatesHealth();
        }

        /**
         * This method validates if the player or enemy is death
         */
        private void ValidatesHealth()
        {
            // check if player or enemy has enough health points
            if (healthPoints == 0)
            {
                Die();
            }
        }

        /**
         * This method determines the player or enemy behavior when it dies
         */
        private void Die()
        {
            // Don't do anything if the player or enemy is already dead
            if (isDead) return;
            // Set state to indicate that is dead
            isDead = true;
            // triggers Death animation event
            GetComponent<Animator>().SetTrigger("Death");
            // Cancel current action
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        // Private variables
        [SerializeField] float healthPoints = 100f;

        // State variables
        private bool isDead = false;

        // Getters
        public bool IsDead()
        {
            return isDead;
        }

        /**
         * Player or enemy takes damage 
         */ 
        public void TakeDamage(float damage)
        {
            // Reduce health
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            ValidatesHealth();
        }

        /**
         * Validates if the player or enemy is death
         */ 
        private void ValidatesHealth()
        {
            // check if player or enemy has enough health points
            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // Don't do anything if the player or enemy is already dead
            if (isDead) return;
            // Set state to indicate that is dead
            isDead = true;
            // triggers Death animation event
            GetComponent<Animator>().SetTrigger("Death");
        }
    }
}
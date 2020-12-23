﻿using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // Distance from enemy when the player is attacking him
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 20f;

        // Private fields
        private Mover mover;

        // Enemy variable
        Health target;
        float timeSinceLastAttack = 0f;

        // Start method
        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        // Update method
        private void Update()
        {
            // Increase time since last attack
            timeSinceLastAttack += Time.deltaTime;

            // Returns if no target is selected
            if (target == null) return;

            // Don't do anything is the target is dead
            if (target.IsDead()) return;

            // Move player to enemy
            InteractWithEnemyRange();
        }

        /**
         * Move the player close to enemy in order to attack it 
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
                mover.MoveTo(target.transform.position);
            }
        }

        /**
         * Set Attack Animation
         */ 
        private void AttackBehaviour()
        {
            // Look at the enemy before hitting him
            transform.LookAt(target.transform);

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                // Triggers animation
                TriggerAttack();
                // Reset time to attack 
                timeSinceLastAttack = 0f;
            }
        }

        /**
         * Triggers events in attack animation
         */ 
        private void TriggerAttack()
        {
            // reset trigger stop attack animation event
            GetComponent<Animator>().ResetTrigger("StopAttack");
            // triggers attack animation event
            GetComponent<Animator>().SetTrigger("Attack");
            // Reset the time since last attack
        }

        /**
         * Animation event that is triggered when animation makes the hit 
         */
        public void Hit()
        {
            // Don't hit if there is no target of the target moves
            if (target == null) return;
            
            // The enemy takes damage
            target.TakeDamage(weaponDamage);
        }

        /**
         * Validates if the player is in weapon range for enemy
         */
        private bool validateIsInRange()
        {
            // Finds if the player is in the weapon range to enemy
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        /**
         * Validate if the player can attack an enemy
         */ 
        public bool CanAttack(CombatTarget combatTarget)
        {
            // Validate if there is an enemy
            if (combatTarget == null) { return false; }
            
            // Get Target Health
            Health targetToTest = combatTarget.GetComponent<Health>();
            // Validate if there is a valid enemy and if is not dead
            return targetToTest != null && !targetToTest.IsDead();
        }

        /*
         * Set the target enemy to attack
         */ 
        public void Attack(CombatTarget combatTarget)
        {
            // Tells the action scheduler to make an attack
            GetComponent<ActionScheduler>().StartAction(this);
            // Set the new target enemy to attack him
            target = combatTarget.GetComponent<Health>();
        }

        /*
         * Cancel the attack
         */
        void IAction.Cancel()
        {
            // Reset the target
            target = null;
            // Reset attack animations 
            StopAttack();
        }

        /**
         * Reset attack animations
         */
        private void StopAttack()
        {
            // Cancel Attack Animation
            GetComponent<Animator>().ResetTrigger("Attack");
            // Cancel Attack Animation
            GetComponent<Animator>().SetTrigger("StopAttack");
        }
    }
}
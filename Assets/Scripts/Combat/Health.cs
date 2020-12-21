using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        // Private variables
        [SerializeField] float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            ValidatesHealth();
        }

        private void ValidatesHealth()
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
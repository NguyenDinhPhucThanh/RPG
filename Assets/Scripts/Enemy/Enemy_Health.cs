using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int expReward = 3;

    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;
    public int currentHealth;
    public int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            // Call the loot drop function before destroying the enemy
            PickUpEnemy lootDropper = GetComponent<PickUpEnemy>();
            if (lootDropper != null)
            {
                lootDropper.DropItems();
            }

            // Give EXP to the player
            OnMonsterDefeated?.Invoke(expReward);

            // Destroy the enemy after dropping items
            Destroy(gameObject);
        }
    }
}

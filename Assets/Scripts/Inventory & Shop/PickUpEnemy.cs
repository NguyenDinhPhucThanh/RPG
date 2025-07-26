using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEnemy : MonoBehaviour
{
    [SerializeField] private GameObject lootPrefab;  // Prefab for loot
    [SerializeField] private ItemSO goldItemSO;  // Gold item data

    public void DropItems()
    {
        // Always drop gold (random amount 1-3)
        int randomAmountOfGold = Random.Range(1, 4);

        for (int i = 0; i < randomAmountOfGold; i++)
        {
            GameObject goldDrop = Instantiate(lootPrefab, transform.position, Quaternion.identity);
            Loot loot = goldDrop.GetComponent<Loot>();

            if (loot != null)
            {
                loot.Initialize(goldItemSO, 1); // Drop 1 gold per spawned item
            }
        }
    }
}

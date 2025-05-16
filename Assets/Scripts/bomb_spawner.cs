using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_spawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float spawnRadius = 5f;
    public int numberToSpawn = 10;
    public float raycastHeight = 5f;      // How high above the spawn area the ray starts
    public float spawnOffset = 0.2f;      // How much above the platform to place the bomb
    public LayerMask spawnableLayers;     // Set this to include ground/platform layers

    void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        int maxAttempts = 20;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // Get random horizontal position within radius
            Vector2 randomHorizontal = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            // Start the raycast from above
            Vector2 rayStart = new Vector2(randomHorizontal.x, transform.position.y + raycastHeight);

            // Cast a ray downward to detect platform
            RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, raycastHeight * 2f, spawnableLayers);

            if (hit.collider != null)
            {
                Vector2 spawnPosition = hit.point + Vector2.up * spawnOffset;

                Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
                return;
            }
        }

        Debug.LogWarning("Bomb spawner: Could not find a valid surface to spawn after max attempts.");
    }
}


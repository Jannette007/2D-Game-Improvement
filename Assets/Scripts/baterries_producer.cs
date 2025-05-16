using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteries_producer : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject particles;
    public float spawnRadius = 5f;
    public int numberToSpawn = 10;
    public float raycastHeight = 5f;
    public float spawnOffset = 0.2f; // how high above the platform to spawn
    public LayerMask spawnableLayers;

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
            // Pick a random point in a circle
            Vector2 randomHorizontal = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            // Start a raycast from above
            Vector2 rayStart = new Vector2(randomHorizontal.x, transform.position.y + raycastHeight);

            RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, raycastHeight * 2f, spawnableLayers);

            if (hit.collider != null)
            {
                Vector2 spawnPosition = hit.point + Vector2.up * spawnOffset;

                Instantiate(particles, spawnPosition, Quaternion.identity);
                Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
                return;
            }
        }

        Debug.LogWarning("Could not find a valid surface to spawn battery after max attempts.");
    }
}


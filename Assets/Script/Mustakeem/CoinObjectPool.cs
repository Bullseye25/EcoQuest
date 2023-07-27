using UnityEngine;
using System.Collections.Generic;

public class CoinObjectPool : MonoBehaviour
{
    public GameObject coinPrefab; // The prefab of the collectible coin.
    public int poolSize = 20; // The total number of coin instances in the pool.
    public int activeCoinsLimit = 10; // The maximum number of coins that can be active at a time.
    public int coinsToCollectForRespawn = 5; // The number of coins that need to be collected to trigger respawn.

    private List<GameObject> coinPool;
    private int coinsCollected = 0; // Number of coins collected since last respawn.
    private int lastSpawnPosition = 0; // The index of the last spawned coin.

    private void Start()
    {
        // Create the object pool and instantiate coin instances.
        coinPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab);
            coin.SetActive(false);
            coinPool.Add(coin);
        }

        // Initially, activate the first 'activeCoinsLimit' coins.
        for (int i = 0; i < activeCoinsLimit; i++)
        {
            coinPool[i].SetActive(true);
        }
    }

    public GameObject GetPooledCoin()
    {
        // Find an inactive coin in the pool and return it.
        for (int i = 0; i < coinPool.Count; i++)
        {
            if (!coinPool[i].activeInHierarchy)
            {
                return coinPool[i];
            }
        }

        // If no inactive coins are available, create a new one and add it to the pool.
        GameObject newCoin = Instantiate(coinPrefab);
        newCoin.SetActive(false);
        coinPool.Add(newCoin);
        return newCoin;
    }

    public void ActivateCoin(Vector3 position)
    {
        // Get an inactive coin from the pool and activate it at the specified position.
        GameObject coin = GetPooledCoin();
        coin.transform.position = position;
        coin.SetActive(true);

        // Increase the count of coins collected since last respawn.
        coinsCollected++;

        // If the number of collected coins reaches the threshold, respawn new coins.
        if (coinsCollected >= coinsToCollectForRespawn)
        {
            RespawnCoins();
            coinsCollected = 0; // Reset the collected count after respawn.
        }
    }

    private void RespawnCoins()
    {
        // Deactivate the last 'activeCoinsLimit' coins.
        for (int i = lastSpawnPosition; i < lastSpawnPosition + activeCoinsLimit; i++)
        {
            coinPool[i].SetActive(false);
        }

        // Move the lastSpawnPosition to the next set of coins in the pool.
        lastSpawnPosition += activeCoinsLimit;

        // Activate the next set of coins.
        for (int i = lastSpawnPosition; i < lastSpawnPosition + activeCoinsLimit; i++)
        {
            // Wrap around the pool if necessary.
            int index = i % coinPool.Count;
            coinPool[index].transform.position = Vector3.zero; // Set the initial position of new coins.
            coinPool[index].SetActive(true);
        }
    }

    public void DeactivateCoin(GameObject coin)
    {
        // Deactivate the coin and return it to the pool for reuse.
        coin.SetActive(false);
    }
}

using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public CoinObjectPool coinPool;
    public Transform coinSpawnPointsParent; // Parent object containing the spawn positions.

    private Transform[] coinSpawnPoints;

    private void Start()
    {
        // Set up the coinSpawnPoints array with the positions of the child objects.
        GetCoinSpawnPoints();

        // Assuming you want to spawn coins at the start of the game.
        SpawnInitialCoins();
    }

    private void GetCoinSpawnPoints()
    {
        // Get all the child transforms of the parent object as spawn points.
        coinSpawnPoints = new Transform[coinSpawnPointsParent.childCount];
        for (int i = 0; i < coinSpawnPointsParent.childCount; i++)
        {
            coinSpawnPoints[i] = coinSpawnPointsParent.GetChild(i);
        }
    }

    private void SpawnInitialCoins()
    {
        for (int i = 0; i < coinSpawnPoints.Length; i++)
        {
            coinPool.ActivateCoin(coinSpawnPoints[i].position);
        }
    }

    // Example method to be called when a player collects a coin.
    public void OnCoinCollected(GameObject coin)
    {
        // Assuming you have a reference to the CoinObjectPool script on the same object.
        coinPool.DeactivateCoin(coin);
    }
}

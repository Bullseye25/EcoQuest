using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CollectibleType
{
    Coins,
    Gems,
    AirCylinder,
    Antidote,
    Health
}

public abstract class CollectibleSystemBase : MonoBehaviour
{
    protected Dictionary<CollectibleType, int> collectibleCounts;
    public abstract void UpdateCollectibles(CollectibleType type);
}

public class CollectibleSystem : CollectibleSystemBase
{
    public List<GameObject> collectibles;
    public Text coinText;
    public Text gemText;
    public Text AirCylinderText;
    public Text healthText;
    public Text AntidoteText;
    public static bool cylinder;

    private PlayerMovement timerController;
    public healthManage health;


    private void Start()
    {
        // Initialize the dictionary of collectible counts with all types set to 0
        collectibleCounts = new Dictionary<CollectibleType, int>();
        foreach (CollectibleType type in System.Enum.GetValues(typeof(CollectibleType)))
        {
            collectibleCounts[type] = 0;
        }

        timerController = GetComponent<PlayerMovement>();

    }

    public override void UpdateCollectibles(CollectibleType type)
    {
        switch (type)
        {
            case CollectibleType.Coins:
                coinText.text = "Coins: " + collectibleCounts[type];
                Debug.Log("coin");
                break;
            case CollectibleType.Gems:
                gemText.text = "Gems: " + collectibleCounts[type];
                Debug.Log("Gems");
                break;
            case CollectibleType.AirCylinder:
                AirCylinderText.text = "AirCylinder: " + collectibleCounts[type];
                timerController.FillSlider();
                Debug.Log("AirCylinder");

                break;
            case CollectibleType.Health:
              //  healthText.text = "Health: " + collectibleCounts[type];
                health.GainSmallHealth();
                Debug.Log("Health");
                break;
            case CollectibleType.Antidote:
                AntidoteText.text = "Antidotes: " + collectibleCounts[type];
                Debug.Log("Antidotes");
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered : " + other.gameObject.name);
        if (collectibles.Contains(other.gameObject))
        {
            Debug.Log("Triggered by: " + other.gameObject.name);
            CollectibleType type = other.gameObject.GetComponent<Collectible>().type;
            if (collectibleCounts.ContainsKey(type))
            {
                collectibleCounts[type]++;
                UpdateCollectibles(type);
                Destroy(other.gameObject);
                cylinder = false;
            }
        }
    }
}

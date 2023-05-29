using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CollectibleSystem : CollectibleSystemBase
{
    [SerializeField] private List<GameObject> collectibles;
     [SerializeField] private TextMeshProUGUI coinText;
     [SerializeField] private TextMeshProUGUI gemText;
     [SerializeField] private TextMeshProUGUI AirCylinderText;
     [SerializeField] private TextMeshProUGUI SmallFish;
     [SerializeField] private TextMeshProUGUI AntidoteText;
     [SerializeField] public static bool cylinder;

    private PlayerMovement timerController;
     [SerializeField] private HealthManage health;
     private Levels levels;


    private void Start()
    {
        // Initialize the dictionary of collectible counts with all types set to 0
        collectibleCounts = new Dictionary<CollectibleType, int>();
        levels = FindObjectOfType<Levels>();
         
        foreach (CollectibleType type in System.Enum.GetValues(typeof(CollectibleType)))
        {
            collectibleCounts[type] = 0;
        }
        
        timerController = GetComponent<PlayerMovement>();

    }

     private void Update() 
    {
        SmallFish.text = "Fish : " + AntidotHelp.score.ToString();
        if(AntidotHelp.score == 5){
            levels.NextStage();
            AntidotHelp.score +=10;
        }
    }
    // private void Update() {
        
    // }

   
    public override void UpdateCollectibles(CollectibleType type)
    {
        switch (type)
        {
            case CollectibleType.Coins:
                coinText.text = "Coins: " + collectibleCounts[type];
                break;
            case CollectibleType.Gems:
                gemText.text = "Gems: " + collectibleCounts[type];;
                break;
            case CollectibleType.AirCylinder:
                AirCylinderText.text = "AirCylinder: " + collectibleCounts[type];
                timerController.FillSlider();
                break;
            case CollectibleType.Health:
                health.GainSmallHealth();
                break;
            case CollectibleType.Antidote:
                AntidoteText.text = "Antidotes: " + collectibleCounts[type];    
                break;
            
        }
    }
    public int AntidoteCount
    {
        get { return collectibleCounts[CollectibleType.Antidote]; }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (collectibles.Contains(other.gameObject))
        {
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

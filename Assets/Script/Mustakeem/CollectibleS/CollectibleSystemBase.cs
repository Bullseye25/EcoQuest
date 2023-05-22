using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CollectibleSystemBase : MonoBehaviour
{
    protected Dictionary<CollectibleType, int> collectibleCounts;
    public abstract void UpdateCollectibles(CollectibleType type);
}
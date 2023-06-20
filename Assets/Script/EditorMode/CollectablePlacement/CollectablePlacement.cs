using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePlacement : MonoBehaviour
{
    [SerializeField] private List<Transform> children = new List<Transform>();

    [Space]
    [SerializeField] private GameObject prefab;

    [SerializeField] private List<Transform> newCollectables = new List<Transform>();

    [ContextMenu("GetCollectables")]
    public void GetCollectables()
    {
        foreach(Transform child in transform)
        {
            children.Add(child);
        }
    }

    [ContextMenu("PlaceCollectables")]
    public void PlaceCollectables()
    {
        foreach (Transform child in children)
        {
            var collectable = Instantiate(prefab);
            collectable.transform.SetParent(this.transform);
            collectable.transform.localPosition = child.localPosition;
            newCollectables.Add(collectable.transform);
        }
    }

    [ContextMenu("DeletePlacedCollectables")]
    public void DeletePlacedCollectables()
    {
        foreach (Transform child in newCollectables)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    [ContextMenu("DeleteOrigin")]
    public void DeleteOrigin()
    {
        foreach (Transform child in children)
        {
            DestroyImmediate(child.gameObject);
        }
    }

}

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashPrefab : MonoBehaviour
{
    public delegate void TrashCollectedHandler();
    public static event TrashCollectedHandler OnTrashCollected;

    public void Collect()
    {
        gameObject.SetActive(false);
        OnTrashCollected?.Invoke();
    }
}

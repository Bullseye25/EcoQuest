using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TrashBotCollected : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private int currentAmount = 0;
    [SerializeField] private TextMeshProUGUI info;

    [Space]
    [SerializeField] private UnityEvent TrashCollected = new UnityEvent();

    private void Start()
    {
        currentAmount = 0;
    }

    public void Proceed()
    {
        if (currentAmount == amount)
        {
            TrashCollected.Invoke();
        }
    }

    public void CollectedItem()
    {
        currentAmount++;

        if (info != null)
            info.text = $"{currentAmount}";

        Proceed();
        PlayerPrefs.SetInt("TrashBot", currentAmount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class AreItemsCollected : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private int currentAmount = 0;
    [SerializeField] private TextMeshProUGUI info;

    [Space]
    [SerializeField] private UnityEvent collectedAll = new UnityEvent();

    private void Start()
    {
        currentAmount = 0;
    }

    public void Proceed()
    {
        if (currentAmount == amount)
        {
            collectedAll.Invoke();
        }
    }

    public void CollectedItem()
    {
        currentAmount++;

        if (info != null)
            info.text = $"{currentAmount}/{amount}";

        Proceed();
    }
}

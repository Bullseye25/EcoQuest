using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private const float MAX = 100f;

    [SerializeField] private RectTransform rect;
    
    [Space]
    [SerializeField] private float bigGain, smallGain, bigDamage, smallDamange;

    private float currentHealth;

    private void Start()
    {
        currentHealth = MAX;
    }

    private void UpdateHealth(float healthChange)
    {
        float transformedChange = healthChange * (MAX / 398.6306f);
        currentHealth += transformedChange;

        // keep health within bounds of 0 to 100
        if (currentHealth > MAX)
        {
            rect.anchoredPosition = new Vector2(0f, rect.anchoredPosition.y);
            currentHealth = MAX;
        }
        else if (currentHealth < 0.0f)
        {
            rect.anchoredPosition = new Vector2(398.6306f, rect.anchoredPosition.y);
            currentHealth = 0.0f;
        }
        else
        {
            rect.anchoredPosition = new Vector2(healthChange + rect.anchoredPosition.x, rect.anchoredPosition.y);
        }
    }

    [ContextMenu("ResetHealth")]
    public virtual void ResetHealth()
    {
        UpdateHealth(MAX);
    }

    [ContextMenu("GainBigHealth")]
    public virtual void GainBigHealth()
    {
        UpdateHealth(bigGain);
    }

    [ContextMenu("GainSamllHealth")]
    public virtual void GainSamllHealth()
    {
        UpdateHealth(smallGain);
    }

    [ContextMenu("BigDamage")]
    public virtual void BigDamage()
    {
        UpdateHealth(-bigDamage);
    }

    [ContextMenu("SmallDamage")]
    public virtual void SmallDamage()
    {
        UpdateHealth(-smallDamange);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float currentHealth;

    public void UpdateHealth(float healthChange)
    {
        float transformedChange = healthChange * (100.0f / 205.0f); // map from -102.5 to 102.5 to 0 to 100
        currentHealth += transformedChange;

        // keep health within bounds of 0 to 100
        if (currentHealth > 100.0f)
        {
            rect.anchoredPosition = new Vector2(102.5f, rect.anchoredPosition.y);
            currentHealth = 100.0f;
        }
        else if (currentHealth < 0.0f)
        {
            rect.anchoredPosition = new Vector2(-102.5f, rect.anchoredPosition.y);
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
        UpdateHealth(100);
    }

    [ContextMenu("GainBigHealth")]
    public virtual void GainBigHealth()
    {
        UpdateHealth(45);
    }

    [ContextMenu("GainSamllHealth")]
    public virtual void GainSamllHealth()
    {
        UpdateHealth(25);
    }

    [ContextMenu("BigDamage")]
    public virtual void BigDamage()
    {
        UpdateHealth(-35);
    }

    [ContextMenu("SmallDamage")]
    public virtual void SmallDamage()
    {
        UpdateHealth(-15);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}

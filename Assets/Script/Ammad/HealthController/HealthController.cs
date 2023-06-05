
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    public GameObject player;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            player.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthBar();
    }

    public void Heal(float healingAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healingAmount);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    [ContextMenu("ResetHealth")]
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    [ContextMenu("GainBigHealth")]
    public void GainBigHealth()
    {
        Heal(45);
    }

    [ContextMenu("GainSmallHealth")]
    public void GainSmallHealth()
    {
        Heal(25);
    }

    [ContextMenu("BigDamage")]
    public void BigDamage()
    {
        Debug.Log("bigDamage");
        TakeDamage(40);
    }

    [ContextMenu("SmallDamage")]
    public void SmallDamage()
    {
        TakeDamage(10);
    }

}

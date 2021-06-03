using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    //private BodyLevel bodyLevel;
    public float maxHealth = 10;
    private float currentHealth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.InitializeHealthBar(maxHealth);
    }
    public void RemoveLifePoints(float lifePointsRemoved)
    {
        if (currentHealth > 0.0f)
        {
            currentHealth -= lifePointsRemoved;
            if (healthBar != null)
                healthBar.SetHealth(currentHealth);
        }
    }
    public bool IsDead()
    {
        return currentHealth <= 0.0f;
    }
}

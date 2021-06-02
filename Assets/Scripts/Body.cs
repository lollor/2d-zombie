using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    //private BodyLevel bodyLevel;
    public float MaxHealth;
    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    /*void Update()
    {

    }*/
    public void RemoveLifePoints(float lifePointsRemoved)
    {
        if (currentHealth > 0.0f)
            currentHealth -= lifePointsRemoved;
    }
    public bool IsDead()
    {
        return currentHealth <= 0.0f;
    }
}

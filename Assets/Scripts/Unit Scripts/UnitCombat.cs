using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    [SerializeField]
    protected float damage, maxHealth;
    protected float currentHealth;

    public float CurrentHealth { get { return currentHealth; } }
    public float Damage { get { return damage; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset() {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage) {
        if(damage > 0f) {
            currentHealth -= damage;
        }
    }
}

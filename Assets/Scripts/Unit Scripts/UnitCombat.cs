using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth, currentHealth;

    public float CurrentHealth { get { return currentHealth; } }

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

    protected bool CanAct() {
        return GameManager.instance.CurrentGameState == GameState.Game;
    }
}

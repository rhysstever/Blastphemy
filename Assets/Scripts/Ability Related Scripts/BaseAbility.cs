using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    protected AbilityType abilityType;
    protected string abilityName, abilityDescription, flavorText;
    protected int level;
    protected float damage, cooldown, currentCooldownTimer;

    public AbilityType AbilityType { get { return abilityType; } }
    public string AbilityName { get { return abilityName; } }
    public string AbilityDescription { get { return abilityDescription; } }
    public string FlavorText { get { return flavorText; } }
    public int Level { get { return level; } }
    public float Damage { get { return damage; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        level = 0;
        currentCooldownTimer = 0f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(CanCast()) {
            currentCooldownTimer += Time.deltaTime;

            if(currentCooldownTimer >= cooldown) {
                Cast();
            }
        }
    }

    public virtual bool CanCast() {
        return GameManager.instance.CurrentMenuState == MenuState.Game
            && level > 0;
    }

    public virtual void Cast() {
        currentCooldownTimer = 0f;
    }

    public virtual void Upgrade() {
        level++;
    }
}

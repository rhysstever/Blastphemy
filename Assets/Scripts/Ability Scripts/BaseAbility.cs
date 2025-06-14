using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    protected string abilityName, abilityDescription, flavorText;
    [SerializeField]
    protected int level;
    [SerializeField]
    protected float damage, cooldown;
    [SerializeField]
    protected float currentCooldownTimer;

    public string AbilityName { get { return abilityName; } }
    public string AbilityDescription { get { return abilityDescription; } }
    public string FlavorText { get { return flavorText; } }
    public int Level { get { return level; } }
    public float Damage { get { return damage; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = 0;
        currentCooldownTimer = 0f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(GameManager.instance.CurrentMenuState == MenuState.Game) {
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

    public void Upgrade() {
        level++;
    }
}

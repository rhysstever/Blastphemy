using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    protected string abilityName, abilityDescription, flavorText;
    [SerializeField]
    protected int level;
    [SerializeField]
    protected float range, damage, cooldown;
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
        currentCooldownTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.CurrentGameState == GameState.Game) {
            currentCooldownTimer += Time.deltaTime;

            if(currentCooldownTimer >= cooldown) {
                Cast();
            }
        }
    }

    public virtual bool CanCast() {
        return GameManager.instance.CurrentGameState == GameState.Game
            && level > 0;
    }

    public virtual void Cast() {
        currentCooldownTimer = 0f;
    }

    public void Upgrade() {
        level++;
    }
}

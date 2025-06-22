using UnityEngine;

public class AbilityProjectile : MonoBehaviour
{
    protected float damage, lifeSpan, currentLifeSpanTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        currentLifeSpanTimer = 0f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(GameManager.instance.CurrentMenuState == MenuState.Game) {
            currentLifeSpanTimer += Time.deltaTime;

            if(currentLifeSpanTimer >= lifeSpan) {
                Destroy(gameObject);
            }
        }
    }
}

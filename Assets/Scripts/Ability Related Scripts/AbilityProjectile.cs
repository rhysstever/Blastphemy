using UnityEngine;

public class AbilityProjectile : MonoBehaviour
{
    protected float damage, lifeSpan, currentLifeSpan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        currentLifeSpan = 0f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(GameManager.instance.CurrentMenuState == MenuState.Game) {
            currentLifeSpan += Time.deltaTime;

            if(currentLifeSpan >= lifeSpan) {
                Destroy(gameObject);
            }
        }
    }
}

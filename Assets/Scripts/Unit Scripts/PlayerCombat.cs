using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : UnitCombat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Deal damage to the player
    /// </summary>
    /// <param name="damage">The amount of damage the player should take</param>
    public override void TakeDamage(float damage) {
        if(damage >= 0) {
            base.TakeDamage(damage);

            if(currentHealth <= 0f) {
                GameManager.instance.ChangeMenuState(MenuState.End);
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ApocalypseAbility : BaseAbility
{
    private float range;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        abilityType = AbilityType.Apocalypse;
        abilityName = "Apocalypse";
        abilityDescription = "Strike all nearby enemies with lightning";
        flavorText = "QUOTE NEEDED";

        damage = 2f;
        cooldown = 20f;

        range = 20f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Cast() {
        if(CanCast()) {
            base.Cast();

            Transform[] enemyTrans = EnemyManager.instance.EnemyParent.GetComponentsInChildren<Transform>();

            List<Transform> enemyTransList = enemyTrans.OfType<Transform>().ToList();
            List<Transform> enemyTransInRange = enemyTransList.Where(t => isWithinRange(t.position)).ToList();
            foreach(Transform enemyTranInRange in enemyTransInRange) {
                if(enemyTranInRange.gameObject != null) {
                    enemyTranInRange.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
                }
            }
        }
    }

    private bool isWithinRange(Vector2 position) {
        return Vector2.Distance(transform.position, position) <= range;
    }
}

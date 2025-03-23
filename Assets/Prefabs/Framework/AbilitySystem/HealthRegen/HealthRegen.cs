using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthRegen", menuName = "Ability/HealthRegen")]
public class HealthRegen : Ability
{
    [Header("HealthRegen")]
    [SerializeField] float healthRegenAmt;
    [SerializeField] float healthRegenDuration;
    public override void ActivateAbility()
    {
        if (!CommitAbility()) return;
        HealthComponent HealthComp = AbilityComp.GetComponent<HealthComponent>();
        if (HealthComp != null)
        {
            if(healthRegenDuration == 0)
            {
                HealthComp.changeHealth(healthRegenAmt, AbilityComp.gameObject);
                return;
            }
            AbilityComp.StartCoroutine(StartHealthRegen(healthRegenAmt, healthRegenDuration, HealthComp));
        }
    }
    
    IEnumerator StartHealthRegen(float amt, float duration, HealthComponent healComp)
    {
        float counter = duration;
        float regenRate = amt / duration;
        while (counter > 0)
        {
            counter -= Time.deltaTime;

            healComp.changeHealth(regenRate * Time.deltaTime, AbilityComp.gameObject); 
            yield return null;
        }
    }
}
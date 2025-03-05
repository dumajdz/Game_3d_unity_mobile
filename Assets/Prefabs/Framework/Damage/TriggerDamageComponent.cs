using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerDamageComponent : DamageComponent
{
    [SerializeField] float damage;
    [SerializeField] BoxCollider trigger;
    [SerializeField] bool startedEnabled = false;

    public void SetDamageEnabled(bool enabled)
    {
        if (trigger != null)
        {
            trigger.enabled = enabled;
        }
        else
        {
            //Debug.LogWarning($"Trigger in {gameObject.name} is null!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetDamageEnabled(startedEnabled);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!ShouldDamage(other.gameObject))
           // Debug.Log($"{gameObject.name}: Không gây sát thương lên {other.gameObject.name}");
        return;

        HealthComponent healthComp = other.GetComponent<HealthComponent>();
        if (healthComp != null)
        {
            healthComp.changeHealth(-damage, gameObject);
            //Debug.Log($"{gameObject.name}: Gây {damage} sát thương lên {other.gameObject.name}, máu còn lại: {healthComp.CurrentHealth}");
        }
        else
        {
            //Debug.Log($"{gameObject.name}: Không tìm thấy HealthComponent trên {other.gameObject.name}");
        }
    }
}

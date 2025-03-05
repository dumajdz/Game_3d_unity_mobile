using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IRewardListener
{
    public delegate void OnHealthChange(float health, float delta , float maxHealth);
    public delegate void OnTakeDamege(float health, float delta, float maxHealth,GameObject Instigator);
    public delegate void OnHealthEmpty(GameObject Killer);

    [SerializeField] private float health = 100;
    public float CurrentHealth => health;
    [SerializeField] float maxhealth = 100;

    public event OnHealthChange onHealthChange;
    public event OnTakeDamege onTakeDamage;
    public event OnHealthEmpty onHealthEmpty;

    public void BroadcastHealthValueImmeidately()
    {
        onHealthChange?.Invoke(health, 0, maxhealth);
    }

    public void changeHealth(float amt, GameObject Instigator)
    {
        Debug.Log($"🩸 {gameObject.name} nhận sát thương: {amt}, máu trước: {health}");

        if (amt == 0 || health == 0)
        {
            Debug.Log(" Không thay đổi vì amt = 0 hoặc đã chết.");
            return;
        }

        health += amt;
        Debug.Log($"🩸 Sau khi nhận damage: {health}");

        if (amt < 0)
        {
            onTakeDamage?.Invoke(health, amt, maxhealth, Instigator);
        }

        onHealthChange?.Invoke(health, amt, maxhealth);

        if (health <= 0)
        {
            health = 0;
            Debug.Log("💀 Chết!");
            onHealthEmpty?.Invoke(Instigator);
        }
    }

    public void Reward(Reward reward)
    {
        health = Mathf.Clamp(health + reward.healthReward, 0, maxhealth);
        onHealthChange?.Invoke(health, reward.healthReward, maxhealth);
    }
}

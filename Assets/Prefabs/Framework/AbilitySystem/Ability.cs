using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] Sprite AbilityIcon;
    [SerializeField] float stamilaCost = 10f;
    [SerializeField] float cooldownDuration = 2f;
    AbilityComponent abilityComponent;

    [Header("Audio")]
    [SerializeField] AudioClip abilityAudio;
    [SerializeField] float volume = 1f;

    public AbilityComponent AbilityComp
    {
        get { return abilityComponent; }
        private set { abilityComponent = value; }
    }
    AbilityComponent AbilityComponent;

    bool abilityOnCooldown = false;

    public delegate void OnCooldownStarted();
    public OnCooldownStarted onCooldownStarted;
    internal void InitAbility(AbilityComponent abilityComponent)
    {
        this.abilityComponent = abilityComponent;
    }
    public abstract void ActivateAbility();

    //check all the condition needed to activate the ability
    // experted to be called in the child class
    protected bool CommitAbility()
    {
        if(abilityOnCooldown) return false;// Nếu đang hồi chiêu, không dùng được

        if (abilityComponent == null || !abilityComponent.TryConsumeStamina(stamilaCost))
            GameplayStatics.PlayAudioAtPlayer(abilityAudio,volume);

        return true;// Nếu đủ điều kiện, có thể kích hoạt kỹ năng
    }
    void StartAbilityCooldown()//bắt đầu hồi chiêu
    {
        abilityComponent.StartCoroutine(CooldownCoroutine());
    }
    IEnumerator CooldownCoroutine()
    {
        abilityOnCooldown = true;//Đánh dấu kỹ năng đang trong trạng thái hồi chiêu
        onCooldownStarted?.Invoke();
        yield return new WaitForSeconds(cooldownDuration);
        abilityOnCooldown = false ;
    }

    internal Sprite GetAbilityIcon()
    {
        return AbilityIcon;
    }

    internal float GetCooldownDuration()
    {
        return cooldownDuration;
    }
}

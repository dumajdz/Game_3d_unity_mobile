using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/SpeedBoost")]
public class SpeedBoostAbility : Ability
{
    [Header("SpeedBoost")]
    [SerializeField] float boostAmt = 20f;
    [SerializeField] float boostDuration = 2f;

    //TODO: maybe rafacter this into movent component
    Player Player;
    public override void ActivateAbility()
    {
       if(!CommitAbility()) return;

       Player = AbilityComp.GetComponent<Player>();
       Player.AddMoveSpeed(boostAmt);
        AbilityComp.StartCoroutine(RestSpeed());
    }
    IEnumerator RestSpeed()
    {
        yield return new WaitForSeconds(boostDuration);
        Player.AddMoveSpeed(-boostAmt);
    }
    
}

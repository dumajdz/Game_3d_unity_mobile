using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviorTreeInterface
{
    public void RotateTowards(GameObject target, bool vertialAim = false);
    public void AttackTarget(GameObject target);
}

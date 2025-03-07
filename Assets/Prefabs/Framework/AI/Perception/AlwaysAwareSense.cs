using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysAwareSense : SenseComp
{
    [SerializeField] float awareDistance = 2f;
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        if (stimuli == null || stimuli.gameObject == null)
        {
            Debug.LogWarning("⚠️ Stimuli đã bị hủy nhưng vẫn đang được kiểm tra trong AlwaysAwareSense!");
            return false;
        }
        return Vector3.Distance(transform.position, stimuli.transform.position) <= awareDistance;
    }

    protected override void DrawDebug()
    {
        base.DrawDebug();
        Gizmos.DrawWireSphere(transform.position + Vector3.up, awareDistance);
    }
}

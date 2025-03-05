﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public abstract class SenseComp : MonoBehaviour//lớp chính
{
    [SerializeField] float forgettingTime = 3f;
    static List<PerceptionStimuli> registeredStimulis = new List<PerceptionStimuli>();//static(tĩnh) lớp phụ mới gọi được hàm trong lớp chính
    List<PerceptionStimuli> PerceivableStimulis = new List<PerceptionStimuli>();

    Dictionary<PerceptionStimuli, Coroutine> ForgettingRoutines = new Dictionary<PerceptionStimuli, Coroutine>();

    public delegate void OnPerceptionUpdated(PerceptionStimuli stimuli, bool successfulySence);

    public event OnPerceptionUpdated onPerceptionUpdated;   

    static public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        if (registeredStimulis.Contains(stimuli))
            return;
        registeredStimulis.Add(stimuli);

    }
    public void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Remove(stimuli);
    }
    protected  abstract bool IsStimuliSensable(PerceptionStimuli stimuli);
    // Update is called once per frame
    void Update()
    {
        foreach (var stimuli in registeredStimulis)
        {
            if (IsStimuliSensable(stimuli))
            {
                if (!PerceivableStimulis.Contains(stimuli))
                {
                    PerceivableStimulis.Add(stimuli);
                    if (ForgettingRoutines.TryGetValue(stimuli, out Coroutine routine))
                    {
                        StopCoroutine(routine);
                        ForgettingRoutines.Remove(stimuli);
                    }
                    else
                    {
                        onPerceptionUpdated?.Invoke(stimuli, true);
                    }
                }
            }
            else
            {
                if (PerceivableStimulis.Contains(stimuli))
                {
                    PerceivableStimulis.Remove(stimuli);
                    ForgettingRoutines.Add(stimuli, StartCoroutine(ForgetStimuli(stimuli)));
                }
            }
        }
    }
    IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(forgettingTime);
        ForgettingRoutines.Remove(stimuli);
        onPerceptionUpdated?.Invoke(stimuli,false);
        Debug.Log($"I lost track of {stimuli.gameObject}");
    }
    protected virtual void DrawDebug()
    {

    }
    private void OnDrawGizmos()
    {
        DrawDebug();
    }

    internal void AssignPerceivedStimuli(PerceptionStimuli targetStimuli)
    {
        PerceivableStimulis.Add(targetStimuli);
        onPerceptionUpdated!.Invoke(targetStimuli,true);

        //TODO: WHAT IF WE ARE FORGETTING IT
        if(ForgettingRoutines.TryGetValue(targetStimuli, out Coroutine forgetCoroutine))
        {
            ForgettingRoutines.Remove(targetStimuli);
        }
        
    }
}

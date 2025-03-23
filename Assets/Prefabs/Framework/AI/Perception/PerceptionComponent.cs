using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    [SerializeField] SenseComp[] senses; //mảng chứa các thành phần giác quan (SenesComp) để phát hiện đối tượng
    [Header("Audio")]
    [SerializeField] AudioClip DetectionAudio;
    [SerializeField] float volume = 1f;

    LinkedList<PerceptionStimuli> currentlyPerceivedStimulis = new LinkedList<PerceptionStimuli>();
    //Danh sách các mục tiêu hiện đang bị phát hiện

    PerceptionStimuli targetStimuli;//Mục tiêu quan trọng nhất được chọn từ danh sách trên

    public delegate void OnPerceptionTagetChanged(GameObject target, bool sensed);

    public event OnPerceptionTagetChanged onPerceptionTargetChanged;

    private void Awake()
    {
        foreach (SenseComp sense in senses)
        {
            sense.onPerceptionUpdated += SenceUpdated;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void SenceUpdated(PerceptionStimuli stimuli, bool successfulySence)
    {
        var nodeFound = currentlyPerceivedStimulis.Find(stimuli);
        if (successfulySence)// Đối tượng được phát hiện
        {            
            if (nodeFound != null)// Nếu đối tượng đã tồn tại trong danh sách
            {
                currentlyPerceivedStimulis.AddAfter(nodeFound, stimuli);
            }
            else
            {
                currentlyPerceivedStimulis.AddLast(stimuli);
            }
        }
        else
        {
            currentlyPerceivedStimulis.Remove(nodeFound);
        }
        if (currentlyPerceivedStimulis.Count !=0)
        {
            PerceptionStimuli highestStimuli = currentlyPerceivedStimulis.First.Value;
            if (targetStimuli == null || targetStimuli!=highestStimuli)
            {
                targetStimuli = highestStimuli;
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, true);
                Vector3 audioPos = transform.position;
                GameplayStatics.PlayAudioAtLoc(DetectionAudio,audioPos, volume);
            }
        }
        else
        {
            if(targetStimuli!= null)
            {
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject,false);
                targetStimuli = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void AssignPercievedStimui(PerceptionStimuli targetStimuli)
    {
        if (senses.Length != 0)
        {
            senses[0].AssignPerceivedStimuli(targetStimuli);
        }
    }
}

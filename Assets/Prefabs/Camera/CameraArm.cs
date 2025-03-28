using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraArm : MonoBehaviour
{
    [SerializeField] float armLength;
    [SerializeField] Transform child;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        child.position = transform.position - child.forward*armLength;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(child.position, transform.position);
    }
}

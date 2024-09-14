using System;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    [SerializeField] private GrabSystem grabSystem;
    [SerializeField] private GameObject snapTarget;
    [SerializeField] private float snapRadius = 0.1f;

    public event Action<PuzzleItem> OnSnapped;
    
    void Start()
    {
        grabSystem.OnRelease += (released) =>
        {
            if (released == this.gameObject)
            {
                TrySnap();
            }
        };
    }

    void TrySnap()
    {
        if (Vector3.Distance(transform.position, snapTarget.transform.position) <= snapRadius)
        {
            transform.position = snapTarget.transform.position;
            transform.rotation = snapTarget.transform.rotation;
            
            OnSnapped?.Invoke(this);

            tag = "Untagged";
        }
    }
}

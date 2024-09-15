using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzleItem : MonoBehaviour
{
    [SerializeField] private GameObject snapTarget;
    [SerializeField] private float snapRadius = 0.1f;
    
    [SerializeField] private AudioSource audioSource;
    
    private XRGrabInteractable grabInteractable;
    
    public event Action<PuzzleItem> OnSnapped;
    
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    void OnSelectExited(SelectExitEventArgs eventArgs)
    {
        if (eventArgs.interactableObject.transform.gameObject == gameObject)
        {
            TrySnap();
        }
    }

    void TrySnap()
    {
        if (Vector3.Distance(transform.position, snapTarget.transform.position) <= snapRadius)
        {
            transform.position = snapTarget.transform.position;
            transform.rotation = snapTarget.transform.rotation;
            
            OnSnapped?.Invoke(this);

            grabInteractable.enabled = false;
            
            audioSource.Play();
        }
    }
}

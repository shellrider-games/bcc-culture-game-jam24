using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ScenePortal : MonoBehaviour
{
    [SerializeField] Transform target;
    private BoxCollider trigger;
    
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = target.position;
        }
    }
}

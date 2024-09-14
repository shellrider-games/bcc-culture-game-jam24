using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GrowOnInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grow()
    {
        SceneManager.LoadScene("Painting");
    }
    
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Grow();
    }
}

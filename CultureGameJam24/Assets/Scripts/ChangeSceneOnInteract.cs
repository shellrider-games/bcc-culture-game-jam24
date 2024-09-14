using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeSceneOnInteract : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Painting");
    }
    
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        ChangeScene();
    }
}

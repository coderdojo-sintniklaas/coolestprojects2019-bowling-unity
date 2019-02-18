using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartOpnieuw()
    {
        // Laad start scene
        SceneManager.LoadScene("startScene");
    }
}

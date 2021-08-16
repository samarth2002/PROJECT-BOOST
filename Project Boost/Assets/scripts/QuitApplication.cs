using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("You have exited the game"); 
           Application.Quit();
        }
    }
}

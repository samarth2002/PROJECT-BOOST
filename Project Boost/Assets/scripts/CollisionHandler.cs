
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    string TagCheck;
    AudioSource audioSource;
    [SerializeField] AudioClip CrashSound , LevelSound; 
    [SerializeField] ParticleSystem CrashParticles , LevelParticle;
    [SerializeField] float timeDelay=2f;
    bool isTransitioning = false;
    bool ColDisable=false;
    void Start()
    {
      audioSource=GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ColDisable = !ColDisable;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeLevel();
        }
    }

    void OnCollisionEnter(Collision other)
    {
      
      TagCheck=other.gameObject.tag;
      if(isTransitioning||ColDisable){return;}

      switch(TagCheck)
      {
        case "Friendly" : Debug.Log("You have collided with a friendly!!");break;
        case "Finish"   : ChangeLevelSequence();break;
        default         : StartCrashSequence();break;
      }

    }    
    void Respawn()
    {
      int GetSceneIndex = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(GetSceneIndex);
    }
    void StartCrashSequence()
    {
      isTransitioning = true;
      CrashParticles.Play();
      audioSource.Stop();
      audioSource.PlayOneShot(CrashSound);
      GetComponent<Movement>().enabled=false;
      Invoke("Respawn",timeDelay);
    }
    void ChangeLevel()
    {
      int LevelIndex = SceneManager.GetActiveScene().buildIndex+1;
      if(LevelIndex==SceneManager.sceneCountInBuildSettings)
      {
        LevelIndex = 0;
      } 
      SceneManager.LoadScene(LevelIndex); 
    }
    void ChangeLevelSequence()
    {
      isTransitioning = true;
      audioSource.Stop();
      LevelParticle.Play();
      audioSource.PlayOneShot(LevelSound);
      GetComponent<Movement>().enabled=false;
      Invoke("ChangeLevel",timeDelay);
    }
}

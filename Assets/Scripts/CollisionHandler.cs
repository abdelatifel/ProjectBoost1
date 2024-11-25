using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{ 

    [SerializeField] float LevelLoadDelay=2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem  successParticle;
    [SerializeField] ParticleSystem  crashParticle;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisiondisabled=false;

     void Start()
      {
        audioSource=GetComponent<AudioSource>();
        
    }

    void Update()
    {
       RespondToDebugKey();
    }
 void RespondToDebugKey()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisiondisabled=!collisiondisabled;
        }
    }
    void OnCollisionEnter(Collision other) {

        if(isTransitioning || collisiondisabled ){ return;}
        
else {


        switch(other.gameObject.tag)
        {
         case "Friendly":
            Debug.Log("it is Friendly");
            break;

        case "Finish":
            StarSuccessSequence();
            break;
        case "Fuel":
        Debug.Log("op u picked up fuel");
        break;

        default:
        StarCrashSequence();
        break;
        

        }
    }
        
    }

    void ReloadLevel()
    {
        int CurrentIndexScene=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentIndexScene);
    }
    void NextLevel()
    {
        int CurrentIndexScene=SceneManager.GetActiveScene().buildIndex;
            int NextSceneIndex =  CurrentIndexScene+1;
            if(NextSceneIndex==SceneManager.sceneCountInBuildSettings)
            {
                NextSceneIndex=0;
            }
            SceneManager.LoadScene(NextSceneIndex);
    }
    
    
    
    void   StarSuccessSequence()
    {
        isTransitioning= true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        GetComponent<Movement>().enabled= false;
        Invoke("NextLevel",LevelLoadDelay);
    }

    void StarCrashSequence()
    {
        isTransitioning= true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticle.Play();

       
        GetComponent<Movement>().enabled= false;
        Invoke("ReloadLevel",LevelLoadDelay);
    }

    

}


using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    void OnCollisionEnter(Collision other){
        switch (other.gameObject.tag){
            case "Friendly":
                Debug.Log("Standing on the start");
                break;
            case "Finish":
                StartWinSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel chuel bhar lo");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    void StartWinSequence(){
        // todo add SFX upon win
        // todo add particle effect
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel",delayTime);
    }
    void StartCrashSequence(){
        // todo add SFX upon crash
        // todo add particle effect
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",delayTime);

    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void NextLevel(){
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}

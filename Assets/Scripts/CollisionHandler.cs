
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelDelay = 1.3f;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem winParticles;
    [SerializeField] ParticleSystem crashParticles;
    

    AudioSource audioSource;

    bool isTransitioning = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {

        if (isTransitioning)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Standing on the start");
                break;
            case "Finish":
                isTransitioning = true;
                StartWinSequence();
                break;
            case "Checkpoint":
                MarkCheckpoint();
                break;
            default:
                isTransitioning = true;
                StartCrashSequence();
                break;
        }

    }
    void StartWinSequence()
    {
        IntroFader.LevelChange = true;
        winParticles.Play();
        audioSource.Stop();
        audioSource.volume = 1f;
        audioSource.PlayOneShot(winSound);
        GetComponent<Movement>().enabled = false;
        Invoke("DisappearRocket",0.03f);
        Invoke("NextLevel", levelDelay);
    }
    void StartCrashSequence()
    {
        DisappearRocket();
        crashParticles.Play();
        audioSource.Stop();
        audioSource.volume = 1f;
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelDelay);

    }

    void DisappearRocket(){
        transform.Find("SpaceRocket").GetComponent<MeshRenderer>().enabled = false;
        transform.Find("SpaceRocket").transform.Find("Flap1").GetComponent<MeshRenderer>().enabled = false;
        transform.Find("SpaceRocket").transform.Find("Flap2").GetComponent<MeshRenderer>().enabled = false;
        transform.Find("SpaceRocket").transform.Find("Flap3").GetComponent<MeshRenderer>().enabled = false;
        transform.Find("SpaceRocket").transform.Find("Flap4").GetComponent<MeshRenderer>().enabled = false;
        transform.Find("Main Booster").gameObject.SetActive(false);
        transform.Find("Left Booster").gameObject.SetActive(false);
        transform.Find("Right Booster").gameObject.SetActive(false);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void NextLevel()
    {
        Movement.checkpoint = 0;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 2;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void MarkCheckpoint(){
        
        GameObject.Find("Checkpoint 1 Light").GetComponent<Light>().color = new Color(0f/255f,100f/255f,255f/255f);
        if (Movement.checkpoint != 1){
        audioSource.Stop();
        audioSource.volume = 1f;
        audioSource.PlayOneShot(winSound);
        GameObject.Find("CheckpointText").GetComponent<MeshRenderer>().enabled = true;
        Invoke("RemoveCheckText", 3f);
        }
        Movement.checkpoint = 1;
        
    }

    void RemoveCheckText(){
        GameObject.Find("CheckpointText").GetComponent<MeshRenderer>().enabled = false;
    }

}

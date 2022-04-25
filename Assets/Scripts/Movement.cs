using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rb;
    AudioSource audioSource;
    public static int checkpoint = 0;
    public static Vector3 rocketPosition;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        switch (checkpoint){
            case 0:
                switch (SceneManager.GetActiveScene().buildIndex){
                    case 2:
                        rocketPosition = new Vector3(-4.58f, 1.324f, 0f);
                        break;
                    case 4:
                        rocketPosition = new Vector3(6.28f, 1.324f, 0f);
                        break;
                    case 6:
                        rocketPosition = new Vector3(-11.93f,1.324f,0f);
                        break;
                }
                break;
            case 1:
                switch (SceneManager.GetActiveScene().buildIndex){
                    case 4:
                        rocketPosition = new Vector3(22.3f, 1.324f, 0f);
                        break;
                    case 6:
                        rocketPosition = new Vector3(6.58f, 1.324f, 0f);
                        break;
                }
                break;
            
        
        }
        transform.position = rocketPosition;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Stop();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();

        }
        else
        {
            StopRotating();
        }
    }


    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        audioSource.volume = 1;

        if (!audioSource.isPlaying)
        {

            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    void StopThrusting()
    {
        mainBooster.Stop();
        IEnumerator fadeSound1 = Movement.FadeOut(audioSource, 0.1f);
        StartCoroutine(fadeSound1);
        StopCoroutine(fadeSound1);
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }


    void StopRotating()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }


    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;

    }


    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }


        audioSource.volume = startVolume;
    }



    //END
}

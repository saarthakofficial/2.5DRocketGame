
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class IntroFader : MonoBehaviour
{
    [SerializeField] GameObject Rocket;
    public static bool LevelChange = true;
    public Canvas canvas;
    public float fadeDuration = 2f;
 
    private float maxAlpha = 1;
    private float lerpParam;
    private float startAlpha;
    private float canvasGroupAlpha;
 
    void Start()
    {
        canvasGroupAlpha = canvas.GetComponent<CanvasGroup>().alpha;
        startAlpha = canvas.GetComponent<CanvasGroup>().alpha;
        if (LevelChange==true){
            Rocket.GetComponent<Movement>().enabled = false;
        }
    }
 
    void Update()
    {
        if (LevelChange==true){
            
  
            Invoke("FadingCanvas",1f);
            Invoke("EnableMovement",3f);
        }
        else{
            gameObject.SetActive(false);
        }
        
    }

    void FadingCanvas()
    {
        lerpParam += Time.deltaTime;

        canvasGroupAlpha = Mathf.Lerp(startAlpha, maxAlpha, lerpParam / fadeDuration);
        canvas.GetComponent<CanvasGroup>().alpha = canvasGroupAlpha;
        if (canvasGroupAlpha >= 1)
        {
            FadeTo(0);
        }
    }

    void FadeTo(float alpha)
    {
        maxAlpha = alpha;
        lerpParam = 0;
    }
    
    void EnableMovement(){
        Rocket.GetComponent<Movement>().enabled = true;
        LevelChange = false;
    }



}
 
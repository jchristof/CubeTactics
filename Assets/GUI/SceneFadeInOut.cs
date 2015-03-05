using UnityEngine;
using System.Collections;
using System;
using FMOD.Studio;

public class SceneFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 0.5f;          // Speed that the screen fades to and from black.
    
    
    private bool sceneStarting = true;      // Whether or not the scene is still fading in.

    public float Level { get; set; }
    public bool SceneEnding { get; set; }

    public Action OnLevelLoad { get; set; }

    void Awake ()
    {
        // Set the texture so that it is the the size of the screen and covers it.
        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        
    }
    
    
    void Update ()
    {
        // If the scene is starting...
        if(sceneStarting)
            // ... call the StartScene function.
            StartScene();
        else if(SceneEnding)
            EndScene();
    }
    
    
    void FadeToClear ()
    {
        // Lerp the colour of the texture between itself and transparent.
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
    
    
    void FadeToBlack ()
    {
        // Lerp the colour of the texture between itself and black.
        Level = fadeSpeed * Time.deltaTime;
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, Level);

        //FMOD_StudioEventEmitter emitter = GameObject.Find("Cube").GetComponent<FMOD_StudioEventEmitter>();
        //FMOD.Studio.ParameterInstance param = emitter.getParameter("Progression");

        //FMOD.Studio.System system = FMOD_StudioSystem.instance.System;
        //FMOD.Studio.EventDescription zoom;
        //system.getEvent("event:/Music/Music", out zoom);
        //FMOD.Studio.EventInstance musicInstance;
        //zoom.createInstance(out musicInstance);
        //musicInstance.setVolume(Level);
    }
    
    
    void StartScene ()
    {
        // Fade the texture to clear.
        FadeToClear();
        
        // If the texture is almost clear...
        if(guiTexture.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the GUITexture.
            guiTexture.color = Color.clear;
            guiTexture.enabled = false;
            
            // The scene is no longer starting.
            sceneStarting = false;
        }
    }
    
    
    public void EndScene ()
    {
        // Make sure the texture is enabled.
        guiTexture.enabled = true;
        
        // Start fading towards black.
        FadeToBlack();
        
        // If the screen is almost black...
        if (guiTexture.color.a >= 0.95f)
            OnLevelLoad();
    }
}

using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer musicMixer; 
    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup soundMixerGroup;

    public Sound[] gameSounds;
    public Sound[] mainMenuSounds;

    public bool updateMade = false;

    Sound[] IngameBackgroundSounds; 

    private void Awake() {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        volumeTransfer(gameSounds);
        volumeTransfer(mainMenuSounds);

        IngameBackgroundSounds = Array.FindAll(gameSounds, sound => sound.name.Contains("GameBackgroundSound") );
    }

    public void Play(string name, int sceneNum)
    {   
        Sound s = null;

        switch(whichSceneAreYou(sceneNum)){
            case "mainMenu":
                s = Array.Find(mainMenuSounds, sound => sound.name == name);
                break;
            case "game":
                s = Array.Find(gameSounds, sound => sound.name == name);
                break;
            default:
                Debug.Log("Wrong scene type in Audio Manager");
                break;
        }

        if(s==null)
        {
            Debug.LogWarning("Sound: " + name + "not found at scene: " + sceneNum);
            return;
        }
        s.source.Play();
    }

    public void PlaySoundContaining(string containedString, int sceneNum){
        
        Sound[] sounds = null;
        
        switch(whichSceneAreYou(sceneNum)){
            case "mainMenu":
                sounds = Array.FindAll(mainMenuSounds, sound => sound.name.Contains(containedString));
                break;
            case "game":
                sounds = Array.FindAll(gameSounds, sound => sound.name.Contains(containedString));
                break;
            default:
                Debug.Log("Wrong scene type in Audio Manager");
                break;
        }
        int randomNum = UnityEngine.Random.Range(0, sounds.Length);
        
        if(sounds[randomNum].source.isPlaying && sounds.Length > 1){
            int newRandomNum = 0;
            while(newRandomNum == randomNum){
                newRandomNum = UnityEngine.Random.Range(0, sounds.Length);
            }
            randomNum = newRandomNum;
        }

        Play(sounds[randomNum].name, sceneNum);
    }
    
    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Sound[] soundBackground;
        switch(whichSceneAreYou(scene.buildIndex)){
            case "mainMenu":
                soundBackground = Array.FindAll(mainMenuSounds, sound => sound.name == "MainMenuBackgroundSound");
                if(!soundBackground[0].source.isPlaying)
                    Play("MainMenuBackgroundSound", scene.buildIndex);
                disableSounds(gameSounds);
                break;
            case "game":
                soundBackground = Array.FindAll(gameSounds, sound => sound.name.Contains("GameBackgroundSound") );
                if(!CheckIfSoundsArePlaying(soundBackground))
                    PlaySoundContaining("GameBackgroundSound", scene.buildIndex);
                InvokeRepeating("LoopBackgroundSoundsInGame", 2.0f, 2.0f);
                disableSounds(mainMenuSounds);
                break;
            default:
                Debug.Log("Wrong scene type in Audio Manager");
                break;
        }
    }

    void LoopBackgroundSoundsInGame(){
        Debug.Log("INVOKED ahhahahahahaha ");
        if(!CheckIfSoundsArePlaying(IngameBackgroundSounds)){
            Scene scene = SceneManager.GetActiveScene();

            switch(whichSceneAreYou(scene.buildIndex)){
                case "mainMenu":
                    CancelInvoke();
                    break;
                case "game":
                    PlaySoundContaining("GameBackgroundSound", scene.buildIndex);
                    break;
            }
        }
    }

    bool CheckIfSoundsArePlaying(Sound[] sounds){
        for(int i = 0; i < sounds.Length; i++){
            if(sounds[i].source.isPlaying){
                return true;
            }
        }
        return false;
    }

    public void disableSounds(Sound[] sounds){
        foreach (Sound s in sounds)
        {
            if(!s.name.Contains("ButtonPressed"))
                s.source.Stop();
        }
    }

    public string whichSceneAreYou(int sceneNum){
        if(sceneNum >= 1 && sceneNum <= 6 || sceneNum == 19){
            return "mainMenu";
        }
        else{
            if(sceneNum >= 7 && sceneNum <= 18){
                return "game";
            }
            else{
                Debug.Log("The scene number: " + sceneNum + "has not been taken into account in the audio manager");
                return null;
            }
        }
    }

    private void volumeTransfer(Sound[] sounds){
        
        //Debug.Log(sounds.Length);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); 

            s.source.clip = s.clip;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            if(s.name == "MainMenuBackgroundSound" || s.name.Contains("GameBackgroundSound")){
                s.source.outputAudioMixerGroup = musicMixerGroup;
            }
            else{
                s.source.outputAudioMixerGroup = soundMixerGroup;
            }
        }
    }
}

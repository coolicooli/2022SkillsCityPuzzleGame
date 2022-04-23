using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    public static AudioManager audioManager;

    [SerializeField] private AudioSource audioSource;

    // Managed sounds for game each set from Unity
    [SerializeField]
    private AudioClip background, buttonDown, buttonUp, menuSwish,
                      playerCollision, playerDenied, playerStateChange,
                      winningSound, losingSound, newLevel, shootingSound;
                      
                        

    private bool isSoundOption = true;
    private bool backgroundMusicStated = false;

    // init class instance
    private void Awake()
    {
        audioManager = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // play background sound once
    public bool PlayBackgroundSound()
    {
        if (isSoundOption)
        {
            if (!backgroundMusicStated)
                PlayAudio(background);

            backgroundMusicStated = true;
            return true;
        }
        return false;
    }

    // play button down sound
    public bool PlayButtonDown()
    {
        if (isSoundOption)
        {
            PlayAudio(buttonDown);
            return true;
        }
        return false;
    }

    // play button up sound
    public bool PlayButtonUp()
    {
        if (isSoundOption)
        {
            PlayAudio(buttonUp);
            return true;
        }
        return false;
    }

    // play swish sound
    public bool PlayMenuSwish()
    {
        if (isSoundOption)
        {
            PlayAudio(menuSwish);
            return true;
        }
        return false;
    }

    // play Player Collision sound
    public bool PlayPlayerCollision()
    {
        if (isSoundOption)
        {
            PlayAudio(playerCollision);
            return true;
        }
        return false;
    }

    // play Player Denied sound
    public bool PlayPlayerDenied()
    {
        if (isSoundOption)
        {
            PlayAudio(playerDenied);
            return true;
        }
        return false;
    }

    // play Player State Changed
    public bool PlayPlayerSateChanged()
    {
        if (isSoundOption)
        {
            PlayAudio(playerStateChange);
        }
        return false;
    }

    // play shooting sound
    public bool PlayShootingSound()
    {
        if (isSoundOption)
        {
            PlayAudio(shootingSound);
        }
        return false;
    }

    // play Player winning game
    public bool PlayPlayerWinSound()
    {
        if (isSoundOption)
        {
            PlayAudio(winningSound);
        }
        return false;
    }

    // play Player losing game
    public bool PlayPlayerLoseSound()
    {
        if (isSoundOption)
        {
            PlayAudio(losingSound);
        }
        return false;
    }

    // play Player starts new level
    public bool PlayNewLevel()
    {
        if (isSoundOption)
        {
            PlayAudio(newLevel);
        }
        return false;
    }

    // play clip
    private void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Toggle Mute sound
    public void EnableSound(bool state)
    {
        isSoundOption = state;
        audioSource.mute = !state;
        
        //return audioSource.enabled;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameSoundEnable : MonoBehaviour
{
    public string ButtonType;
    [SerializeField] private Sprite objectEnable; //enabled image
    [SerializeField] private Sprite objectDisable; // disabled image

    private Button button; // this button
    
    private bool soundState; // current state

    const string GAME_DATA_FILENAME = "\\slimon.dat";
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>(); 
        
        soundState = true;

        InitialiseGameSettings(); // first get settings
        
        SoundEnable(soundState);

        AudioManager.audioManager.EnableSound(soundState);
    }

    // Sound option change click method
    public void OnOptionChange()
    {
        soundState = !soundState;

        SoundEnable(soundState);

        AudioManager.audioManager.EnableSound(soundState);

        SaveSettings();
    }

    // Change Enable / Disable sound button image
    private void SoundEnable(bool State)
    {
        Sprite ButtonOptionImage;
        
        if (State)
        {
            ButtonOptionImage = objectEnable;
        }
        else
        {
            ButtonOptionImage = objectDisable;
        }

        button.image.sprite = ButtonOptionImage; ;
    }

    // First Init Game option sound setting from file
    private void InitialiseGameSettings()
    {
        try
        {
            // From file eg Key/Value  Sound=True;
            string settings = LoadSettings(); 

            // Split lines by semi colon
            string[] part = settings.Split(';');

            // Find key part - split by equals to get value
            for (int i = 0; i < part.Length - 1; i++)
            {
                string[] option = part[i].Split('=');
                
                //find key and set value
                switch (option[0].ToLower())
                {
                    case "sound":
                        soundState = option[1].Contains("ue");
                        break;

                    default:
                        break;
                }
            }
        }
        catch 
        {
        }

    }

    // Get setting from file returns the settings data
    private string LoadSettings()
    {
        string filename = Application.persistentDataPath + GAME_DATA_FILENAME;
        string data = "";
        
        if (File.Exists(filename))
        {
            data = File.ReadAllText(@filename);
        }
        return data;
    }

    // Save settings to file
    private bool SaveSettings()
    {
        string filename = Application.persistentDataPath + GAME_DATA_FILENAME;
        string data = "sound=" + soundState.ToString().ToLower() + ";";

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(data);
        }
        return true;
    }
}

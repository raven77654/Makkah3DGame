using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageSelector : MonoBehaviour
{
    public static LanguageSelector Instance;
   
    [SerializeField]private string selectedLanguage = "English"; // Default to English

    private void Awake()
    {
        // Make sure there is only one instance of this class
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectEnglish()
    {
        selectedLanguage = "English";
        Debug.Log("English selected.");
    }

    public void SelectUrdu()
    {
        selectedLanguage = "Urdu";
        Debug.Log("Urdu selected.");
    }

    public  string GetSelectedLanguage()
    {
        return selectedLanguage;
    }
}

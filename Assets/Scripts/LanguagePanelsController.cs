using UnityEngine;

public class LanguagePanelsController : MonoBehaviour
{
    public GameObject englishPanels; // Parent object for all English panels
    public GameObject urduPanels;    // Parent object for all Urdu panels

    private GameObject activePanels;

    private void Start()
    {
        ApplyLanguage();
    }

    public void ApplyLanguage()
    {
        string selectedLanguage = LanguageSelector.Instance.GetSelectedLanguage();

        if (englishPanels != null) englishPanels.SetActive(false);
        if (urduPanels != null) urduPanels.SetActive(false);

        if (selectedLanguage == "English" && englishPanels != null)
        {
            englishPanels.SetActive(true);
            activePanels = englishPanels;
            Debug.Log("English panels activated.");
        }
        else if (selectedLanguage == "Urdu" && urduPanels != null)
        {
            urduPanels.SetActive(true);
            activePanels = urduPanels;
            Debug.Log("Urdu panels activated.");
        }
    }

    public GameObject GetActivePanels()
    {
        return activePanels;
    }
}

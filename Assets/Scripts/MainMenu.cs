using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);

        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        // Debug.Log("Quit button pressed");
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        // Debug.Log("Volume set to: " + volume);
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}

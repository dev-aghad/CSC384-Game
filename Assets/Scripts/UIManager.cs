using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text powerUpText;
    [SerializeField] private TMP_Text waveText;

    public void ShowPowerUpText(string message)
    {
        powerUpText.text = message;
        StartCoroutine(ClearText(powerUpText));
    }

    public void UpdateWave(int wave)
    {
        waveText.text = "Wave " + wave;
    }

    public void ShowWaveText(string message)
    {
        StartCoroutine(TemporaryText(waveText, message));
    }

    // For messages that disappear like when a powerup is activated
    private IEnumerator ClearText(TMP_Text textToClear)
    {
        yield return new WaitForSeconds(2f);
        textToClear.text = "";
    }

    // For when new waves are announced
    private IEnumerator TemporaryText(TMP_Text textToClear, string message)
    {
        string original = textToClear.text;

        textToClear.text = message;

        yield return new WaitForSeconds(2f);

        textToClear.text = original;
    }
}

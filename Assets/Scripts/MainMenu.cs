using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Volume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(2);
    }
}

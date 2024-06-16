using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MeniuUI : MonoBehaviour
{
    [SerializeField] private Button play_buton;
    [SerializeField] private Button quit_buton;

    private void Awake()
    {
        play_buton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Jocul);
        });
        quit_buton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Time.timeScale = 1.0f;
    }
}

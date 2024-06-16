using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        Meniu,
        Jocul,
        Loading
    }
    private static Scene scena_nume;

    public static void Load(Scene scena_nume)
    {
        Loader.scena_nume = scena_nume;
        SceneManager.LoadScene(Scene.Loading.ToString());
    }
    public static void LoaderCallback()
    {
        SceneManager.LoadScene(scena_nume.ToString());

    }
}

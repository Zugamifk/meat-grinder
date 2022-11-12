using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Scenes
{
    const string BOOT_SCENE_NAME = "Game";
    const string NAVIGATION_SCENE_NAME = "Navigation";
    const string SHIP_INTERIOR_SCENE_NAME = "ShipInterior";

    public static void ReloadGame()
    {
        // unload all scene, load boot scene
    }

    public static void LoadShipInterior()
    {
        SceneManager.LoadSceneAsync(SHIP_INTERIOR_SCENE_NAME);
    }

    public static void LoadNavigation()
    {
        SceneManager.LoadSceneAsync(NAVIGATION_SCENE_NAME);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableblocks;

    SceneLoader sceneloader;

     void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();
    }
    public void CountingBlocks()
    {
        breakableblocks++;
    }

    public void BrokenBlock()
    {
        breakableblocks--;
        if (breakableblocks == 0)
        {
            sceneloader.LoadNextScene();
        }
    }
}

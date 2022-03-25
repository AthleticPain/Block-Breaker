using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks; // Serialized for debugging purposes

    SceneLoaderScript scene;

    private void Start()
    {
        scene = FindObjectOfType<SceneLoaderScript>();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void DecrementBlockNo()
    {
        breakableBlocks--;
        if(breakableBlocks == 0)
        {
            scene.LoadNextScene();
        }
    }
}


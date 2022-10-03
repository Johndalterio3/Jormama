using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public GameObject[] entrances; //0-z, 1-x, 2--z, 3--x
    public GameObject[] walls;


    public void updateRoom(bool[] exists)
    {
        for (int i = 0; i < exists.Length; i++)
        {
            entrances[i].SetActive(exists[i]);
            walls[i].SetActive(!exists[i]);
        }
    }
}

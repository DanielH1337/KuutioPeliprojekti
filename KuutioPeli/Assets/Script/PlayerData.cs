using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Save ja load systeemi binaryformatterilla
    public int level;
   
    public int health;
    public float[] position;
    public float elapsedTime;


    public PlayerData(Player player)
    {
        level = player.level;
        health = player.health;
        elapsedTime=player.elapsedTime;
       

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
    
}
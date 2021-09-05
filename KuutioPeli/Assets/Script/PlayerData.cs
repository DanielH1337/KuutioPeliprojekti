using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Save ja load systeemiin ladattavat muuttujat

   

    public float[] position;
    public float elapsedTime;
    public float[] CubePos;


    public PlayerData(Player player)
    {


        elapsedTime=player.elapsedTime;
       

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        CubePos = new float[4];
        CubePos[0] = player.worldCube.transform.rotation.x;
        CubePos[1] = player.worldCube.transform.rotation.y;
        CubePos[2] = player.worldCube.transform.rotation.z;
        CubePos[3] = player.worldCube.transform.rotation.w;
    }
    
}

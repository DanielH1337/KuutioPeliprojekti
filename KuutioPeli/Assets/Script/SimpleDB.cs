using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class SimpleDB : MonoBehaviour
{

    private string dbName = "URI=file:Inventory.db";
    // Start is called before the first frame update
    void Start()
    {
        CreateDB();
        AddWeapon("Silver sword", 30);
        DisplayWeapons();
    }
    //Metodi jossa luodaan tietokanta
    public void CreateDB()
    {
        //Luodaan yhteys tietokantaan
        using (var connection = new SqliteConnection(dbName))
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS weapons (name VARCHAR(20), damage INT);";
                command.ExecuteNonQuery();
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

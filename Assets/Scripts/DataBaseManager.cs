using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;
using System.IO;

public static class DataBaseManager
{
    private static SQLiteConnection db => new SQLiteConnection(Application.streamingAssetsPath + "/Database/ScoreEndlessWings.db");

    public static List<DBScore> scores => db.Query<DBScore>("SELECT * FROM Scores");

    public static void TestDB()
    {
        foreach (DBScore score in scores)
        {
            Debug.Log(score.name + ": " + score.score);
        }
    }
    public class DBScore
    {
        public int id { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        public string dateTime { get; set; }
    }

   
    
}

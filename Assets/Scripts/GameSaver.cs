using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;
public class GameSaver {

    private readonly IGameplayManager gameplayManager;
    public GameSaver(IGameplayManager gameplayManager) {
        this.gameplayManager = gameplayManager;
    }
    public void Save() {
        SavedData save = new SavedData();
        save.Score = gameplayManager.score; 
        string json = JsonUtility.ToJson(save);
        Debug.Log(Application.persistentDataPath +json);
        FileStream file = File.Create(Application.persistentDataPath + Constants.SAVE_PATH);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(json);
        file.Flush();
        file.Close(); 
    }

    public void Restore() {

        if (File.Exists(Application.persistentDataPath + Constants.SAVE_PATH)) {
            FileStream file = File.Open(Application.persistentDataPath + Constants.SAVE_PATH, FileMode.Open);
            BinaryReader reader = new BinaryReader(file);
            SavedData data = JsonUtility.FromJson<SavedData>(reader.ReadString());
            gameplayManager.score = data.Score;
            file.Close(); 
        }
    }
}
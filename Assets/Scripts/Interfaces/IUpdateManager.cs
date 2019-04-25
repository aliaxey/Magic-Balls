using UnityEngine;
using UnityEditor;

public interface IUpdateManager {
    void Update();
    void AddSubscriber(IUpdatable updatable);
    void RemoveSubscriber(IUpdatable updatable);
}


using UnityEngine;
using UnityEditor;

public interface IMeshFiller {
    ICorutinier Coroutinier { get; set; }
    void Update();
    void FillMesh();
}
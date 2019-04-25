using UnityEngine;

public interface IMeshManager {
    Cell[,] Mesh { get; }
    void AddBall(BallType type, int col);
    void CreateRandomBall(int col, int row);
    Vector3 AbsolutePos(Cell cell);


}
using UnityEngine;

public interface IObjectPublisher {
    GameObject CreateBall(BallType type, Vector3 position);
}

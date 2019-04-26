using UnityEngine;

public interface IObjectCreator {
    Object CreateNewBall(BallType ball);
    Object CreateNewBoost(BallType ball);
}

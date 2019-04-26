using UnityEngine;
using System.Collections;

public class ObjectPubisher : IObjectPublisher {
    private readonly IObjectCreator creator;
    private readonly IInputMamager input;


    public ObjectPubisher(IObjectCreator objectCreator,IInputMamager inputMamager) {
        creator = objectCreator;
        input = inputMamager; 
    }
    public GameObject CreateBall(BallType type,Vector3 position) {
        Object ball = creator.CreateNewBall(type);
        return Publish(ball, position);
    }

    public GameObject CreateBooster(BallType type, Vector3 position) {
        Object ball = creator.CreateNewBoost(type);
        return Publish(ball, position);
    }

    private GameObject Publish(Object obj, Vector3 position) {
        GameObject gameObject = Object.Instantiate(obj, position, Quaternion.identity)as GameObject;
        gameObject.AddComponent<TochHandler>();
        var handler = gameObject.GetComponent<TochHandler>();
        handler.inputMamager = input;
        return gameObject;
    }
}

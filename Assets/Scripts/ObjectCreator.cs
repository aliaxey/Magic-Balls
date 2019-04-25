using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectCreator :  IObjectCreator {
    IDictionary chace = new Dictionary<BallType, Object>();

    public Object CreateNewBall(BallType ball) {
        
        if (!chace.Contains(ball)) {
            string link = "";
            switch (ball) {
                case BallType.RED:
                    link = "Prefabs/redBall";
                    break;
                case BallType.GREEN:
                    link = "Prefabs/greenBall";
                    break;
                case BallType.BLUE:
                    link = "Prefabs/blueBall";
                    break;
                case BallType.YELLOW:
                    link = "Prefabs/yellowBall";
                    break;
            }
            var element = Resources.Load(link);
            chace.Add(ball, element);
            
        }
        return (Object)chace[ball];
        
    }
}

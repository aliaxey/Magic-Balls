using System.Collections;
using UnityEngine;

public class Bootstrapper : MonoBehaviour {
    public static GameSaver gameSaver; //TODO rm
	void Start () {

        IObjectCreator creator = new ObjectCreator();
        IInputMamager inputMamager = new InputManager();
        IObjectPublisher publisher = new ObjectPubisher(creator, inputMamager);
        IMeshManager meshManager = new MeshManager(publisher);
        IGameplayManager gameplayManager = new GameplayManager(meshManager);

        
        var updateManagerHolder = new GameObject("UpdateManagerHolder");
        updateManagerHolder.AddComponent<UpdateManager>();
        updateManagerHolder.AddComponent<Corutinier>();
        
		
        IUpdateManager updateManager = updateManagerHolder.GetComponent<UpdateManager>();
        updateManager.AddSubscriber(gameplayManager);
        inputMamager.AddSubscriber(gameplayManager);
        ICorutinier corutinier = updateManagerHolder.GetComponent<Corutinier>();
        gameSaver = new GameSaver(gameplayManager);
        gameSaver.Restore(); 
        //gameplayManager.FillMesh(Constants.HEIGHT-1);  
        //meshFiller.FillMesh();

    }

    void Update () {
		
	}
}
        

        //publisher.Publish(creator.CreateNewBall(BallType.GREEN), new Vector3(0, 1, 0));
        //_objectStorage.AddUpdateManager

        // А он создается как new GameObject("UpdateManager")

        //  А потом AddComponent
        /*StartCoroutine(Lol());
          	IEnumerator Lol()
          	{
          		yield return new WaitForSeconds(1);
          		Debug.Log("ogo");
          	}
                 meshManager.AddBall(BallType.BLUE, 2);
              meshManager.AddBall(BallType.GREEN, 2);
              meshManager.AddBall(BallType.RED, 0);
              meshManager.AddBall(BallType.RED, 4);
               */
        /*
 for(int i = 0; i < Constants.HEIGHT; i++) {
     for (int j = 0; j < Constants.WIDTH; j++) {
         int rand = Random.Range(0, 4);
         switch (rand) {
             case 0:
                 meshManager.AddBall(BallType.RED,j);
                 break;
             case 1:
                 meshManager.AddBall(BallType.GREEN, j);
                 break;
             case 2:
                 meshManager.AddBall(BallType.BLUE, j);
                 break;
             case 3:
                 meshManager.AddBall(BallType.YELLOW, j);
                 break;
             
         }

     }
 }
 */
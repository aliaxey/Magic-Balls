using System.Collections;
using UnityEngine;
using UnityEditor;

public class MeshFiller : IMeshFiller {
    IMeshManager _meshManager;
    private ICorutinier _corutinier;
    private int col = 0;
    private int row = 0;
    public MeshFiller(IMeshManager manager) {
        _meshManager = manager;
    }

    public ICorutinier Coroutinier
    {
        get
        {
            return _corutinier;
        } 
        set 
        { 
            _corutinier = value;         
        }
    }

    public void FillMesh() {
         _corutinier.StartCoroutine(AddBall());
    }

    private BallType GetRandomBall()
    {
        int rand = Random.Range(0, 4);
        switch (rand) {
            case 0:
                return BallType.RED;
                break;
            case 1:
                return BallType.GREEN;
                break;
            case 2:
                return BallType.BLUE;
                break;
            default:
                return BallType.YELLOW;
        }
    }
    
    IEnumerator AddBall()
    {
        yield return new WaitForSeconds(Constants.SPAM_DELAY);
        
        _meshManager.AddBall(GetRandomBall(),col);
        col++;
        if (col == Constants.WIDTH)
        {
            col = 0;
            row++;
        }

        if (row < Constants.HEIGHT)
        {
            _corutinier.StartCoroutine(AddBall());
        }
    }

    public void Update() {
        throw new System.NotImplementedException();
    }
    
}
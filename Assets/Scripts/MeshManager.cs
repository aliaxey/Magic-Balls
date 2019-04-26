using UnityEngine;

public class MeshManager:IMeshManager{

    readonly IObjectPublisher publisher;
    public Cell[,] Mesh { get; private set; }
    

    public MeshManager(IObjectPublisher objectPublisher) {
        publisher = objectPublisher;
        Mesh  = new Cell[Constants.WIDTH, Constants.HEIGHT];
    }


    public void AddBall(BallType type, int col)
    {
        int freeRow = GetFreeRow(col);
        if (freeRow >= 0)
        {      //START_POSITION.y + (freeRow * MESH_SIZE)
            var position = new Vector3(Constants.START_POSITION.x + 
                (Constants.MESH_SIZE * col), Constants.START_HIGH , Constants.START_POSITION.z);
            var ball = publisher.CreateBall(type,position);
            var cell = new Cell(ball,type);
            Mesh[col, freeRow] = cell;
        }
    }
    public void Swap() {

    }
    public void DestroyBall(Cell cell) {
        DestroyBall(cell.TargetX, cell.TargetY);
         //Object.Destroy(cell.ball);
         //Mesh[cell.TargetX, cell.TargetX] = null;
        
    }
    public void DestroyBall(int col, int row) {
        if (Mesh[col, row] != null) {
            Object.Destroy(Mesh[col, row].ball);
        }
        Mesh[col, row] = null;
    }
    public void CreateRandomBall(int col, int row) {
        BallType type = GetRandomBall();
        var ball = publisher.CreateBall(type, AbsolutePos(col, row));
        Mesh[col, row] = new Cell(ball,type);
    }
    void CreateBooster(int col,int row,BallType type) {
        var ball = publisher.CreateBall(type, AbsolutePos(col, row));
        Mesh[col, row] = new Cell(ball, type);
        Mesh[col, row].isBooster = true; 
    }

    private int GetFreeRow(int col)
    {
        for (int i = 0; i < Constants.HEIGHT; i++)
        {
            if (Mesh[col, i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    public Vector3 AbsolutePos(Cell cell) {
        return new Vector3(Constants.MESH_SIZE * cell.TargetX, Constants.MESH_SIZE * cell.TargetY, Constants.START_POSITION.z);
    }
    private Vector3 AbsolutePos(int col, int row) {
        return new Vector3(Constants.MESH_SIZE * col, Constants.MESH_SIZE * row, Constants.START_POSITION.z);
    }
    private BallType GetRandomBall() {
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
}
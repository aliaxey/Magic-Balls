
using UnityEngine;

public class GameplayManager:IGameplayManager
{
    private IMeshManager meshManager;
    public IState state { get; set; }
    public bool InverseGravity { get; }
    public bool isStability;
    public bool haveEmptyCells;

    public GameplayManager(IMeshManager manager)
    {
        meshManager = manager;
        state = new UpdatingState(this);
        isStability = false;
        InverseGravity = false;
        haveEmptyCells = true;
    }
    public void GravityUpdate() {
        for (int col = 0; col < Constants.WIDTH; col++) {
            for (int row = 0; row < Constants.HEIGHT - 1; row++) {
                if (meshManager.Mesh[col, row] == null && meshManager.Mesh[col,row + 1]!=null) {
                    meshManager.Mesh[col, row] = meshManager.Mesh[col, row + 1];
                    meshManager.Mesh[col, row + 1] = null;
                    meshManager.Mesh[col, row].Update(col, row);
                    
                }
            }
        }
    }
    public void FillStartRow(int row) {
        for (int col = 0; col < Constants.WIDTH; col++) {
            if (meshManager.Mesh[col, row] == null) {
                meshManager.CreateRandomBall(col, row);
            }
        }
        GravityUpdate();
    }
    public bool HaveMatch() {
        for (int col = 0; col < Constants.WIDTH; col++) {
            
        }
    }
    public void FrameUpdate() {
        state.OnUpdate(); 
    }
    public void PositionUpdate() {
        isStability = true;
        haveEmptyCells = false;
        float delta = Time.deltaTime;
        for (int col = 0; col < Constants.WIDTH; col++) {           //for getting position
            for (int row = 0; row < Constants.HEIGHT; row++) {
                Cell cell = meshManager.Mesh[col, row];
                if (cell != null) {                  //check not null balls, what falls
                    if (cell.needUpdate) {
                        if (!cell.ready) {
                            cell.Update(col, row);
                        }
                        cell.Speed += Constants.GRAVIY * delta;
                        var position = cell.ball.transform.position;
                        cell.ball.transform.position = Vector3.Lerp(position, meshManager.AbsolutePos(cell), cell.Speed);
                        if (cell.ball.transform.position.Equals(meshManager.AbsolutePos(cell))) {
                            cell.needUpdate = false;
                        } else {
                            isStability = false;
                        }
                    }
                } else {
                    haveEmptyCells = true;
                }
            }
        }
    }

 
    public void CellClick(int x, int y) {
        state.OnCellSelect(x, y);
    }
    
}
/*Debug.Log($"{x}  {y}");
 *         foreach (Cell cell in meshManager.Mesh) {

            if(cell != null) {
                var position = cell.ball.transform.position;
                cell.ball.transform.position = new Vector3(position.x, position.y - delta, position.z);
            }
        }
   public void oFrameUpdate(){                                    
        float delta = Time.deltaTime;
        for (int col = 0; col < Constants.WIDTH; col++) {           //for getting position
            for (int row = 0; row < Constants.HEIGHT; row++) {
                Cell cell = meshManager.Mesh[col, row];
                if (cell != null && cell.needUpdate) {                  //check not null balls, what falls
                    var position = cell.ball.transform.position;
                    var height = row * Constants.MESH_SIZE;         //final height
                    cell.Speed += Constants.GRAVIY * delta;
                    if(position.y - cell.Speed*delta <= height) {  //finish fall
                        cell.ball.transform.position = new Vector3(position.x, height, position.z);
                        cell.needUpdate = false;
                    } else {
                        cell.ball.transform.position = new Vector3(position.x,position.y - (float)(cell.Speed * delta), position.z);
                    }
                }
            }
        }
    }
        
     
    public void FillMesh(int row) {
        bool needMore;
        do {
            needMore = false;
            for (int col = 0; col < Constants.WIDTH; col++) {         
                if (meshManager.Mesh[col, row] == null) {
                   meshManager.CreateRandomBall(col, row);
                    
                    needMore = true; 
                } 
            }
            GravityUpdate(); Debug.Log("upd " + needMore);
        } while (needMore);
    }
    
     
    public void MapUpdate() {
        for (int col = 0; col < Constants.WIDTH; col++) {           //for getting position
            for (int row = 0; row < Constants.HEIGHT; row++) {
                Cell cell = meshManager.Mesh[col, row];
                if (cell != null) {                  //check not null balls
                    cell.Update(col, row);
                }
            }
        }
    }
     */

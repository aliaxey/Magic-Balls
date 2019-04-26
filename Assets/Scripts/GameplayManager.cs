
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager:IGameplayManager
{
    private IMeshManager meshManager;
    public IState state { get; set; }
    public bool InverseGravity { get; set; }
    public bool isStability;
    public bool haveEmptyCells;
    public int score;

    public GameplayManager(IMeshManager manager)
    {
        meshManager = manager;
        state = new UpdatingState(this);
        isStability = false;
        InverseGravity = false;
        haveEmptyCells = true;
        score = 0;
    }
    public void GravityUpdate() {
        MapUpdate();
        if (InverseGravity) {
           InverseGravityUpdate();
        } else {
            NormalGravityUpdate();
        }
        MapUpdate();
    }
    private void NormalGravityUpdate() {
        for (int col = 0; col < Constants.WIDTH; col++) {
            for (int row = 0; row < Constants.HEIGHT - 1; row++) {
                if (meshManager.Mesh[col, row] == null && meshManager.Mesh[col, row + 1] != null) {
                    meshManager.Mesh[col, row] = meshManager.Mesh[col, row + 1];
                    meshManager.Mesh[col, row + 1] = null;
                    meshManager.Mesh[col, row].Update(col, row);
                    isStability = false;
                    haveEmptyCells = true;
                }
            }
        }
    }
    private void InverseGravityUpdate() {
        for (int col = 0; col < Constants.WIDTH; col++) {
            for (int row = Constants.HEIGHT - 1; row > 0 ; row--) {
                if (meshManager.Mesh[col, row] == null && meshManager.Mesh[col, row - 1] != null) {
                    meshManager.Mesh[col, row] = meshManager.Mesh[col, row - 1];
                    meshManager.Mesh[col, row - 1] = null;
                    meshManager.Mesh[col, row].Update(col, row);
                    isStability = false;
                    haveEmptyCells = true;
                }
            }
        }
    }
    public void FillStartRow() {
        int row = GetStartRow(); 
        for (int col = 0; col < Constants.WIDTH; col++) {
            if (meshManager.Mesh[col, row] == null) {
                meshManager.CreateRandomBall(col, row);
                isStability = false;
            }
        }
        GravityUpdate();
    }
    public void SwapCells(int col, int row, int destCol,int destRow) {
        Cell tmp = meshManager.Mesh[destCol, destRow];
        meshManager.Mesh[destCol, destRow] = meshManager.Mesh[col, row];
        meshManager.Mesh[col, row] = tmp;
        MapUpdate();
    }
    public void RemoveCell(int col,int row) {
        meshManager.DestroyBall(col,row);
        
    }
    public void RemoveCells(IList<Cell> list) {
        foreach (Cell cell in list) {
            meshManager.DestroyBall(cell);
        }
        haveEmptyCells = true;
        GravityUpdate();
        score += list.Count;
        if (list.Count >= 4) {
            Cell pattern = list[Random.Range(0, list.Count)];
            meshManager.CreateBooster(pattern.TargetX, pattern.TargetY, pattern.ballType); 
        }
        CheckGravityChange(list);
        Debug.Log($"Boom! Destroyed {list.Count} balls!\nScore: {score}"); 
    }
    public IList<Cell> Match() {
        IList< Cell > list = new List<Cell>();
        for (int row = 0; row < Constants.HEIGHT; row++) {
            list.Clear();
            list.Add(meshManager.Mesh[0, row]);
            for (int col = 1; col < Constants.WIDTH; col++) {
                var sample = list[0];
                var current = meshManager.Mesh[col, row];
                if (sample != null) {
                    if (current != null && sample.ballType == current.ballType) {  //continue math
                        list.Add(current);
                    } else {
                        if(list.Count >= Constants.MATCH_COUNT) {
                            MapUpdate();
                            return list;
                        }
                        list.Clear();
                        list.Add(current);
                    }
                    //if (list.Count >= Constants.MATCH_COUNT) {
                    //    return list;
                    //}
                } else {
                    if(list.Count >= Constants.MATCH_COUNT) {
                        MapUpdate();
                        return list;
                    }
                    list.Clear();
                    list.Add(current);
                }
            }
            if (list.Count >= Constants.MATCH_COUNT) {
                MapUpdate();
                return list;
            }
        }
        list.Clear();
        for (int col = 0; col < Constants.WIDTH; col++) {
            list.Clear();
            list.Add(meshManager.Mesh[col, 0]);
            for(int row = 1; row <Constants.HEIGHT; row++) {
                var sample = list[0];
                var current = meshManager.Mesh[col, row];
                if (sample != null) {
                    if(current!=null && sample.ballType == current.ballType) {  //continue math
                        list.Add(current);
                    } else {
                        if(list.Count >= Constants.MATCH_COUNT) {
                            MapUpdate();
                            return list;
                        }
                        list.Clear();
                        list.Add(current);
                    }
                    
                } else {
                    if (list.Count >= Constants.MATCH_COUNT) {
                        MapUpdate();
                        return list;
                    }
                    list.Clear();
                    list.Add(current);
                }
            }
            if (list.Count >= Constants.MATCH_COUNT) {
                MapUpdate();
                return list;
            }
        }
        return null;
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
    private void CheckGravityChange(IList<Cell> list) {
        foreach(Cell cell in list) {
            if (cell.isBooster) {
                InverseGravity = !InverseGravity;
                return;
            }
        }
    }
 
    public void CellClick(int x, int y) {
        state.OnCellSelect(x, y);
        //Debug.Log($"click {x}   {y} ------- {meshManager.Mesh[x,y].ballType}");
    }
    public void MapUpdate() {
        for (int col = 0; col < Constants.WIDTH; col++) {           //for getting position
            for (int row = 0; row < Constants.HEIGHT; row++) {
                Cell cell = meshManager.Mesh[col, row];
                if (cell != null) {                  //check not null balls
                    cell.Update(col, row);
                } else {
                    isStability = false;
                }
            }
        }
        
    }
    private int GetStartRow() {
        if (InverseGravity) {
            return 0;
        } else {
            return Constants.HEIGHT - 1;
        }
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
    
     
    
     */

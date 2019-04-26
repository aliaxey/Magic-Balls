
using UnityEngine;

public class Cell{
    public GameObject ball { get; set; }
    public bool needUpdate { get; set; }
    public bool ready { get; set; }
    public int TargetX { get; set; }
    public int TargetY { get; set; }
    public float Speed { get; set; }
    private TochHandler handler;
    public BallType ballType;
    public bool isBooster;

    public Cell(GameObject ball,BallType type) {
        this.ball = ball;
        ballType = type;
        handler = ball.GetComponent<TochHandler>();
        needUpdate = true;
        ready = false; 
        Speed = 0;
        isBooster = false;
    }
    public void Update(int col,int row) {
        TargetX = col;
        TargetY = row;
        handler.X = col;
        handler.Y = row;
        ready = true;
        needUpdate = true;
    }
    
}
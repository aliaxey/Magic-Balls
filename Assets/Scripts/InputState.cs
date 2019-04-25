using UnityEngine;
public class InputState : IState {
    private GameplayManager gameplayManager;
    private bool cellSelected;
    private int tmpCol;
    private int tmpRow;
    public InputState(GameplayManager gameplayManager) {
        this.gameplayManager = gameplayManager;
        cellSelected = false; 
    }

    public void OnCellSelect(int col, int row) {
        if (cellSelected) {
            if(Mathf.Abs(tmpCol - col)+Mathf.Abs(tmpRow - row) == 1) {
                Debug.Log("do it!!!!!!!!!!!!!!!!!");
            } else {
                tmpCol = col;
                tmpRow = row;
            }
        } else {
            tmpCol = col;
            tmpRow = row;
            cellSelected = true; 
        }
    }

    public void OnUpdate() {
    }
}
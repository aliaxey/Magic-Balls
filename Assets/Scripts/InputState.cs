using UnityEngine;
public class InputState : IState {
    private IGameplayManager gameplayManager;
    private bool cellSelected;
    private int tmpCol;
    private int tmpRow;
    private Behaviour effect;
    public InputState(IGameplayManager gameplayManager) {
        this.gameplayManager = gameplayManager;
        cellSelected = false; 
    }

    public void OnCellSelect(int col, int row) {
        //gameplayManager.RemoveCells(col, row);
        if (cellSelected) {
            if(Mathf.Abs(tmpCol - col)+Mathf.Abs(tmpRow - row) == 1) {
                effect.enabled = false;
                gameplayManager.state = new SwapState(gameplayManager, tmpCol, tmpRow, col, row);
            } else {
                effect.enabled = false;
                tmpCol = col;
                tmpRow = row;
                cellSelected = false; 
            }
        } else {
            if (effect != null) {
                effect.enabled = false;
            }
            tmpCol = col;
            tmpRow = row;
            effect = gameplayManager.GetCell(col, row).ball.GetComponent("Halo") as Behaviour;
            effect.enabled = true;
            cellSelected = true; 
        }
    }

    public void OnUpdate() {
    }
}
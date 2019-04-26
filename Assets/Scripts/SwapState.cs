public class SwapState : IState {
    private GameplayManager gameplayManager;
    private int tmpCol;
    private int tmpRow;
    private int col;
    private int row;

    public SwapState(GameplayManager gameplayManager, int tmpCol, int tmpRow, int col, int row) {
        this.gameplayManager = gameplayManager;
        this.tmpCol = tmpCol;
        this.tmpRow = tmpRow;
        this.col = col;
        this.row = row;
        gameplayManager.SwapCells(col, row, tmpCol, tmpRow); 
    }

    public void OnCellSelect(int col, int row) {
        
    }

    public void OnUpdate() {
        if (gameplayManager.isStability) {
            var matches = gameplayManager.Match();
            if (matches!= null) {
                gameplayManager.RemoveCells(matches);
                gameplayManager.state = new UpdatingState(gameplayManager);
            } else {
                gameplayManager.SwapCells(col, row, tmpCol, tmpRow);
                gameplayManager.state = new UpdatingState(gameplayManager);
            }
        }
    }
}
using UnityEngine;
public class UpdatingState : IState {
    private GameplayManager gameplayManager;
    public UpdatingState(GameplayManager gameplayManager) {
        this.gameplayManager = gameplayManager;
    }

    public void OnCellSelect(int col, int row) {
        
    }

    public void OnUpdate() {
        Debug.Log($"<<<<<<{gameplayManager.isStability}]]]]]]]]]][[[[[[{gameplayManager.haveEmptyCells}");
        gameplayManager.PositionUpdate();
        gameplayManager.GravityUpdate();
        if (gameplayManager.isStability) {
            if (gameplayManager.haveEmptyCells) {
                gameplayManager.FillStartRow(Constants.HEIGHT - 1);
            } else {
                gameplayManager.state = new InputState(gameplayManager);
            }
        }
    }
}
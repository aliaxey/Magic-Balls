using System.Collections.Generic;
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
            IList<Cell> list = gameplayManager.Match();
            while(list != null) {
                gameplayManager.GravityUpdate();
                gameplayManager.RemoveCells(list);
                gameplayManager.GravityUpdate();
                list = gameplayManager.Match();
            }
            if (gameplayManager.haveEmptyCells) {
                gameplayManager.GravityUpdate();
                gameplayManager.FillStartRow(Constants.HEIGHT - 1);
                gameplayManager.GravityUpdate();
            } else  {
                if(list == null) {
                    gameplayManager.state = new InputState(gameplayManager);
                }
            }
        }
    }
}
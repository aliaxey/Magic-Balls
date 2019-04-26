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
        //Debug.Log($"<<<<<<{gameplayManager.isStability}]]]]]]]]]][[[[[[{gameplayManager.haveEmptyCells}");
        gameplayManager.GravityUpdate();
        gameplayManager.MapUpdate();
        gameplayManager.PositionUpdate();
        IList<Cell> list;
        if (gameplayManager.isStability) {
            list = gameplayManager.Match();
            while(list != null) {
                gameplayManager.GravityUpdate();
                gameplayManager.RemoveCells(list);
                list = gameplayManager.Match();
                gameplayManager.GravityUpdate();
            }
            
            if (gameplayManager.haveEmptyCells) {
                gameplayManager.GravityUpdate();
                gameplayManager.FillStartRow(Constants.HEIGHT - 1);
                gameplayManager.GravityUpdate();
            } else  {
                list = gameplayManager.Match();
                if(list == null&&!gameplayManager.haveEmptyCells) {
                    gameplayManager.state = new InputState(gameplayManager);
                }
            }
        }
    }
}
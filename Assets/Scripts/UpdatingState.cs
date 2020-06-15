using System.Collections.Generic;
using UnityEngine;
public class UpdatingState : IState {
    private IGameplayManager gameplayManager;
    private IList<Cell> list;

    public UpdatingState(IGameplayManager gameplayManager) {
        this.gameplayManager = gameplayManager;
    }

    public void OnCellSelect(int col, int row) {
        
    }

    public void OnUpdate() {
        gameplayManager.PositionUpdate();
        gameplayManager.GravityUpdate();
        if (gameplayManager.isStability) {
            //gameplayManager.GravityUpdate();
            list = gameplayManager.Match();
            while (list != null) {
                gameplayManager.RemoveCells(list);
                //gameplayManager.GravityUpdate();                           --
                list = gameplayManager.Match();
                //gameplayManager.GravityUpdate();
            }

            if (gameplayManager.haveEmptyCells) {
                //gameplayManager.GravityUpdate();
                gameplayManager.FillStartRow();
                gameplayManager.GravityUpdate();                         
            } else {
                list = gameplayManager.Match();
                if (list == null && !gameplayManager.haveEmptyCells) {
                    gameplayManager.state = new InputState(gameplayManager);
                }
            }
        }
    }

    public void OOnUpdate() {
        //Debug.Log($"<<<<<<{gameplayManager.isStability}]]]]]]]]]][[[[[[{gameplayManager.haveEmptyCells}");
        gameplayManager.GravityUpdate();
        gameplayManager.PositionUpdate();
        IList<Cell> list;
        if (gameplayManager.isStability) {
            //gameplayManager.GravityUpdate();                                     ---
            list = gameplayManager.Match();
            while(list != null) {
                gameplayManager.RemoveCells(list);
                //gameplayManager.GravityUpdate();                                   ---
                list = gameplayManager.Match();
                //gameplayManager.GravityUpdate();
            }
            
            if (gameplayManager.haveEmptyCells) {
                //gameplayManager.GravityUpdate();
                gameplayManager.FillStartRow();
                //gameplayManager.GravityUpdate();                    --
            } else  {
                list = gameplayManager.Match();
                if(list == null&&!gameplayManager.haveEmptyCells) {
                    gameplayManager.state = new InputState(gameplayManager);
                }
            }
        }
    }
}
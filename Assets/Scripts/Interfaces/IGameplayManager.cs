using System.Collections.Generic;

public interface IGameplayManager:IUpdatable, IInputSubscriber
{
    void RemoveCells(IList<Cell> list);
    void GravityUpdate();
    IList<Cell> Match();
    void FillStartRow();
    void PositionUpdate();
    void SwapCells(int col, int row, int tmpCol, int tmpRow);
    bool isStability { get; }
    bool haveEmptyCells { get; }
    int score { get; set; }
    IState state { get; set; }
    Cell GetCell(int x, int y);

}
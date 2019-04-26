using System.Collections.Generic;

public interface IGameplayManager:IUpdatable, IInputSubscriber
{
    void RemoveCells(IList<Cell> list);
    //void FillMesh(int height);
}
public interface IState {
    void OnUpdate();
    void OnCellSelect(int col, int row);
}
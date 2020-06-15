using System;
[Serializable]
public class SavedCell {
    public int ballType;
    public bool isBooster;

    public SavedCell(Cell cell) {
        ballType = (int)cell.ballType;
        isBooster = cell.isBooster;
    }
}
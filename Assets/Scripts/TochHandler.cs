using UnityEngine;
public class TochHandler:MonoBehaviour {
    public IInputMamager inputMamager { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    private void OnMouseDown() {
        if (inputMamager != null) {
            inputMamager.CellTouch(X, Y);
        }
    }
}
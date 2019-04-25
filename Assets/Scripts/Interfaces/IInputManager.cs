using UnityEngine;
using UnityEditor;

public interface IInputMamager {
    void CellTouch(int x, int y);
    void AddSubscriber(IInputSubscriber subscriber);
    void RemoveSubscriber(IInputSubscriber subscriber);
}
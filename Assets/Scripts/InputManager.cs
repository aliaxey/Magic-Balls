using System.Collections.Generic;
public class InputManager : IInputMamager {
    private IList<IInputSubscriber> subscribers = new List<IInputSubscriber>();

    public InputManager() {

    }
    public void CellTouch(int x, int y) {
        foreach(IInputSubscriber subscriber in subscribers) {
            subscriber.CellClick(x, y);
        }
    }
    public void AddSubscriber(IInputSubscriber subscriber) {
        subscribers.Add(subscriber);
    }

    public void RemoveSubscriber(IInputSubscriber subscriber) {
        subscribers.Remove(subscriber);
    }
}
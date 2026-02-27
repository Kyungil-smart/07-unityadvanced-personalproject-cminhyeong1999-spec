using UnityEngine;

public interface IListener
{
    public void OnEvent(EventType eventType, Component sender, Object param = null);
}

using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GameEvents 
{
    private static List<Action<GameEvent>> subscribers = new List<Action<GameEvent>>();
    public static void Subscribe(Action<GameEvent> action) {
        subscribers.Add(action);
    }

    public static void UnSubscribe(Action<GameEvent> action)
    {
        subscribers.Remove(action);
    }

    public static void EmitEvent(GameEvent e) {
        foreach (var action in subscribers)//перебор коллекции подписок 
        {
            action.Invoke(e);
        }
    }
    
}

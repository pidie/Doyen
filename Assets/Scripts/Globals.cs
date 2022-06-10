using System;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    private static readonly Dictionary<string, KeyCode> KeyBindings = new ()
    {
        { "Interact",   KeyCode.E },
        { "Inventory",  KeyCode.I },
        { "MainMenu" ,  KeyCode.M }
    };

    public static KeyCode GetKeyBinding(string keycodeKey)
    {
        if (KeyBindings.ContainsKey(keycodeKey))
            return KeyBindings[keycodeKey];

        throw new Exception($"KeyCode {keycodeKey} does not exist in the current context");
    }
}

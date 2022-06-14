using System;
using System.Collections.Generic;
using System.Globalization;
using Humanizer;
using UnityEngine;

public static class Globals
{
    private static string _globalRegion = "en-US";
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

    public static string TitleCase(string phrase)
    {
        var words = new CultureInfo(_globalRegion, false).TextInfo;
        phrase = phrase.Humanize(LetterCasing.LowerCase);
        return words.ToTitleCase(phrase);
    }
}

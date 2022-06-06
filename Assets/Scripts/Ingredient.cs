using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Ingredients", fileName = "New Ingredient")]
public class Ingredient : ScriptableObject
{
    public new string name;
    public Texture icon;
    
    [Header("Default Properties")]
    public IngredientProperty purity = new ("Purity", true, 0, 1, 0.75f);
    public IngredientProperty lifeSpan = new("LifeSpan", true, 0, 1000, 950);

    [Header("Additional Properties")]
    public IngredientProperty[] properties;
}
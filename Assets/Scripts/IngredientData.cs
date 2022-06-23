using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Collectable/Ingredient", fileName = "New Ingredient")]
public class IngredientData : ScriptableObject
{
    public new string name;
    public Texture icon;
    [Range(5, 600)]
    public int timeToRegrow;
    
    [Header("Default Properties")]
    public IngredientProperty purity = new ("Purity", true, 0, 1, 0.75f);
    public IngredientProperty lifeSpan = new("LifeSpan", true, 0, 1000, 950);

    [Header("Additional Properties")]
    public IngredientProperty[] properties;
}
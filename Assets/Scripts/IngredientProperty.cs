using System;

[Serializable]
public struct IngredientProperty
{
	public string name;
	public bool enabled;
	public float minimum;
	public float maximum;
	public float value;

	public IngredientProperty(string name = "", bool enabled = false, float minimum = 0f, float maximum = 0f, float value = 0f)
	{
		this.name = name;
		this.enabled = enabled;
		this.minimum = minimum;
		this.maximum = maximum;
		this.value = value;
	}
}

[Serializable]
public struct IngredientPropertyValue
{
	public string name;
	public float value;

	public IngredientPropertyValue(string name, float value)
	{
		this.name = name;
		this.value = value;
	}

	public IngredientPropertyValue(IngredientProperty property)
	{
		name = property.name;
		value = property.maximum;
	}
}
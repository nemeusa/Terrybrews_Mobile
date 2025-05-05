using System.Collections.Generic;


[System.Serializable]
public class DrinkName
{
    public List<DrinkType> drinks = new List<DrinkType>();
}

public enum DrinkType
{
    Water,
    Coca
}
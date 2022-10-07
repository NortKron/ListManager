using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ListStructure
{
    public List<ListFromJson> Lists;

    public ListStructure(List<ListFromJson> Lists)
    {
        this.Lists = Lists;
    }
}

[Serializable]
public class ListFromJson
{
    public string Name;
    public List<string> Elements;

    public ListFromJson (string Name, List<string> Elements)
    {
        this.Name = Name;
        this.Elements = Elements;
    }
}
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    private string pathDefault;

    public ListNonSorting   listNonSorting;
    public ListSorting      listSorting;

    private bool isSortingNumByAsc;
    private bool isSortingStringByAsc;

    public void OnLoadFile()
    {
        var path = EditorUtility.OpenFilePanel("Open Json", pathDefault, "Json");

        if (string.IsNullOrEmpty(path))
            return;

        string fileContents = File.ReadAllText(path);
        ListStructure lists = JsonUtility.FromJson<ListStructure>(fileContents);

        listNonSorting.SetList(lists.Lists[0]);
        listSorting.SetList(lists.Lists[1]);
    }

    public void OnSaveFile()
    {
        var path = EditorUtility.SaveFilePanel("Save Json", pathDefault, "lists.json", "Json");

        if (string.IsNullOrEmpty(path))
            return;

        List<ListFromJson> listFromJson = new();
        listFromJson.Add(listNonSorting.GetList());
        listFromJson.Add(listSorting.GetList());

        ListStructure lists = new ListStructure(listFromJson);
        string fileContents = JsonUtility.ToJson(lists);

        File.WriteAllText(path, fileContents);
    }

    public void OnToggleNum(bool _isSortingNumByAsc)
    {
        isSortingNumByAsc = _isSortingNumByAsc;
        listSorting.SotringList(isSortingStringByAsc, isSortingNumByAsc);
    } 
    
    public void OnToggleStr(bool _isSortingStringByAsc)
    {
        isSortingStringByAsc = _isSortingStringByAsc;
        listSorting.SotringList(isSortingStringByAsc, isSortingNumByAsc);
    }

    void Start()
    {
        isSortingStringByAsc = true;
        isSortingNumByAsc = true;

        pathDefault = Application.dataPath + "/JSON Files/";
    }
}

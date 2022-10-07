using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    /*
    public GameObject panelListLeft;
    public GameObject panelListRight;

    public GameObject prefabItem;
    */
    public ListNonSorting   listNonSorting;
    public ListSorting      listSorting;

    private bool isSortingByNum;
    private bool isSortingByString;

    public void OnLoadFile()
    {
        var path = EditorUtility.OpenFilePanel("Open Json", "", "Json");

        if (string.IsNullOrEmpty(path))
            return;

        Debug.Log(path);

        //Read
        string fileContents = File.ReadAllText(path);
        //var reader = new StreamReader(path);
        //Debug.Log("Contents:" + fileContents);

        ListStructure lists = JsonUtility.FromJson<ListStructure>(fileContents);
        //JsonConvert.DeserializeObject<ListStructure>(reader);

        //Debug.Log("Name = " + lists.Lists[0].Name);

        listNonSorting.SetList(lists.Lists[0]);
        listSorting.SetList(lists.Lists[1]);
    }

    public void OnSaveFile()
    {
        var path = EditorUtility.SaveFilePanel("Save Json", "", "lists.json", "Json");

        if (string.IsNullOrEmpty(path))
            return;

        Debug.Log(path);

    }

    public void OnToggleNum(bool N)
    {
        Debug.Log("OnToggleNum:" + N);
        isSortingByNum = N;

        listSorting.SotringList(isSortingByNum, isSortingByString);
    } 
    
    public void OnToggleStr(bool S)
    {
        Debug.Log("OnToggleStr");
        isSortingByString = S;

        listSorting.SotringList(isSortingByNum, isSortingByString);
    }

    // Start is called before the first frame update
    void Start()
    {
        isSortingByNum = false;
        isSortingByString = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

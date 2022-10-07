using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ListNonSorting : MonoBehaviour, IDropHandler
{
    string nameList;

    public GameObject panelList;
    public ItemList prefabItem;
    public GameObject textListName;

    protected List<ItemList> itemList;

    public void SetList(ListFromJson listItems)
    {
        this.ClearList();
        nameList = listItems.Name;
        textListName.GetComponent<Text>().text = "Name: " + nameList + "; Count: " + listItems.Elements.Count;

        foreach (var element in listItems.Elements)
        {
            var item = Instantiate(prefabItem, panelList.transform);
            item.SetParentObject(transform.gameObject);
            item.SetString(element);
            
            itemList.Add(item);
        }
    }

    public ListFromJson GetList()
    {
        if (itemList.Count == 0)
            return null;

        List<string> listLabels = new();

        foreach(var item in itemList)
        {
            listLabels.Add(item.value);
        }

        ListFromJson listFromJson = new ListFromJson(nameList, listLabels);
        return listFromJson;
    }

    protected void ClearList()
    {
        if (itemList.Count > 0) 
        {
            foreach (var item in itemList)
            {
                Destroy(item.gameObject);
            }

            itemList.Clear();
        }
    }

    public void RefreshList()
    {
        itemList.Clear();

        foreach (Transform item in panelList.transform)
        {
            ItemList newItem = item.GetComponent<ItemList>();
            newItem.SetParentObject(transform.gameObject);

            itemList.Add(newItem);
        }

        textListName.GetComponent<Text>().text = "Name: " + nameList + "; Count: " + itemList.Count;
    }

    public void OnDrop(PointerEventData eventData)
    {
        EventSystem.current.currentSelectedGameObject.transform.parent = panelList.transform;
        var sizey = panelList.transform.GetComponent<RectTransform>().rect.size.y;
        int index = (int)((((panelList.transform.position.y + (sizey / 2)) - Input.mousePosition.y)) / 35) + 1;

        if (index <= 0)
        {
            index = 1;
        }

        EventSystem.current.currentSelectedGameObject.transform.SetSiblingIndex(index);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

        this.RefreshList();
    }
    
    void Start()
    {
        itemList = new();
    }
}

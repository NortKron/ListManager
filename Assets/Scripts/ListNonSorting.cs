using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ListNonSorting : MonoBehaviour, IDropHandler
{
    string nameList;

    public GameObject panelList;
    public ItemList prefabItem;
    public GameObject textListName;

    private List<ItemList> itemList;

    public void SetList(ListFromJson listItems)
    {
        this.ClearList();
        nameList = listItems.Name;
        textListName.GetComponent<TMPro.TextMeshProUGUI>().text = "Name: " + nameList + "; Count: " + listItems.Elements.Count;

        foreach (var element in listItems.Elements)
        {
            var item = Instantiate(prefabItem);

            itemList.Add(item);
            item.index = listItems.Elements.Count - 1;
            item.parentObject = transform.gameObject;
            item.SetString(element);
            item.transform.SetParent(panelList.transform);
        }
    }

    private void ClearList()
    {
        if (itemList.Count > 0) 
        {
            Debug.Log("Clear list");

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
            newItem.index = itemList.Count;
            newItem.parentObject = transform.gameObject;

            itemList.Add(newItem);
        }

        textListName.GetComponent<TMPro.TextMeshProUGUI>().text = "Name: " + nameList + "; Count: " + itemList.Count;
        //Debug.Log("Count : " + itemList.Count);
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject);

        EventSystem.current.currentSelectedGameObject.transform.parent = panelList.transform;
        var sizey = panelList.transform.GetComponent<RectTransform>().rect.size.y;
        int index = (int)((((panelList.transform.position.y + (sizey / 2)) - Input.mousePosition.y)) / 35) + 1;

        if (index <= 0)
        {
            index = 1;
        }
        //Debug.Log(index);
        EventSystem.current.currentSelectedGameObject.transform.SetSiblingIndex(index);

        //Debug.Log("drop " + gameObject.name);

        // rebuilds the layout and its child elements (previously done in UIDrag)
        // the objects that allow drop are the ones who actually need this rebuild
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

        this.RefreshList();
    }
    
    void Start()
    {
        itemList = new();
    }
}

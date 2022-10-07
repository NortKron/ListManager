using System.Collections.Generic;
using System.Linq;

public class ListSorting : ListNonSorting
{
    public void SotringList(bool ByString, bool ByNum)
    {
        List<ItemList> resultList;
        IOrderedEnumerable<ItemList> tempList;

        if (ByString)
            tempList = itemList.OrderBy(n => n.str);
        else
            tempList = itemList.OrderByDescending(n => n.str);

        if (ByNum)
            resultList = tempList.ThenBy(n => n.num).ToList();
        else
            resultList = tempList.ThenByDescending(n => n.num).ToList();

        this.ClearList();

        foreach (ItemList item in resultList)
        {
            var newItem = Instantiate(prefabItem, panelList.transform);
            newItem.SetCopy(item);
            newItem.SetParentObject(transform.gameObject);

            itemList.Add(newItem);
        }
    }
}

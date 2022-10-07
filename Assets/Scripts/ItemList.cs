using UnityEngine;
using UnityEngine.EventSystems;

public class ItemList : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField]
    private GameObject textField;

    Vector2 startPosition;
    Vector2 diffPosition;
    GameObject canvas;
    GameObject parentObject;

    public string value { get; set; }
    public int num { get; set; }
    public string str { get; set; }

    public void SetString(string value)
    {
        this.value = value;

        string[] words = value.Split(" ");
        this.str = words[0];
        this.num = int.Parse(words[1]);

        textField.GetComponent<TMPro.TextMeshProUGUI>().text = value;
    }
    
    public void SetCopy(ItemList itemList)
    {
        this.value = itemList.value;
        this.str = itemList.str;
        this.num = itemList.num;

        textField.GetComponent<TMPro.TextMeshProUGUI>().text = this.value;
    }

    public void SetParentObject(GameObject gameObject)
    {
        parentObject = gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector2)Input.mousePosition - diffPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosition = transform.position;
        diffPosition = (Vector2)Input.mousePosition - startPosition;

        EventSystem.current.SetSelectedGameObject(gameObject);
        EventSystem.current.currentSelectedGameObject.transform.SetParent(canvas.transform);
        EventSystem.current.currentSelectedGameObject.transform.SetAsFirstSibling();

        parentObject.SendMessage("RefreshList");
    }

    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemList : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    Vector2 startPosition;
    Vector2 diffPosition;
    GameObject canvas;

    public GameObject textField;
    public GameObject parentObject;

    public int num { get; set; }
    public string str { get; set; }
    public int index;

    public void SetString(string str)
    {
        string[] words = str.Split(" ");
        this.str = words[0];
        this.num = int.Parse(words[1]);

        textField.GetComponent<TMPro.TextMeshProUGUI>().text = str;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        //Debug.Log("OnBeginDrag");

        startPosition = transform.position;
        //startPositionY = transform.position.y;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        //var position = (Vector2)Input.mousePosition + (Vector2.up * verticalOffset) + (Vector2.left * horizontalOffset);
        //transform.position = position;

        transform.position = (Vector2)Input.mousePosition - diffPosition;
        //Debug.Log(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Name: " + eventData.pointerCurrentRaycast.gameObject.name);

        if (transform.parent == canvas.transform)
        {
            //transform.position = startPosition;
            transform.parent = parentObject.transform;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosition = transform.position;
        diffPosition = (Vector2)Input.mousePosition - startPosition;

        EventSystem.current.SetSelectedGameObject(gameObject);
        EventSystem.current.currentSelectedGameObject.transform.SetParent(canvas.transform);
        EventSystem.current.currentSelectedGameObject.transform.SetAsFirstSibling();

        parentObject.SendMessage("RefreshList");

        //Debug.Log("start drag " + gameObject.name);
    }

    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
}

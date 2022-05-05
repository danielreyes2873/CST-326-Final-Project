using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemInfoText;

    RectTransform rectTransform;
    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    public void SetupTooltip(itemData_SO item)
    {
        itemNameText.text = item.itemName;
        itemInfoText.text = item.description;
    }

    public void UpdatePosition()
    {
        Vector3[] corners = new Vector3[4];
        //out corners 
        rectTransform.GetWorldCorners(corners);
        
        float width = corners[3].x - corners[0].x;
        float height = corners[1].y - corners[0].y;

        Vector3 mousePos = Input.mousePosition;

        Vector3 verticalMove;
        Vector3 horizontalMove;
        if(mousePos.y < height)
            verticalMove = Vector3.up * height * 0.7f;
        else
            verticalMove = Vector3.down * height * 0.7f;
        
        if(Screen.width - mousePos.x > width)
            horizontalMove = Vector3.right * width * 0.5f;
        else
            horizontalMove = Vector3.left * width * 0.5f;

        rectTransform.position = mousePos + verticalMove + horizontalMove;
    }
}

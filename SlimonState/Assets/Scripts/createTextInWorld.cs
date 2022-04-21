using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createTextInWorld : MonoBehaviour
{
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localposition, int fontSize,Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localposition;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;


    }
}

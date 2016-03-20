using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.EventSystems;

public class ColorPicker : MonoBehaviour {
    
    [SerializeField]
    Texture2D texture2D;

    Renderer parentRenderer;

	// Use this for initialization
	void Start () {
	    gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter() {
        parentRenderer = transform.parent.GetComponent<Renderer>();
    }

    void OnMouseOver() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            parentRenderer.material.color = texture2D.GetPixel(Mathf.FloorToInt(hit.textureCoord.x * texture2D.width), Mathf.FloorToInt(hit.textureCoord.y * texture2D.height));
        }
    }

    void OnMouseDown() {
        transform.SetParent(null);
        gameObject.SetActive(false);
    }
}

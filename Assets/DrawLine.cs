using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DrawLine : MonoBehaviour
{
    public static DrawLine instance;
    public TMPro.TMP_Dropdown dropdownMenu;
    [SerializeField] GameObject linePrefab;
    private GameObject currentLine;
    private LineRenderer lineRenderer;
    [SerializeField] private List<Vector2> fingerPositions;
    private int currentPoint;
    public float currentLayer;
    public Color selectedColor;

    void Start()
    {
        instance = this;
        dropdownMenu.onValueChanged.AddListener(new UnityAction<int>(HandleInputData));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .01f)
            {
                UpdateLine(tempFingerPos);
            }
        }
    }

    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        Material newMaterial = new Material(Shader.Find("Sprites/Default"));
        newMaterial.color = selectedColor;
        lineRenderer.material = newMaterial;
        fingerPositions.Clear();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fingerPositions.Add(mousePos);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, new Vector3(mousePos.x, mousePos.y, currentLayer));
        currentPoint = 1;
        currentLayer -= 0.001f;
    }

    public void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(newFingerPos.x, newFingerPos.y, currentLayer));
    }

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            selectedColor = Color.black;
        }
        else if (val == 1)
        {
            selectedColor = Color.red;
        }
        else if (val == 2)
        {
            selectedColor = Color.green;
        }
        else if (val == 3)
        {
            selectedColor = Color.blue;
        }
        else if (val == 4)
        {
            selectedColor = Color.yellow;
        }
        else if (val == 5)
        {
            selectedColor = Color.magenta;
        }
        else if (val == 6)
        {
            selectedColor = Color.gray;
        }
    }
}
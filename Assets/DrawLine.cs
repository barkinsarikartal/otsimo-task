using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

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
    public GameObject fairySprite;
    public int flag = 1;
    public Slider sizeSlider;
    private float lineWidth = .05f;

    void Start()
    {
        instance = this;
        dropdownMenu.onValueChanged.AddListener(new UnityAction<int>(HandleInputData));
        sizeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {
        if ((flag == 1) && Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if ((flag == 1) && Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .01f)
            {
                UpdateLine(tempFingerPos);
            }
        }
        if((flag == 2) && Input.GetMouseButtonDown(0))
        {
            DrawFairy();
        }
        if ((flag == 3) && Input.GetMouseButtonDown(0))
        {
            BucketFill();
        }
        if ((flag == 4) && Input.GetMouseButtonDown(0))
        {
            CreateEraser();
        }
        if ((flag == 4) && Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .01f)
            {
                UpdateEraser(tempFingerPos);
            }
        }
    }

    private void CreateEraser()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        Material newMaterial = new Material(Shader.Find("Sprites/Default"));
        newMaterial.color = Color.white;
        lineRenderer.material = newMaterial;
        lineRenderer.startWidth = lineWidth;
        fingerPositions.Clear();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fingerPositions.Add(mousePos);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, new Vector3(mousePos.x, mousePos.y, currentLayer));
        currentPoint = 1;
        currentLayer -= 0.001f;
    }

    private void UpdateEraser(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(newFingerPos.x, newFingerPos.y, currentLayer));
    }

    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        Material newMaterial = new Material(Shader.Find("Sprites/Default"));
        newMaterial.color = selectedColor;
        lineRenderer.material = newMaterial;
        lineRenderer.startWidth = lineWidth;
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

    public void BucketFill()
    {
        GameObject objectToDelete = GameObject.Find("fairy(Clone)");
        do
        {
            if (objectToDelete != null)
            {
                Destroy(objectToDelete);
            }
            else
            {
                Debug.LogWarning("Silmek istediðiniz isme sahip bir nesne bulunamadý.");
            }
        } while (objectToDelete);
        Camera.main.backgroundColor = selectedColor;
    }

    public void PenButton()
    {
        Debug.Log("pen button pressed");
        flag = 1;
    }

    public void StampButton()
    {
        Debug.Log("stamp button pressed");
        flag = 2;
    }

    public void BucketButton()
    {
        Debug.Log("bucket button pressed");
        flag = 3;
    }

    public void EraserButton()
    {
        Debug.Log("eraser button pressed");
        flag = 4;
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

    private void OnSliderValueChanged(float value)
    {
        lineWidth = value;
    }

    public void DrawFairy()
    {
        Debug.Log("drawing fairy");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 tempFingerPos = new Vector3(mousePos.x, mousePos.y, currentLayer);
        GameObject currentStamp = Instantiate(fairySprite, tempFingerPos, Quaternion.identity);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    #region Variables
    #region Main UI
    [Header("Main UI")]
    public bool showSelectMenu;
    public bool toggleTogglable;
    public float scrW, scrH;
    #endregion

    #region Resources
    [Header("Resources")]
    public Texture2D radialTexture;
    public Texture2D slotTexture;
    [Range(0, 100)]
    public int circleScaleOffset;
    #endregion

    #region Icons
    [Header("Icons")]
    public Vector2 iconSize;
    public bool showIcons, showBoxes, showBounds;
    [Range(0.1f, 1)]
    public float iconSizeNum;
    [Range(-360, 360)]
    public int radialRotation;
    [SerializeField]
    private float iconOffset;
    #endregion

    #region Mouse Settings
    [Header("Mouse Settings")]
    public Vector2 mouse;
    public Vector2 input;
    public Vector2 circleCenter;
    #endregion

    #region Input Settings
    [Header("Input Settings")]
    public float inputDist;
    public float inputAngle;
    public int keyIndex;
    public int mouseIndex;
    public int inputIndex;
    #endregion

    #region Sector Settings
    [Header("Sector Settings")]
    public Vector2[] slotPos;
    public Vector2[] boundsPos;
    [Range(1, 8)]
    public int numOfSectors = 1;
    [Range(50, 300)]
    public float circleRadius = 50;
    public float mouseDistance, sectorDegree, mouseAngles;
    public int sectorIndex;
    public bool withinCircle;
    #endregion

    #region Misc
    [Header("Misc")]
    private Rect debugWindow;
    #endregion
    #endregion

    void Start()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;

        circleCenter.x = Screen.width / 2;
        circleCenter.y = Screen.height / 2;

        debugWindow = new Rect(Scr(0, 0), Scr(4, 1));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scrW = Screen.width / 16;
            scrH = Screen.height / 9;

            showSelectMenu = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            showSelectMenu = false;
        }
    }

    private void OnGUI()
    {
        debugWindow = GUI.Window(0, debugWindow, JoyStickUI, "");

        if (showSelectMenu)
        {
            CalulateMoseAngles();

            sectorDegree = 360 / numOfSectors;
            iconOffset = sectorDegree / 2;
            slotPos = SlotPositions(numOfSectors);
            boundsPos = BoundPositions(numOfSectors);

            // Center
            GUI.Box(new Rect(Scr(7.5f, 4), Scr(1, 1)), "");

            // Circle
            GUI.DrawTexture(new Rect(circleCenter.x - circleRadius - (circleScaleOffset / 4),
                                     circleCenter.y - circleRadius - (circleScaleOffset / 4),
                                    (circleRadius * 2) + (circleScaleOffset / 2),
                                    (circleRadius * 2) + (circleScaleOffset / 2)), radialTexture);

            if (showBoxes)
            {
                for (int i = 0; i < numOfSectors; i++)
                {
                    GUI.DrawTexture(new Rect(slotPos[i].x - (scrW * iconSizeNum * 0.5f), 
                                             slotPos[i].y - (scrH * iconSizeNum * 0.5f), 
                                             scrW * iconSizeNum, scrH * iconSizeNum), slotTexture);
                }
            }

            if (showBounds)
            {
                for (int i = 0; i < numOfSectors; i++)
                {
                    GUI.Box(new Rect(boundsPos[i].x - (scrW * 0.05f),
                                     boundsPos[i].y - (scrH * 0.05f),
                                     scrW * 0.1f, scrH * 0.1f), "");
                }
            }

            if (showIcons)
            {
                SetItemSlots(numOfSectors, slotPos);

            }
        }
    }

    void CalulateMoseAngles()
    {
        mouse = Input.mousePosition;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        mouseDistance = Mathf.Sqrt(Mathf.Pow((mouse.x - circleCenter.x), 2) + Mathf.Pow((mouse.y - circleCenter.y), 2));
        inputDist = Vector2.Distance(Vector2.zero, input);

        withinCircle = mouseDistance <= circleRadius ? true : false;

        if (input.x != 0 || input.y != 0)
        {
            inputAngle = (Mathf.Atan2(-input.y, input.x) * 180 / Mathf.PI) + radialRotation;
        }

        else
        {
            mouseAngles = (Mathf.Atan2(mouse.y * circleCenter.y, mouse.x - circleCenter.x) * 180 / Mathf.PI) + radialRotation;
        }

        if (mouseAngles < 0)
        {
            mouseAngles += 360;
        }

        if (inputAngle < 0)
        {
            inputAngle += 360;
        }

        inputIndex = CheckCurrentSector(inputAngle);
        mouseIndex = CheckCurrentSector(mouseAngles);

        if (input.x != 0 || input.y != 0)
        {
            sectorIndex = inputIndex;
        }

        if (input.x == 0 && input.y == 0)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                sectorIndex = mouseIndex;
            }
        }

    }

    private int CheckCurrentSector(float angle)
    {
        float boundAngle = 0;

        for (int i = 0; i < numOfSectors; i++)
        {
            boundAngle += sectorDegree;
            if (angle < boundAngle)
            {
                return i;
            }
        }

        return 0;
    }

    void SetItemSlots(int slots, Vector2[] pos)
    {
        for (int i = 0; i < slots; i++)
        {
            GUI.DrawTexture(new Rect(pos[i].x - (scrW * iconSizeNum * 0.5f), pos[i].y - (scrH * iconSizeNum * 0.5f), iconSizeNum, iconSizeNum), slotTexture);
            // pos[i].x - Scr(iconSizeNum * 0.5f, iconSizeNum * 0.5f).x, pos[i].y - Scr(iconSizeNum * 0.5f, iconSizeNum * 0.5f).y
        }
    }

    private Vector2[] SlotPositions(int slots)
    {
        Vector2[] slotPos = new Vector2[slots];
        float angle = iconOffset * radialRotation;

        for (int i = 0; i < slotPos.Length; i++)
        {
            slotPos[i].x = circleCenter.x + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            slotPos[i].y = circleCenter.y + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);

            angle += sectorDegree;
        }

        return slotPos;
    }

    private Vector2[] BoundPositions(int slots)
    {
        Vector2[] boundPos = new Vector2[slots];
        float angle = radialRotation;

        for (int i = 0; i < boundPos.Length; i++)
        {
            boundPos[i].x = circleCenter.x + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            boundPos[i].y = circleCenter.y + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);

            angle += sectorDegree;
        }


        return boundPos;
    }

    void JoyStickUI(int windowID)
    {
        GUI.Box(new Rect(Scr(0, 0), Scr(1, 1)), "");
        GUI.Box(new Rect(Scr(0.25f + (Input.GetAxis("Horizontal") * 0.25f), 0.25f + (-Input.GetAxis("Vertical") * 0.25f)), Scr(0.5f, 0.5f)), "");
        GUI.Box(new Rect(Scr(1.25f, 0.25f), Scr(0.5f, 0.5f)), "Tab");

        if (showSelectMenu)
        {
            GUI.Box(new Rect(Scr(1.25f, 0.25f), Scr(0.5f, 0.5f)), "");
        }

        GUI.DragWindow();

    }

    private Vector2 Scr(float x, float y)
    {
        Vector2 coord = Vector2.zero;
        coord = new Vector2(scrW * x, scrH * y);
        return coord;
    }

}

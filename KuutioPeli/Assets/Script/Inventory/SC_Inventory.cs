using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Inventory : MonoBehaviour
{
    int buttonclicks = 0;
    public Texture crosshairTexture;
    public MouseLook playerController;
    public SC_PickItem[] availableItems; //Prefab list

    //Free slots

    int[] itemSlots = new int [12];
    bool showInventory = false;
    float windowAnimation = 1;
    float animationTimer = 0;



    //UI Drag
    int hoveringOverIndex = -1;
    int itemIndexToDrag = -1;
    Vector2 dragOffset = Vector2.zero;


    //Pickup
    SC_PickItem detectedItem;
    int detectedItemIndex;
    

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        

        //Slots
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = -1;

        }
    }

    

    void Update()
    {
        if (buttonclicks == 2)
        {
            playerController.canMove = true;
            buttonclicks = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerController.canMove = false;
            buttonclicks += 1;
        }
    

        //ShowHide
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerController.canMove = false;
            buttonclicks += 1;

            showInventory = !showInventory;
            animationTimer = 0;
            if (showInventory)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (animationTimer < 1)
        {
            animationTimer += Time.deltaTime;
        }
        if (showInventory)
        {
            windowAnimation = Mathf.Lerp(windowAnimation, 0, animationTimer);
            //playerController.canMove = false;
        }
        else
        {
            windowAnimation = Mathf.Lerp(windowAnimation, 1f, animationTimer);
            //playerController.canMove = true;
        }

        //Begin drag
        if (Input.GetMouseButtonDown(0) && hoveringOverIndex > -1 && itemSlots[hoveringOverIndex] > -1)
        {
            itemIndexToDrag = hoveringOverIndex;
        }

        //Release
        if (Input.GetMouseButtonUp(0) && itemIndexToDrag > -1)
        {
            if (hoveringOverIndex < 0)
            {
                Instantiate(availableItems[itemSlots[itemIndexToDrag]], playerController.playerCamera.transform.position + (playerController.playerCamera.transform.forward), Quaternion.identity);
                itemSlots[itemIndexToDrag] = -1;
;           }
            else
            {
                //Swithc items
                int itemIndexImp = itemSlots[itemIndexToDrag];
                itemSlots[itemIndexToDrag] = itemSlots[hoveringOverIndex];
                itemSlots[hoveringOverIndex] = itemIndexImp;

            }
            itemIndexToDrag = -1;

        }

        //Pickup
        if (detectedItem && detectedItemIndex > -1)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Add item
                int slotToAddTo = -1;

                for (int i = 0; i < itemSlots.Length; i++)
                {
                    if (itemSlots[i] == -1)
                    {
                        slotToAddTo = i;
                        break;
                    }
                }
                if (slotToAddTo > -1)
                {
                    itemSlots[slotToAddTo] = detectedItemIndex;
                    detectedItem.PickItem();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Ray ray = playerController.playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if (Physics.Raycast(ray, out hit, 2.5f))
        {
            Transform objectHit = hit.transform;

            if (objectHit.CompareTag("Respawn"))
            {
                if ((detectedItem == null || detectedItem.transform != objectHit) && objectHit.GetComponent<SC_PickItem>() != null)
                {
                    SC_PickItem itemTmp = objectHit.GetComponent<SC_PickItem>();

                    for (int i = 0; i < availableItems.Length; i++)
                    {
                        if (availableItems[i].Vasara == itemTmp.Vasara)
                        {
                            detectedItem = itemTmp;
                            detectedItemIndex = i;
                        }
                               
                    }
                }
            }
            else
            {
                detectedItem = null;
            }
        }
        else
        {
            detectedItem = null;
        }
    }
    void OnGUI()
    {
        //UI
        GUI.Label(new Rect(5, 40, 200, 25), "Press 'Tab' to open inventory");


        //Window

    if ( windowAnimation < 1)
        {
            GUILayout.BeginArea(new Rect(10 - (430 * windowAnimation), Screen.height / 2 - 200, 302, 430), GUI.skin.GetStyle("Box"));
            GUILayout.Label("Inventory", GUILayout.Height(25));

            GUILayout.BeginVertical();

            for (int i = 0; i < itemSlots.Length; i +=3)
            {
                GUILayout.BeginHorizontal();

                for (int a =0; a < 3; a++)
                {
                    if (i + a < itemSlots.Length)
                    {
                        if (itemIndexToDrag == i + a || (itemIndexToDrag > -1 && hoveringOverIndex == i + a))
                        {
                            GUI.enabled = false;
                        }


                        if (itemSlots[i + a] > -1)
                        {
                            if (availableItems[itemSlots[i + a]].itemPreview)
                            {
                                GUILayout.Box(availableItems[itemSlots[i + a]].itemPreview, GUILayout.Width(95), GUILayout.Height(95));
                            }
                            else
                            {
                                GUILayout.Box(availableItems[itemSlots[i + a]].Vasara, GUILayout.Width(95), GUILayout.Height(95));
                            }
                        }
                        else
                        {
                            //Empty
                            GUILayout.Box("", GUILayout.Width(95), GUILayout.Height(95));
                        }
                        //Detect mouse cursor
                        Rect lastRect = GUILayoutUtility.GetLastRect();
                        Vector2 eventMousePosition = Event.current.mousePosition;
                        
                        if (Event.current.type == EventType.Repaint && lastRect.Contains(eventMousePosition))
                        {
                            hoveringOverIndex = i + a;

                            if (itemIndexToDrag < 0)
                            {
                                dragOffset = new Vector2(lastRect.x - eventMousePosition.x, lastRect.y - eventMousePosition.y);
                            }
                        }
                        GUI.enabled = true;
                    }
                }
                GUILayout.EndHorizontal();

            }
            GUILayout.EndHorizontal();

            if (Event.current.type == EventType.Repaint && !GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
            {
                hoveringOverIndex = -1;
            }
            GUILayout.EndArea();


        }
        //ItemDrag
        if (itemIndexToDrag > -1)
        {
            if (availableItems[itemSlots[itemIndexToDrag]].itemPreview)
            {
                GUI.Box(new Rect(Input.mousePosition.x + dragOffset.x, Screen.height - Input.mousePosition.y + dragOffset.y, 95, 95), availableItems[itemSlots[itemIndexToDrag]].itemPreview);
            }
            else
            {
                GUI.Box(new Rect(Input.mousePosition.x + dragOffset.x, Screen.height - Input.mousePosition.y + dragOffset.y, 95, 95), availableItems[itemSlots[itemIndexToDrag]].Vasara);
            }
        }
        //Display name while hovering over
        if (hoveringOverIndex > -1 && itemSlots[hoveringOverIndex] > -1 && itemIndexToDrag < 0)
        {
            GUI.Box(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y - 30, 100, 25), availableItems[itemSlots[hoveringOverIndex]].Vasara);
        }
        if (!showInventory)
        {
            GUI.color = detectedItem ? Color.green : Color.white;
            GUI.DrawTexture(new Rect(Screen.width / 2 - 4, Screen.height / 2 - 4, 8, 8), crosshairTexture);
            GUI.color = Color.white;

            //Message
            if (detectedItem)
            {
                GUI.color = new Color(0, 0, 0, 0.84f);
                GUI.Label(new Rect(Screen.width / 2 - 75 + 1, Screen.height / 2 - 50 + 1, 150, 20), "Press 'F' to pick up'" + detectedItem.Vasara + "'");
                GUI.color = Color.green;
                GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 20), "Press 'F' to pick up '" + detectedItem.Vasara + "'");
            }
        }
    }

}

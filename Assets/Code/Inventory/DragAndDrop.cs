using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace MyNameSpace
{
    public class DragAndDrop : MonoBehaviour
    {
        public static DragAndDrop Instance { get; private set; }

        public static bool IsDragging { get; private set; }

        public Image image;
        public RectTransform spriteTransform;

        ItemSaveFile startingFile;
        UIItemSlot startingSlot;

        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            if (IsDragging)
            {
                spriteTransform.position = Input.mousePosition;

                /*
                 Vector2 pos;
             RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
             transform.position = myCanvas.transform.TransformPoint(pos);
                 */
            }
        }

        public void StartDrag(UIItemSlot uiSlot, ItemSaveFile itemFile)
        {
            if (itemFile != null && itemFile.ID != ItemID.Empty)
            {
                IsDragging = true;

                startingSlot = uiSlot;
                startingFile = itemFile;

                image.sprite = ItemDirectory.GetItem(itemFile.ID).icon;
                image.enabled = true;
            }
        }

        public void StopDrag(GameObject endObject)
        {
            if (!IsDragging || !startingSlot.CanReleaseFile(startingFile)) return;

            IsDragging = false;
            image.enabled = false;

            /* Potential outcomes in drag-and-drop:
             * A - item is dropped on the ground
             * B - Receiver has no item and simply takes the sender's file
             * C1 - 2 items swap successfull
             * C2 - 2 items stacked successfully
             * C3. Sender file can't be accepted by receiver OR
             *     Receiver cann't accept the sender's returning file 
             */


            //If we didn't drag onto a slot, then drop the item
            if (endObject == null)
            {
                startingSlot.DragAndDraop_DropItemOnGroundAndNotifyInventory();
            }
            else
            {
                UIItemSlot endSlot = endObject.GetComponent<UIItemSlot>();
                if (endSlot == null)
                {
                    //A - Drop item
                    startingSlot.DragAndDraop_DropItemOnGroundAndNotifyInventory();
                }
                else
                {
                    ItemSaveFile endFile = endSlot.ItemFile;

                    //If there is an end slot and it accepts this file type...
                    if (endSlot.CanAcceptFile(startingFile))
                    {
                        if (IsFileEmpty(endFile))
                        {
                            //B - If the slot is empty then just add it
                            endSlot.DragAndDrop_ForceAssignFile_AndNotifyInventory(startingFile);
                            startingSlot.DragAndDrop_ClearSlotAndNotifyInventory();
                        }
                        else //...but if there is already an item here...
                        {
                            //...First check if we can stack, and if not...
                            if (endSlot.DragAndDrop_TryStackingItems_AndNotifyInventory(startingFile))
                            {
                                startingSlot.DragAndDrop_ClearSlotAndNotifyInventory();
                            }
                            else
                            {
                                // ... check if we can swap...
                                if (startingSlot.CanAcceptFile(endSlot.ItemFile))
                                {
                                    endSlot.DragAndDrop_ForceAssignFile_AndNotifyInventory(new ItemSaveFile(startingFile.ID, startingFile.stacks));
                                    startingSlot.DragAndDrop_ForceAssignFile_AndNotifyInventory(new ItemSaveFile(endFile.ID, endFile.stacks));
                                }
                            }
                        }
                    }
                }
            }
        }

        bool IsFileEmpty(ItemSaveFile file) => file == null || file.ID == ItemID.Empty;

        void OnGUI()
        {
            GUI.Label(new Rect(20, 220, 200, 20), "startFile: " + startingFile);
            GUI.Label(new Rect(20, 240, 200, 20), "dragging: " + IsDragging);
            //GUI.Label(new Rect(20, 260, 200, 20), "endFile: "   + endFile);
        }
    }
}
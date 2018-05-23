using System;
using System.Collections;
using UnityEngine;

// Versio 2.5 Update 
// 
// Set name of the other gameobject that can start the trigger
// Set movement speed per node
// Set rotation speed per node
// Rare bug fixes
// 

public class NodeFollow : MonoBehaviour
{
    #region Private variables
    private Quaternion originalRotation;            // Original rotation of the moving object

    private Vector3 targetDirection;                // Moving object facing way

    private Vector3 currentPoint;                   // Position the moving object is currently moving to

    private bool reverse;                           // Should the moving object move in reverse

    private bool triggerEntered;                    // Is start trigger entered

    private bool stopMoving;                        // Should the moving object stop
    #endregion

    #region Public variables
    public bool useWhiteGizmo;                      // Change gizmo icon to the same as handles color   (White)

    public bool useBlueGizmo;                       // Change gizmo icon to the same as handles color   (Blue)

    public bool useRedGizmo;                        // Change gizmo icon to the same as handles color   (Red)

    public bool useGreenGizmo;                      // Change gizmo icon to the same as handles color   (Green)

    public bool useYellowGizmo;                     // Change gizmo icon to the same as handles color   (Yellow)

    public enum SetHandlesColor                     // Colors for handles
    {
        white,
        blue,
        red,
        green,
        yellow
    }

    public enum Direction                           // Ways that the movingObject can be facing
    {
        up,
        down,
        left,
        right
    }

    public SetHandlesColor HandlesColor;            // Colors for handles

    public Direction objectDirection;               // Which way the gameObject is facing

    public GameObject movingObject;                 // The gameobject that should move

    public int pointSelection;                      // Node number the movingObject is currently moving towards to

    public float movementSpeed;                     // How fast the movingObject should move

    public bool nodeBasedSpeed;                     // Use node based movement speed

    public bool useTrigger;                         // Start moving object at start instead of onTriggerEnter

    public string triggerName;                      // Name of the other gameobject that can start the trigger

    public bool moveToStart;                        // Should the movingObject instantly move back to the start once end has been reached

    public bool rotateTowardsNextNode;              // Should the movingObject rotate towards next node

    public float rotationSpeed;                     // How fast the rotation should be

    public bool nodeBasedRotation;                  // // Use node based rotation speed

    public bool loop;                               // Should the movement loop                 

    public bool drawLines = true;                   // Draw lines in editor mode at all

    public bool drawDotLine;                        // Draw dot lines

    public bool drawLastToFirst;                    // Draw line from last point to the first

    public bool showHandles = true;                 // Show handles

    public float handlesSize = 1;                   // Size of the node handles

    public Vector3[] nodes = new Vector3[0];        // List of all nodes

    public float[] stops = new float[0];            // List of all wait times

    public float[] speeds = new float[0];           // List of all movement speeds for node based movement

    public float[] rotations = new float[0];        // List of all rotation speeds for node based rotation

    public Color backgroundColor = Color.white;     // Custom editor background color

    public Vector3 currentPosition;                 // Used to store transforms current position

    public Vector3 lastPosition;                    // Used to compare transform position and and it's last position

    public float newX;                              // Used to store new x value if transform is moved   

    public float newY;                              // Used to store new y value if transform is moved 

    public float newZ;                              // Used to store new z value if transform is moved 

    public bool firstNodesplaced;                   // Check if any nodes are placed already. If not, place first ones with offset pattern

    public float xResetPosition;                    // X reset offset position

    public float yResetPosition;                    // Y reset offset position

    public float zResetPosition;                    // Z reset offset position

    public bool objectSettingsFoldout;              // Foldout

    public bool movementSettingsFoldout;            // Foldout

    public bool visualSettingsFoldout;              // Foldout
    #endregion

    void Reset()
    {
        #region Rename gameobject
        gameObject.name = "Node Follow";
        #endregion
    }

    void OnValidate()
    {
        #region Resize arrays
        if (nodes != null)
        {
            #region Resize "stops" array to equal size with node array
            if (stops.Length != nodes.Length)
            {
                Array.Resize(ref stops, nodes.Length);
                Array.Clear(stops, 0, stops.Length);
            }
            #endregion

            #region Resize "speeds" array to equal size with node array
            if (speeds.Length != nodes.Length)
            {
                Array.Resize(ref speeds, nodes.Length);
                Array.Clear(speeds, 0, speeds.Length);

                for (int i = 0; i < speeds.Length; i++)
                {
                    speeds[i] = movementSpeed;
                }
            }
            #endregion

            #region Resize "rotations" array to equal size with node array
            if (rotations.Length != nodes.Length) 
            {
                Array.Resize(ref rotations, nodes.Length);
                Array.Clear(rotations, 0, rotations.Length);

                for (int i = 0; i < rotations.Length; i++)
                {
                    rotations[i] = rotationSpeed;
                }
            }
            #endregion
        }
        #endregion
    }

    void Start()
    {
        #region Null check
        if (movingObject == null)
        {
            Debug.LogError("Moving object not set");
            return;
        }
        if (nodes.Length == 0)
        {
            Debug.LogError("Nodes not set");
            return;
        }
        #endregion

        #region Reference to the original rotation of moving object
        originalRotation = movingObject.transform.rotation;
        #endregion

        #region Start at first node
        movingObject.transform.position = nodes[0];
        currentPoint = nodes[0];
        pointSelection = 0;
        #endregion
    }

    void Update()
    {
        #region Null Check
        if (movingObject == null)
        {
            return;
        }
        if (nodes.Length == 0)
        {
            return;
        }
        #endregion

        #region Setting the direction of moving object
        switch (objectDirection)
        {
            case Direction.up:
                targetDirection = Vector3.up;
                break;
            case Direction.down:
                targetDirection = Vector3.down;
                break;
            case Direction.left:
                targetDirection = Vector3.back;
                break;
            case Direction.right:
                targetDirection = Vector3.forward;
                break;
        }
        #endregion

        #region Allow movement only if ...
        if ((triggerEntered == true || useTrigger == false) && stopMoving == false)
        {
            #endregion

            #region Moving object moves from current point towards to currentpoint
            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, currentPoint, Time.deltaTime * movementSpeed);
            #endregion

            #region Rotate towards Next Node
            if (rotateTowardsNextNode == true)
            {
                if (movingObject.transform.position != currentPoint)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(movingObject.transform.position - currentPoint, targetDirection);
                    targetRotation.x = 0.0f;
                    targetRotation.y = 0.0f;
                    movingObject.transform.rotation = Quaternion.Slerp(movingObject.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }
            }

            else
            {
                movingObject.transform.rotation = originalRotation;
            }
            #endregion

            #region Require new trigger enter if moving object is at the first node
            if (movingObject.transform.position == nodes[0])
            {
                triggerEntered = false;
            }
            #endregion

            #region Check if moving object reached current point
            if (movingObject.transform.position == currentPoint)
            {
                #endregion

                #region Change movement speed on every node if node based movement is chosen
                if (nodeBasedSpeed == true)
                {
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        if (currentPoint == nodes[i])
                        {
                            if (currentPoint == nodes[nodes.Length - 1] && moveToStart == true)
                            {
                                // Do nothing
                            }
                            else
                            {
                                movementSpeed = speeds[i];
                            }
                        }
                    }
                }
                #endregion

                #region Change rotation speed on every node if node based rotation is chosen
                if (nodeBasedRotation == true)
                {
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        if (currentPoint == nodes[i])
                        {
                            if (currentPoint == nodes[nodes.Length - 1] && moveToStart == true)
                            {
                                // Do nothing
                            }
                            else
                            {
                                rotationSpeed = rotations[i];
                            }
                        }
                    }
                }
                #endregion

                #region Stop at each node for the set amount of stop time
                for (int i = 0; i < nodes.Length; i++)
                {
                    if (currentPoint == nodes[i])
                    {
                        if (currentPoint == nodes[nodes.Length - 1] && moveToStart == true)
                        {
                            // Do nothing
                        }
                        else
                        {
                            StartCoroutine(WaitBeforeMoving(stops[i]));
                        }
                    }
                }
                #endregion

                #region Check if looping is allowed. Change to reverse at the end and back to forward movement in start
                if (loop == false)
                {
                    pointSelection++;
                }
                if (loop == true && pointSelection == nodes.Length -1)
                {
                    reverse = true;
                }
                if (loop == true && movingObject.transform.position == nodes[0])
                {
                    reverse = false;
                }
                if (loop == true && reverse == false)
                {
                    pointSelection++;
                }

                if (loop == true && reverse == true)
                {
                    pointSelection--;
                }
                #endregion

                #region Last node reached. If not looping, move back to start either normally or instantly
                else if (pointSelection == nodes.Length && loop == false)
                {
                    if (moveToStart == true)
                    {
                        movingObject.transform.position = nodes[0];
                        movingObject.transform.rotation = originalRotation;
                        pointSelection = 0;
                    }
                    else
                    {
                        pointSelection = 0;
                    }
                }
                #endregion

                #region Update currentPoint
                if (nodes.Length > 1)
                {
                    currentPoint = nodes[pointSelection];

                }
                #endregion
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        #region Check if other gameobject with "triggerName" entered trigger zone
        if (other.gameObject.name == triggerName)
        {
            triggerEntered = true;
        }
        #endregion
    }

    IEnumerator WaitBeforeMoving(float stopTime)
    {
        #region Stop at each node for the set amount of stop time
        stopMoving = true;
        yield return new WaitForSeconds(stopTime);
        stopMoving = false;
        #endregion
    }

    void OnDrawGizmosSelected()
    {
        #region If using trigger, add BoxCollider2D if there is no already one. If not using trigger, remove the BoxCollider2D if there is one
        if (gameObject.GetComponent<BoxCollider2D>() == null)
        {
            if (useTrigger == true)
            {
                gameObject.AddComponent<BoxCollider2D>();
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
        else if (gameObject.GetComponent<BoxCollider2D>() != null)
        {
            if (useTrigger == false)
            {
                DestroyImmediate(gameObject.GetComponent<BoxCollider2D>());
            }
        }
        #endregion

        #region Check if lines should be drawn
        if (drawLines == false || nodes == null)
        {
            return;
        }
        #endregion

        #region Check what icon should be drawn
        for (int i = 1; i < nodes.Length; i++)
        {
            if (useWhiteGizmo == true)
            {
                Gizmos.DrawIcon(nodes[i], "Icon White", false);
                Gizmos.DrawIcon(nodes[i - 1], "Icon White", false);
            }
            if (useBlueGizmo == true)
            {
                Gizmos.DrawIcon(nodes[i], "Icon Blue", false);
                Gizmos.DrawIcon(nodes[i - 1], "Icon Blue", false);
            }
            if (useRedGizmo == true)
            {
                Gizmos.DrawIcon(nodes[i], "Icon Red", false);
                Gizmos.DrawIcon(nodes[i - 1], "Icon Red", false);
            }
            if (useGreenGizmo == true)
            {
                Gizmos.DrawIcon(nodes[i], "Icon Green", false);
                Gizmos.DrawIcon(nodes[i - 1], "Icon Green", false);
            }
            if (useYellowGizmo == true)
            {
                Gizmos.DrawIcon(nodes[i], "Icon Yellow", false);
                Gizmos.DrawIcon(nodes[i - 1], "Icon Yellow", false);
            }
        }
        #endregion
    }
}
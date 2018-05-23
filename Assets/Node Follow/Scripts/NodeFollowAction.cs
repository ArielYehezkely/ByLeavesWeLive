using System.Collections;
using UnityEngine;

public class NodeFollowAction : MonoBehaviour
{
    #region Private variables
    private NodeFollow nodeFollowScript;                // Reference to the Node Follow
    #endregion

    #region Public Variables
    public string actionName;                           // Name to be passed with send message

    public int node;                                    // The node number

    public bool doingAction;                            // Is doing the action
    #endregion

    void Start ()
    {
        #region Get component & null checks
        if (gameObject.GetComponent<NodeFollow>() != null)
        {
            nodeFollowScript = gameObject.GetComponent<NodeFollow>();
        }
        else
        {
            Debug.LogError("Gameobject has no NodeFollow");
            return;
        }
        if (nodeFollowScript.movingObject == null)
        {
            Debug.LogError("Moving object not set");
            return;
        }
        if (node > nodeFollowScript.nodes.Length || node <= 0)
        {
            Debug.LogError("Invalid node number");
            return;
        }
        #endregion
    }

    void Update ()
    {
        #region Null checks
        if (nodeFollowScript == null)
        {
            return;
        }
        if (nodeFollowScript.movingObject == null)
        {
            return;
        }
        if (node > nodeFollowScript.nodes.Length || node <= 0)
        {
            Debug.LogError("Invalid node number");
            return;
        }
        #endregion

        #region At the specified node and check that not already doing the action
        if ((nodeFollowScript.movingObject.transform.position == nodeFollowScript.nodes[node-1] && doingAction == false) || (node == nodeFollowScript.nodes.Length && nodeFollowScript.movingObject.transform.position == nodeFollowScript.nodes[nodeFollowScript.nodes.Length -1]))
        {
            #region Starts coroutine that checks if moving object left the node position
            StartCoroutine(WaitUntillMovingOut());
            #endregion

            #region Calls method with given name on all scripts in gameobject. Used to start the "Action"
            gameObject.SendMessage(actionName, null, SendMessageOptions.RequireReceiver);
            #endregion
        }
        #endregion
    }

    IEnumerator WaitUntillMovingOut()
    {
        while (nodeFollowScript.movingObject.transform.position == nodeFollowScript.nodes[node-1] || (node == nodeFollowScript.nodes.Length && nodeFollowScript.movingObject.transform.position == nodeFollowScript.nodes[nodeFollowScript.nodes.Length - 1]))
        {
            doingAction = true;
            yield return null;
        }

        doingAction = false;
    }
}
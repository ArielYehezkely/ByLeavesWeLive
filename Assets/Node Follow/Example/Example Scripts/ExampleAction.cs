using System.Collections;
using UnityEngine;

/// <This is example>
/// EXAMPLE
/// <This is example>

public class ExampleAction : MonoBehaviour
{
    #region Private variables
    private NodeFollow nodeFollow;      // Reference to the Node Follow
    private NodeFollowAction action;    // Reference to the Node Follow Action
    #endregion

    void Start()
    {
        #region Get components
        nodeFollow = gameObject.GetComponent<NodeFollow>();    
        action = gameObject.GetComponent<NodeFollowAction>();
        #endregion
    }

    #region Action called from the NodeFollowAction
    void WantedAction()
    {
        StartCoroutine(ChangeColor());
    }
    #endregion

    #region WantedAction calls this enumerator to change the moving objects color while in the specified node
    IEnumerator ChangeColor()
    {
        while (action.doingAction == true)
        {
            nodeFollow.movingObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            yield return null;
        }
        nodeFollow.movingObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }
    #endregion
}
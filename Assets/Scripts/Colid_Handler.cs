using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TagListType
{
    Blacklist,
    Whitelist
}

public class Colid_Handler : MonoBehaviour
{
    //Allows objects to Have Given TagListType
    [SerializeField]
    private TagListType tagListType = TagListType.Blacklist;

    //Allows the interaction of the TagListType
    [SerializeField]
    private List<string> tags;

    //On Colliding with another object
    void OnTriggerEnter(Collider other)
    {
        bool TagInList = tags.Contains(other.gameObject.tag);

        //If TaglistType is Blacklist
        if (tagListType == TagListType.Blacklist
            && TagInList)
        {
            //Function to Be Determined
        }
        //If TaglistType is Whitelist
        else if (tagListType == TagListType.Whitelist
            && !TagInList)
        {
            //Function to Be Determined
        }
        else
        {
            //Uses Default Collision Code
        }
    }

}

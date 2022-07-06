using Items;
using Persist;
using UnityEngine;

public class CustomDialogueRewardTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        DialogueReward.AddReward(gameObject, InventoryList.GetSharedBag(), ResourceIndex.GetItem(201));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

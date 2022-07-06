using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cysharp.Threading.Tasks;
using UI;
using UI.Dialogue;
using UnityEngine;

namespace Items {
    public class Shopkeeper : MonoBehaviour, InteractionTarget {
        [field: SerializeField] public float Rate { get; private set; } = 3;
        [SerializeField] private Warelist stockList;
        [SerializeField] private String name;
        [SerializeField] private List<String> openingDialogue = new List<string> {"I am a shopkeeper. I keep the shops."};
        [SerializeField] private String purchaseDialogue = "1x $item for you!";
        [SerializeField] private String leaveDialogue = "Have a nice day!";
        [SerializeField] private String windowShopperDialogue = "";
        [SerializeField] private String brokeDialogue = "Stop wasting my time.";
        public ReadOnlyCollection<Item> Items => stockList.Stock;

        public async UniTask<bool> SellTo(InventoryList bag, int index) {
            var ware = Items[index];
            var totalCost = (int) Math.Round(ware.cost * Rate);
            if (bag.SubtractMoney(totalCost)) {
                bag.AddItem(ware);
                await UIManager.INSTANCE.PerformDialogue(new DialogueChunk(name, purchaseDialogue));
                return true;
            }

            await UIManager.INSTANCE.PerformDialogue(new DialogueChunk(name, brokeDialogue));

            return false;
        }

        public void Interact() {
            var chunks = openingDialogue.Select(d => new DialogueChunk(name, d)).ToList();
            UIManager.INSTANCE.PerformDialogue(chunks).Forget();
        }
    }
}

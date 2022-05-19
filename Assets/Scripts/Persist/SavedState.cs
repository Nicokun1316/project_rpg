using System;
using Items;
using UnityEngine;

namespace Persist {
    [Serializable]
    public class SavedState  {
        public InventoryList inventory;
    }
}

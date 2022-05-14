using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UI;
using UnityEngine;

public class TabItem : MonoBehaviour, RedirectingFocusable {
    [SerializeField]
    private GameObject targetTab;

    Focusable RedirectingFocusable.target => targetTab.GetComponent<Focusable>();
}

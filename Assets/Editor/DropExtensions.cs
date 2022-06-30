using System.Linq;
using Items;
using UI.Dialogue;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editor {
    [InitializeOnLoad]
    public static class DropExtensions {
        private struct DropData {
            public Sprite[] sprites;
            public Vector2 position;
        }

        static DropExtensions() {
            DragAndDrop.AddDropHandler(HierarchyHandler);
            DragAndDrop.AddDropHandler(SceneHandler);
            SceneView.duringSceneGui += OnSceneGui;
        }

        private static bool drawingOutline;
        private static Texture2D icon;

        static DragAndDropVisualMode HierarchyHandler(int instanceTargetId, HierarchyDropFlags dropMode, Transform parentForDrops, bool perform) {
            if (perform) {
                var view = SceneView.lastActiveSceneView;
                Vector2 center = view.pivot;
                var position = SnapPoint(center);
                return AddAtPosition(position) ? DragAndDropVisualMode.Copy : DragAndDropVisualMode.None;
            }

            return DragAndDropVisualMode.None;
        }

        private static Sprite TextureToSprite(Texture2D texture) {
            var path = AssetDatabase.GetAssetPath(texture);
            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }

        static bool AddAtPosition(Vector2 position) {
            var objs = DragAndDrop.objectReferences;
            var hasTextures = objs.OfType<Texture2D>().Any();
            var hasSprites = objs.OfType<Sprite>().Any();
            if (!hasTextures && !hasSprites) return false;
            var menu = new GenericMenu();
            object dropData;
            if (hasSprites) {
                dropData = new DropData {
                    sprites = objs.OfType<Sprite>().ToArray(),
                    position = position
                };
            } else {
                dropData = new DropData {
                    sprites = objs.OfType<Texture2D>().Select(TextureToSprite).ToArray(),
                    position = position
                };
            }

            menu.AddItem(new GUIContent("Dialogue"), false, AddInteractibles, dropData);
            menu.AddItem(new GUIContent("Chest"), false, AddChests, dropData);
            menu.ShowAsContext();
            return true;
        }

        static DragAndDropVisualMode SceneHandler(object dropUpon, Vector3 worldPosition, Vector2 viewportPosition,
            Transform parentTransform, bool perform) {
            var view = SceneView.lastActiveSceneView;
            Vector2 pos = view.camera.ScreenToWorldPoint(GetMousePos(view));
            var position = SnapPoint(pos);
            if (perform) {
                drawingOutline = false;
                return AddAtPosition(position) ? DragAndDropVisualMode.Copy : DragAndDropVisualMode.None;
            } else {
                var texture = DragAndDrop.objectReferences.OfType<Texture2D>().FirstOrDefault();
                var sprite = DragAndDrop.objectReferences.OfType<Sprite>().FirstOrDefault();
                if (texture == null && sprite != null) {
                    texture = sprite.texture;
                }

                if (texture != null) {
                    drawingOutline = true;
                    icon = texture;
                    return DragAndDropVisualMode.Copy;
                }
            }


            return DragAndDropVisualMode.None;
        }

        static void AddInteractibles(object data) {
            if (data is DropData dropData) {
                foreach (var sprite in dropData.sprites) {
                    CreateBasicInteractible(sprite, dropData.position).AddComponent<SimpleDialogueComponent>();
                }
            }
        }

        static void AddChests(object data) {
            if (data is DropData dropData) {
                foreach (var sprite in dropData.sprites) {
                    var go = CreateBasicInteractible(sprite, dropData.position);
                    var bagID = AssetDatabase.FindAssets("shared_bag t:InventoryList").First();
                    var bagPath = AssetDatabase.GUIDToAssetPath(bagID);
                    var bag = AssetDatabase.LoadAssetAtPath<InventoryList>(bagPath);
                    DialogueReward.AddReward(go, bag);
                    go.AddComponent<ChestDialogue>();
                }
            }
        }

        static GameObject CreateBasicInteractible(Sprite sprite, Vector2 position) {
            var go = new GameObject("Interactible", typeof(SpriteRenderer), typeof(BoxCollider2D), typeof(DialogueComponent));
            Undo.RegisterCreatedObjectUndo(go, "Created interactible");
            var renderer = go.GetComponent<SpriteRenderer>();
            go.layer = LayerMask.NameToLayer("Interactible");
            renderer.sprite = sprite;
            var collider = go.GetComponent<BoxCollider2D>();
            collider.size = sprite.rect.size / sprite.pixelsPerUnit;
                    
            go.transform.position = position;
            return go;
        }

        static Vector2 GetMousePos(SceneView view) {
            var p = Event.current.mousePosition;
            var mousePos = Event.current.mousePosition;
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = view.camera.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;
            return mousePos;
        }

        static void DrawRectAtPoint(Vector2 point, float width) {
            var hw = width / 2;
            var p1 = point + new Vector2(-hw, -hw);
            var p2 = point + new Vector2(-hw, +hw);
            var p3 = point + new Vector2(+hw, +hw);
            var p4 = point + new Vector2(+hw, -hw);
            Handles.DrawLine(p1, p2);
            Handles.DrawLine(p2, p3);
            Handles.DrawLine(p3, p4);
            Handles.DrawLine(p4, p1);
        }

        static Vector2 SnapPoint(Vector2 point) {
            return Vector2Int.RoundToInt(point + new Vector2(0.5f, 0.5f)) - new Vector2(0.5f, 0.5f);
        }

        static void OnSceneGui(SceneView view) {
            var mousePos = GetMousePos(view);
            if (!view.position.Contains(Event.current.mousePosition + view.position.min) || mousePos.y <= 0) {
                drawingOutline = false;
            }

            if (drawingOutline) {
                Handles.color = Color.red;
                var p = SnapPoint(view.camera.ScreenToWorldPoint(mousePos));
                DrawRectAtPoint(p, 1);
                Graphics.DrawTexture(new Rect(p.x - 0.5f, p.y + 0.5f, 1, -1), icon);
            }
        }
        
    }
}

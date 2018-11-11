using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public Level Level { get; set; }

    void Start() {
        Level = Level.Instance;
    }

    public void Update() {
        if(Input.GetKeyDown("w") && !Collisions(Vector3Int.up)) {
            transform.Translate(0, 1, 0);
        } else if (Input.GetKeyDown("s") && !Collisions(Vector3Int.down)) {
            transform.Translate(0, -1, 0);
        }

        if (Input.GetKeyDown("a") && !Collisions(Vector3Int.left)) {
            transform.Translate(-1, 0, 0);
        } else if (Input.GetKeyDown("d") && !Collisions(Vector3Int.right)) {
            transform.Translate(1, 0, 0);
        }
    }

    bool Collisions(Vector3Int direction) {
        Vector3Int newPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z) + direction;

        Tile tileToMoveTo = Level.GetTileAt(newPosition.x, newPosition.y);

        if (tileToMoveTo.Type == TileType.WALL) {
            return true;
        } else if(tileToMoveTo.itemOnTop != null) {
            switch(tileToMoveTo.itemOnTop.tag) {
                case "MovableItem":
                    MovableItem item = tileToMoveTo.itemOnTop.GetComponent<MovableItem>();
                    // You add direction again because this is what the item will be moving to. Basically checking your position + 2
                    if(item.CanMoveItem(newPosition + direction)) {
                        // If you can move the item that direction then move it and move character
                        item.MoveItem(newPosition + direction);
                    } else {
                        // If you can't move the item that direction then don't do anything
                        return true;
                    }
                break;
                case "CollectibleItem":
                    CollectibleItem collectible = tileToMoveTo.itemOnTop.GetComponent<CollectibleItem>();

                    collectible.CollectItem();
                break;
            }
        }

        return false;
    }
}

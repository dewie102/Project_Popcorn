using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableItem : MonoBehaviour {

    public Level level { get; protected set; }

    void Start() {
        level = Level.Instance;
    }

    public void MoveItem(Vector3Int Position) {
        level.GetTileAt((int)transform.position.x, (int)transform.position.y).RemoveItemOnTile();
        level.GetTileAt(Position.x, Position.y).AddItemOnTile(this.gameObject);
        transform.position = Position;
    }

	public bool CanMoveItem(Vector3Int Position) {

        Tile newTile = level.GetTileAt(Position.x, Position.y);

        // If the newTile is a wall or has a movableItem script then you cannot move
        if(newTile.Type != TileType.WALL) {
            if (newTile.itemOnTop == null || newTile.itemOnTop.GetComponent<MovableItem>() == null) {
                return true;
            }
        }

        return false;
    }
}

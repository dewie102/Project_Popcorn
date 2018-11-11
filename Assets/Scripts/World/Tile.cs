using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {FLOOR = 0, WALL};

public class Tile {

	public int X { get; protected set; }
    public int Y { get; protected set; }
    public TileType Type { get; protected set; }
    public GameObject itemOnTop { get; protected set; }


    public Tile(int X, int Y, TileType Type, GameObject ItemOnTop = null) {
        this.X = X;
        this.Y = Y;
        this.Type = Type;
        this.itemOnTop = ItemOnTop;
    }

    public void RemoveItemOnTile() {
        itemOnTop = null;
    }

    public void AddItemOnTile(GameObject Item) {
        itemOnTop = Item;
    }
}

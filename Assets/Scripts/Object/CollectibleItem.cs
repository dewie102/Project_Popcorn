using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {
    
    public Level level { get; protected set; }

	void Start () {
        level = Level.Instance;
	}

    public void CollectItem() {
        level.numberItemsCollected++;
        level.GetTileAt((int)transform.position.x, (int)transform.position.y).RemoveItemOnTile();
        Destroy(this.gameObject);
    }
}

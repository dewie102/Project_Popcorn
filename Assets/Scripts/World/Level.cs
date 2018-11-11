using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public Sprite floorSprite;
    public Sprite wallSprite;
    public Sprite characterSpriteFront;

    public static Level Instance { get; protected set; }

    public int Width = 10;
    public int Height = 10;

    public int numberItemsCollected = 0;
    int numberOfCollectibles = 0;

    Tile[,] tiles;

    Dictionary<Tile, GameObject> tileGameObjectMap;

    void OnEnable () {
        if (Instance != null) {
            Debug.LogError("Should not have multiple levels at one time.");
        }

        Instance = this;

        SetupLevel(Width, Height);
	}

    void Update() {
        if(numberOfCollectibles != 0) {
            if(numberItemsCollected >= numberOfCollectibles) {
                // Activate Portal
                Debug.Log("Activating portal now!");
            }
        }
    }

    void SetupLevel(int Width, int Height) {
        tiles = new Tile[Width, Height];

        tileGameObjectMap = new Dictionary<Tile, GameObject>();

        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1) {
                    tiles[x, y] = new Tile(x, y, TileType.WALL);
                } else {
                    tiles[x, y] = new Tile(x, y, TileType.FLOOR);
                }
            }
        }

        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                Tile tile = GetTileAt(x, y);

                GameObject tile_go = new GameObject();
                tile_go.transform.position = new Vector3(x, y, 0.0f);
                tile_go.name = "Tile_" + x + ", " + y;
                tile_go.transform.parent = transform;

                tileGameObjectMap.Add(tile, tile_go);

                SpriteRenderer sr = tile_go.AddComponent<SpriteRenderer>();
                
                switch(tile.Type) {
                    case TileType.FLOOR:
                        sr.sprite = floorSprite;
                        break;
                    case TileType.WALL:
                        sr.sprite = wallSprite;
                        break;
                }
            }
        }

        int size = 0;
        if(Width > Height) {
            size = Width / 2 - 1;
        } else if(Height >= Width) {
            size = Height / 2 + 1;
        }

        Camera.main.orthographicSize = size;
        Camera.main.transform.position = new Vector3(Width / 2 - 0.5f, Height / 2 - 0.5f, Camera.main.transform.position.z);

        // All temp ways to setup the level, should be loaded or preset somehow

        CreateCharacter(2, 2);

        MovableItem[] items = FindObjectsOfType<MovableItem>();
        foreach (var item in items) {
            GetTileAt((int)item.transform.position.x, (int)item.transform.position.y).AddItemOnTile(item.gameObject);
        }

        CollectibleItem[] Collectibles = FindObjectsOfType<CollectibleItem>();
        foreach (var item in Collectibles) {
            GetTileAt((int)item.transform.position.x, (int)item.transform.position.y).AddItemOnTile(item.gameObject);
            numberOfCollectibles++;
        }
    }

    public void CreateCharacter(int X, int Y) {

        GameObject character_go = new GameObject();
        character_go.transform.position = new Vector3(X, Y, 0);
        character_go.name = "Character";
        character_go.AddComponent<Character>();
        character_go.AddComponent<BoxCollider2D>();

        GameObject character_spriteGO = new GameObject();
        character_spriteGO.transform.parent = character_go.transform;
        character_spriteGO.name = "Sprite Object";
        character_spriteGO.transform.localPosition = new Vector3(0.0f, 0.18f, 0.0f);
        SpriteRenderer character_sr = character_spriteGO.AddComponent<SpriteRenderer>();
        character_sr.sprite = characterSpriteFront;
        character_sr.sortingLayerName = "Character";
    }

    public Tile GetTileAt(int X, int Y) {
        if(X < 0 || X > Width || Y < 0 || Y > Height) {
            return null;
        }

        return tiles[X, Y];
    }
}
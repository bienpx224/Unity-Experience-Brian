# Unity Tilemaps : 

## Start : 
Tilemaps :
- ở Hierarchy ta tạo : 2D > Tilemaps > Regular : Tạo xong thì chọn tile cần vẽ ở trong tile Palatte (Chưa hiện thì vào mục Window chọn 2D > tilePalatte).
- Lưu ý nên setup các tile có kích cỡ per unit tương ứng với mỗi ô trên màn hình. Ví dụ ở game mình chọn sprites, chọn pixels per unit là 16 vì sprites có size là 16x16.
- Ở trong assets Tiles. Ta ấn vào rồi chọn collider phù hợp cho từng tile. Ví dụ Tile thảm cỏ thì chọn collider là none. còn Tile tường thì chọn collider là Sprite.
- Tạo tilemap: 1 tilemap background, và 1 tilemap có thể phá huỷ, kiểu chỉ là vật cản mà thôi. (indestructibles va destructibles)
- Thêm Tilemaps collider 2D vào cho 2 tilemap đó. Nếu muốn các collider liên kết thành khối với nhau thì add thêm component Composite Collider 2D, tuy nhiên chỉnh type là static collider để nó đứng yên ko move.

- Import Tilemap Asset or Kit or Sprite, etc to using in project. 
- Click Window > 2D > Tile Palette to open Window Tile Palette. 
- Create a new Palette. Drag the Sprite Asset that it'll using as tilemap into Tile Palette. 
- In Hierarchy : Create a new Tilemap : 2D Object > Tilemap > Rectangular  => Scene will show the grid of tiles. 
- Draw Tilemap from Tile Palette. In Tilemap Object, add components : 
    + Tilemap collider 2D: tick Used by Composite.
    + Rigidbody 2D: tick frezze Rotation and Position (optional)
    + Composite Collider 2D : It will combine all colliders become one collider. Avoid player gets stuck when collide.
- You could create more tilemap objects as long as suiable your game requires. (Example: Ground, hard obstacles, soft obstacles, no collision obstacles...).

## Some functions with Tilemap : 
- Find : Tilemap destructibleTiles = GameObject.FindWithTag("DestructiblesTilemap").GetComponent<Tilemap>();   
- When know a world position, you can Get Tile in this position : 
```c# 
    Vector3Int cell = destructibleTiles.WorldToCell(position);  // Convert vector2 to cell in Tilemaps
    TileBase tile = destructibleTiles.GetTile(cell); // Get this tile at position
    if(tile != null){
        destructibleTiles.SetTile(cell, null);   // Remove this tile from destructibleTiles. 
    }
```
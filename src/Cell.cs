using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_of_Life_MonoGame;

public class Cell {
    int x;
    int y;
    int size;

    public bool state = false;
    bool newState = false;

    public Cell(int i, int j, int cellSize, bool cellState) {
        size = cellSize;
        x = i * size;
        y = j * size;
        
        state = cellState;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D sprite, Color color) {
        if (state) // if the cell is alive draw a white square
            spriteBatch.Draw(sprite, new Rectangle(x, y, size, size), color);
    }

    public void SetState(bool setState) => state = setState;

    public bool GetState() => state;

    public void UpdateNewState(int cellsAround) {
        if (state == false && cellsAround == 3)
            newState = true;
        else if (state == true && (cellsAround < 2 || cellsAround > 3) )
            newState = false;
        else
            newState = state;
    }

    public void Update() => state = newState;
}
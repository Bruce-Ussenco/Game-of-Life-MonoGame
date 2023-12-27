using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_of_Life_MonoGame;

public class Cell {
    int x;
    int y;
    int size;

    bool state    = false;
    bool newState = false;

    public Cell(int i, int j, int cellSize, bool cellState) {
        size = cellSize;
        x = i * size;
        y = j * size;
        
        state = cellState;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D sprite) {
        if (state) // if the cell is alive draw a white square
            spriteBatch.Draw(sprite, new Rectangle(x, y, size, size), Color.White);
    }

    public void SetNewState(Cell[][] grid) {

    }

    public void Update() => state = newState;
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_of_Life_MonoGame;

public class Game1 : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D cellTexture;

    private Cell[][] cells;

    public int rows = 32;
    public int cols = 32;

    private int cellSize = 16;

    private bool paused = true;

    public Game1() {
        _graphics = new GraphicsDeviceManager(this);

        _graphics.PreferredBackBufferWidth  = cols * cellSize;
        _graphics.PreferredBackBufferHeight = rows * cellSize;
        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        cells = GenerateCells(rows, cols, cellSize, -1);

        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        cellTexture = new Texture2D(GraphicsDevice, 1, 1); // texture to the cells
        cellTexture.SetData(new[] { Color.White });        // one white pixel
    }

    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        //if (Keyboard.GetState().IsKeyUp(Keys.Space))
        //    paused = !paused;
        
        KeyHelper.GetState();

        if (KeyHelper.IsKeyPressed(Keys.Space))
            paused = !paused;

        if (!paused) {
            for (int j = 0; j < rows; j++) {
            for (int i = 0; i < cols; i++) {
                cells[i][j].SetNewState(CountCellAround(i, j));
            }
            }

            for (int j = 0; j < rows; j++) {
            for (int i = 0; i < cols; i++) {
                cells[i][j].Update();
            }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        for (int j = 0; j < rows; j++) {
        for (int i = 0; i < cols; i++) {
            cells[i][j].Draw(_spriteBatch, cellTexture);
        }
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    // if the intState == 0, the state will be false
    // if == 1, the state will be true
    // else, the state will be random
    private static Cell[][] GenerateCells(int rows, int cols, int size, int intState) {
        Random rnd = new Random();

        Cell[][] array = new Cell[cols][];

        for (int i = 0; i < cols; i++) {
            Cell[] subArray = new Cell[rows];

            for (int j = 0; j < rows; j++) {
                bool state = false;

                if (intState == 0)
                    state = false;
                else if (intState == 1)
                    state = true;
                else
                    state = rnd.NextDouble() < 0.5;
                
                subArray[j] = new Cell(i, j, size, state);
            }

            array[i] = subArray;
        }

        return array;
    }

    private int CountCellAround(int cellI, int cellJ) {
        int sum = 0;
  
        for (int i = -1; i < 2; i++) {
        for (int j = -1; j < 2; j++) {
            int col = (cellI + i + cols) % cols;
            int row = (cellJ + j + rows) % rows;
            sum += cells[col][row].state ? 1 : 0;
        }
        }
        
        sum -= cells[cellI][cellJ].state  ? 1 : 0;
        return sum;
    }
}

using Microsoft.Xna.Framework.Input;

namespace Game_of_Life_MonoGame;

public class KeyHelper {
    static KeyboardState currentKeyState;
    static KeyboardState previousKeyState;

    public static KeyboardState GetState() {
        previousKeyState = currentKeyState;
        currentKeyState = Keyboard.GetState();
        return currentKeyState;
    }

    public static bool IsKeyDown(Keys key) {
        return currentKeyState.IsKeyDown(key);
    }

    public static bool IsKeyPressed(Keys key) {
        return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
    }
}
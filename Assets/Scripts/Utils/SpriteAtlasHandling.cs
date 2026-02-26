using UnityEngine;
using UnityEngine.U2D;

public static class SpriteAtlasHandling
{
    public static Sprite GetSpriteFromAtlas(SpriteAtlas atlas, string spriteName)
    {
        return atlas.GetSprite(spriteName);
    }
}

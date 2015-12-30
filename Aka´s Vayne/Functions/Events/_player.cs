
using SharpDX;

namespace Aka_s_Vayne_reworked.Functions.Events
{
    class _player
    {
        public static bool UltActive()
        {
            return (Variables._Player.HasBuff("vaynetumblefade") && !Functions.other.UnderEnemyTower((Vector2) Variables._Player.Position));
        }
    }
}

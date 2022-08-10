using System.Drawing;

namespace MiniER.Model
{
    public interface IDrawable
    {
        /// <summary>
        /// If coordinates in object -> return object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        object MouseGet(int x, int y, SettingsDTO settings);

        /// <summary>
        /// Draw object
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g, SettingsDTO settings);
    }
}

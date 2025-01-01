using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MX_Simulator_Track_Scaler
{
    internal class ScaleInfo
    {
        public static int terrainScaleNumber;
        public static decimal terrainScale;
        public static decimal minHeight;
        public static decimal maxHeight;

        public static decimal scalarInput;
        public static decimal multiplier;

        public static bool scaleVerticalValues = true;
        public static bool isByFactorMethod = false;
        public static bool isUnchangingScale = false;

        public static int GetTerrainImageWidth()
        {
            return (int) (Math.Pow(2, terrainScaleNumber + 1)) + 1;
        }

        public static int GetTilemapRowColumnCount()
        {
            return (int)(Math.Pow(2, terrainScaleNumber));
        }
    }
}

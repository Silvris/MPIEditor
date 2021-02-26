using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace MPIEditor
{
    class HelperFunctions
    {
        public static Brush GetBrushFromHex(string hexColor)
        {
            BrushConverter bc = new BrushConverter();
            Brush newBrush = (Brush)bc.ConvertFrom(hexColor);
            return newBrush;
        }
    }
}

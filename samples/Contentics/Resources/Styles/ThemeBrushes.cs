using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contentics.Resources.Styles
{
    static class ThemeBrushes
    {
        public static Color Background => Color.FromArgb("#F7F6FA");
        public static Color White => Color.FromArgb("#FFFFFF");

        public static Color Dark => Color.FromArgb("#292932");
        public static Color Gray100 => Color.FromArgb("#95959D");
        public static Color Purple10 => Color.FromArgb("#7455F6");
        public static Color Danger => Color.FromArgb("#FF8B77");
        public static Color Purple20 => Color.FromArgb("#A5A7FF");
        public static Color Purple30 => Color.FromArgb("#B9AAFA");
        public static Color Purple40 => Color.FromArgb("#E3DDFD");
        
        public static Color Green => Color.FromArgb("#43E2C6");
        public static Color Pink10 => Color.FromArgb("#FFE1D6");
        public static Color Pink20 => Color.FromArgb("#FF8B77");
        public static Color Green10 => Color.FromArgb("#D9F9F4");
        public static Color Green20 => Color.FromArgb("#43E2C6");
        

        //Shadows
        public static Color DarkShadow => Color.FromRgba(41, 41, 50, (int)(0.1 * 255));
        public static Color DarkShadow10 => Color.FromRgba(72, 65, 104, (int)(0.1 * 255));
    }
}

using MauiReactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingPuzzle.Resources.Styles
{
    internal static class ThemedControls
    {
        public static Label Label(object content)
        {
            return new Label(content.ToString())
                .FontFamily("OpenSansRegular")
                .FontSize(18);
        }

        
    }
}

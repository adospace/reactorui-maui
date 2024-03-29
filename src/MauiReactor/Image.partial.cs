﻿using Microsoft.Maui.Controls;

namespace MauiReactor;

public partial class Image
{
    public Image(string imageSource) => this.Source(imageSource);
    public Image(Uri imageSource) => this.Source(imageSource);
}

public partial class Component
{
    public static Image Image(string imageSource) =>
        new Image().Source(imageSource);

    public static Image Image(Uri imageSource) =>
        new Image().Source(imageSource);
}

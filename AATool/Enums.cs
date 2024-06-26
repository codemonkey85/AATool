﻿namespace AATool
{
    public enum TrackerSource
    {
        ActiveInstance,
        CustomSavesPath,
        SpecificWorld,
    }

    public enum ProgressFilter
    {
        Solo,
        Combined,
    }

    //used for sftp
    public enum SyncState
    {
        Ready,
        Connecting,
        ServerProperties,
        LastAutoSave,
        Advancements,
        Statistics,
    }

    //ui and rendering
    public enum HorizontalAlign { Center, Left, Right }
    public enum VerticalAlign { Center, Top, Bottom }
    public enum FlowDirection { LeftToRight, RightToLeft, TopToBottom, BottomToTop }
    public enum SizeMode { Absolute, Relative }
    public enum DrawMode { All, ThisOnly, ChildrenOnly, None }
    public enum ControlState { Released, Hovered, Pressed, Disabled }
    public enum FrameType { Normal, Goal, Challenge, Statistic }
    public enum Layer { Main, Glow, Fore }
    public enum Ease { Back, Bounce, Circular, Cubic, Elastic, Exponential, Quadratic, Quartic, Quintic, Sinusoidal }
    public enum WindowSnap { Centered, Remember, TopLeft, TopRight, BottomLeft, BottomRight }
}

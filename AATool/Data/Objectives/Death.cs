﻿using System.Collections.Generic;
using System.Xml;
using AATool.Utilities;

namespace AATool.Data.Objectives
{
    public class Death : Objective
    {
        public bool DoubleHeight { get; private set; }
        public float LightLevel  { get; private set; }

        public bool Glows => this.LightLevel > 0;

        public IEnumerable<string> Messages { get; internal set; }

        public override string FullStatus => this.Name;
        public override string TinyStatus => this.ShortName;

        public override bool IsComplete() => base.IsComplete() || this.ManuallyChecked;

        public Death(XmlNode node) : base (node)
        {
            this.DoubleHeight = XmlObject.Attribute(node, "double_height", false);
            this.LightLevel = XmlObject.Attribute(node, "light_level", 0f);
            this.Messages = XmlObject.Attribute(node, "messages", "").Split(',');
            this.CanBeManuallyChecked = true;
        }

        public void Clear()
        {
            this.ManuallyChecked = false;
        }

        public override void ToggleManualCheck()
        {
            base.ToggleManualCheck();
            Tracker.Deaths.UpdateTotal();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace PhiClient.UI
{
    class TextFieldWidget : Displayable
    {
        public string text = "";
        public Action<string> onChange;

        public TextFieldWidget(string text, Action<string> onChange)
        {
            this.text = text;
            this.onChange = onChange;
        }

        public override void Draw(Rect inRect)
        {
            text = Widgets.TextField(inRect, text);
            onChange(text);
        }
    }
}

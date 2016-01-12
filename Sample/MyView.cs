using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Sample
{
    public class MyView : View
    {
        private Paint paint;
        private float DENSITY = Resources.System.DisplayMetrics.Density;
        public MyView(Context context) : this(context, null)
        {
        }

        public MyView(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public MyView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            SetLayerType(LayerType.Hardware, null);
            paint = new Paint();
        }
        protected override void OnDraw(Canvas canvas)
        {
            int canvasWidth = canvas.Width;
            int canvasHeight = canvas.Height;
            paint.SetStyle(Paint.Style.Fill);
            paint.Color=color(0xffff0000);
            canvas.DrawRect(dp2px(16), dp2px(16), dp2px(50), dp2px(50), paint);
            paint.Color= color(0xffcc9900);
            canvas.DrawRect(dp2px(100), dp2px(16), dp2px(133), dp2px(50), paint);
            paint.Color = color(0xff00ff00);
            canvas.DrawRect(dp2px(16), dp2px(106), dp2px(50), dp2px(140), paint);
            paint.Color = color(0xff6600ff);
            canvas.DrawRect(dp2px(100), dp2px(106), dp2px(133), dp2px(140), paint);

            canvas.Translate(canvasWidth / 2, canvasHeight / 2);
            paint.StrokeWidth=3;
            paint.SetStyle(Paint.Style.Stroke);
            paint.TextSize=dp2px(18);
            paint.Color= color(0xffffff00);
            canvas.DrawText("Custom View", -dp2px(53), dp2px(3), paint);
        }
        private int dp2px(int dp)
        {
            return Java.Lang.Math.Round(dp * DENSITY);
        }
        public Color color(uint color)
        {
            byte R, G, B;
            R = (byte)(color >> 10);
            G = (byte)(color >> 5 & 0x1f);
            B = (byte)(color & 0x1f);
            Color c = Color.Argb(100, R, G, B);
            return c;
        }
    }
}
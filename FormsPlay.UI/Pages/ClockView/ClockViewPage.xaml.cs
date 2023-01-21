using FormsPlay.Core.ViewModels;
using MvvmCross.Forms.Views;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsPlay.UI.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClockViewPage : MvxContentPage<ClockViewViewModel>
    {
		public ClockViewPage ()
		{
			InitializeComponent ();

            Device.StartTimer(TimeSpan.FromSeconds(1 / 60f), () =>
            {
                canvasView.InvalidateSurface();
                return true;
            });
        }

        private SKPaint GetPaintColor(SKPaintStyle style, string hexColor, float strokeWidth = 0, SKStrokeCap cap = SKStrokeCap.Round, bool IsAntialias = true)
        {
            return new SKPaint
            {
                Style = style,
                StrokeWidth = strokeWidth,
                Color = string.IsNullOrWhiteSpace(hexColor) ? SKColors.White : SKColor.Parse(hexColor),
                StrokeCap = cap,
                IsAntialias = IsAntialias
            };
        }

        SKPath path = new SKPath();
        float arcLength = 105;
        private void canvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            SKPaint strokePaint = GetPaintColor(SKPaintStyle.Stroke, null, 10, SKStrokeCap.Square);
            SKPaint dotPaint = GetPaintColor(SKPaintStyle.Fill, "#DE0469");
            SKPaint hrPaint = GetPaintColor(SKPaintStyle.Stroke, "#6EDE8A", 5, SKStrokeCap.Round);
            SKPaint secPaint = GetPaintColor(SKPaintStyle.Stroke, "#F7B267", 1, SKStrokeCap.Square);
            SKPaint minPaint = GetPaintColor(SKPaintStyle.Stroke, "#DFF4E5", 2, SKStrokeCap.Square);
            SKPaint bgPaint = GetPaintColor(SKPaintStyle.Fill, "#343739");

            canvas.Clear();

            SKRect arcRect = new SKRect(10, 10, info.Width - 10, info.Height - 10);
            SKRect bgRect = new SKRect(25, 25, info.Width - 25, info.Height - 25);
            canvas.DrawOval(bgRect, bgPaint);

            strokePaint.Shader = SKShader.CreateLinearGradient(
                               new SKPoint(arcRect.Left, arcRect.Top),
                               new SKPoint(arcRect.Right, arcRect.Bottom),
                               new SKColor[] { SKColor.Parse("#DE0469"), SKColors.Transparent },
                               new float[] { 0, 1 },
                               SKShaderTileMode.Repeat);

            path.ArcTo(arcRect, 45, arcLength, true);
            //canvas.DrawPath(path, strokePaint);

            canvas.Translate(info.Width / 2, info.Height / 2);
            canvas.Scale(info.Width / 200f);

            //canvas.Save();
            //canvas.RotateDegrees(240);
            //canvas.DrawCircle(0, -75, 2, dotPaint);
            //canvas.Restore();

            DateTime dateTime = DateTime.Now;

            //Draw hour hand
            canvas.Save();
            canvas.RotateDegrees(30 * dateTime.Hour + dateTime.Minute / 2f);
            canvas.DrawLine(0, 2, 0, -40, hrPaint);
            canvas.Restore();

            //Draw minute hand
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Minute + dateTime.Second / 10f);
            canvas.DrawLine(0, 2, 0, -60, minPaint);
            canvas.Restore();

            //Seconds
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Second);
            canvas.DrawLine(0, 2, 0, -80, secPaint);
            canvas.Restore();
        }

    }
}
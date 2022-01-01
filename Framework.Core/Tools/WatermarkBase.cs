using System.Drawing;

namespace Framework.Core.Tools
{
    public class WatermarkBase
    {

        #region Private Fields
        private float opacity = 0.5f;
        private WatermarkPosition position = WatermarkPosition.Absolute;
        private RotateFlipType rotateFlip = RotateFlipType.RotateNoneFlipNone;
        private Font font = new Font("Arial", 10);
        private Color fontColor = Color.Black;
        private float scaleRatio = 0.5f;
        private Color transparentColor = Color.Empty;
        #endregion
        public Image WatermarkImage { get; set; }
        private string OrginalImageFileName { get; set; }
        public Image Image { get; set; }
        public WatermarkPosition Position { get => position; set => position = value; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public float Opacity { get => opacity; set => opacity = value; }
        public Color TransparentColor { get => transparentColor; set => transparentColor = value; }
        public RotateFlipType RotateFlip { get => rotateFlip; set => rotateFlip = value; }
        public float ScaleRatio { get => scaleRatio; set => scaleRatio = value; }
        public Font Font { get => font; set => font = value; }
        public Color FontColor { get => fontColor; set => fontColor = value; }
        public WatermarkBase(Image image, Image watermarkImage)
        {
            Image = image;
            WatermarkImage = watermarkImage;
        }
        public WatermarkBase(Image image)
        {
            Image = image;
        }
        public WatermarkBase(string imageFile)
        {
            Image = Image.FromFile(imageFile);
        }
        public WatermarkBase(string imageFile, string watermarkImageFile)
        {
            Image = Image.FromFile(imageFile);
            WatermarkImage = Image.FromFile(watermarkImageFile);
        }
    }

    public enum WatermarkPosition
    {
        Absolute,
        TopLeft,
        TopRight,
        TopMiddle,
        BottomLeft,
        BottomRight,
        BottomMiddle,
        MiddleLeft,
        MiddleRight,
        Center
    }
}


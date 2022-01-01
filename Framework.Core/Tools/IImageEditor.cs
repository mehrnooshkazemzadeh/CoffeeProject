using System.Drawing;
using System.IO;

namespace Framework.Core.Tools
{
    public interface IImageEditor
    {
         Image Image { get; set; }
        Image GetThumbnail(int width);
        Image GetThumbnail(int width, int height);
        byte[] GetThumbnailImage(int width);
        byte[] GetThumbnailImage(int width, int height);
        Stream GetThumbnailStream(int width);
        Stream GetThumbnailStream(int width, int height);


        Image SetWatermark(WatermarkBase waterMarkBase);
        void SaveWatermark(WatermarkBase waterMarkBase, string targetFile);
        Image SetWatermarkText(WatermarkBase watermarkBase, string text);


        Image LoadImage(string filename);
        Image LoadImage(byte[] image);
        Image LoadImage(Stream stream);

        void SaveThumbnailToFile(int width, string imageFile, string targetFile);
        void SaveThumbnailToFile(int width, int height, string imageFile, string targetFile);

        Image RotateImage(string imageFile, RotateFlipType rotateFlip);
        string RotateImage(string imageFile, RotateFlipType rotateFlip,string targetFile);

    }
}

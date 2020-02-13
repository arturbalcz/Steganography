using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LSB_Steganography
{
    class ImageUtils
    {
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image resultImage = Image.FromStream(ms);
            return resultImage;
        }

        public static Bitmap ColorMatrixToImage(Color[,] colorMatrix)
        {
            Bitmap resultBitmap = new Bitmap(colorMatrix.GetLength(0), colorMatrix.GetLength(1));

            for (int x = 0; x < colorMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < colorMatrix.GetLength(1); y++)
                {
                    resultBitmap.SetPixel(x, y, colorMatrix[x,y]);
                }
            }

            return resultBitmap; 
        }

        public static Color[,] GetBitMapColorMatrix(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            Bitmap bitmap = (Bitmap) imageIn;

            int hight = bitmap.Height;
            int width = bitmap.Width;

            Color[,] colorMatrix = new Color[width, hight];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i,j] = bitmap.GetPixel(i, j);
                }
            }
            return colorMatrix;
        }
    }
}

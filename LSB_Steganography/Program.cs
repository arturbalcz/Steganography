using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSB_Steganography
{
    class Program
    {
        static void Main(string[] args)
        {
            String message = "qwertyuiop"; 

            Color[,] colorMatrix = ImageUtils.GetBitMapColorMatrix(Image.FromFile(@"sample.bmp"));
            Color[,] encrypted = Steganography.Encrypt(Encoding.UTF8.GetBytes(message), colorMatrix);

            Bitmap output = ImageUtils.ColorMatrixToImage(encrypted);
            output.Save(@"output.bmp", ImageFormat.Bmp);

            Color[,] readed = ImageUtils.GetBitMapColorMatrix(Image.FromFile(@"output.bmp"));
            byte[] decrypted = Steganography.Decrypt(readed); 
            string decryptedString = System.Text.Encoding.UTF8.GetString(decrypted, 0, message.Length);

            Console.WriteLine(decryptedString); 
        }
    }
}

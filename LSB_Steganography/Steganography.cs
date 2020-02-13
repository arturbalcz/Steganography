using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSB_Steganography
{
    class Steganography
    {
        private static byte setByteLSB(byte b, bool value)
        {
            byte new_value; 

            if (value)
            {
                new_value = (byte)(b & 0xFE | 1);
            }
            else
            {
                new_value = (byte)(b & 0xFE | 0); 
            }

            return new_value; 

        }

        private static bool getLSB(byte b)
        {
            return (b & 1) != 0;
        }

        private static byte convertToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }

        public static Color[,] Encrypt(byte[] text, Color[,] colorMatrix)
        {
            BitArray message = new BitArray(text); 
            int height = colorMatrix.GetLength(0);
            int width = colorMatrix.GetLength(1);

            Color[,] result = new Color[width,height];

            int message_iterator = 0; 

            for(int i = 0; i < height; i++)
            {
                for(int j=0; j < width; j++)
                {
                    byte new_color_r = colorMatrix[i, j].R;
                    byte new_color_g = colorMatrix[i, j].G;
                    byte new_color_b = colorMatrix[i, j].B;

                    if (message_iterator < message.Length)
                    {
                        new_color_r = setByteLSB(colorMatrix[i, j].R, message.Get(message_iterator++));
                    }

                    if (message_iterator < message.Length)
                    {
                        new_color_g = setByteLSB(colorMatrix[i, j].G, message.Get(message_iterator++));
                    }

                    if (message_iterator < message.Length)
                    {
                        new_color_b = setByteLSB(colorMatrix[i, j].B, message.Get(message_iterator++));
                    }

                    Color new_color = Color.FromArgb(new_color_r, new_color_g, new_color_b);
                    result[i, j] = new_color;
                }

            }

            return result; 
        }

        public static byte[] Decrypt(Color[,] colorMatrix)
        { 
            int height = colorMatrix.GetLength(0);
            int width = colorMatrix.GetLength(1);

            byte[] result = new byte[width * height * 3 / 8];

            int byte_iterator = 0;
            BitArray bits = new BitArray(width * height * 3);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {   
                    bits.Set(byte_iterator++, getLSB(colorMatrix[i, j].R)); 
                    bits.Set(byte_iterator++, getLSB(colorMatrix[i, j].G));
                    bits.Set(byte_iterator++, getLSB(colorMatrix[i, j].B));

                }
            }

            byte_iterator = 0; 

            for(int i=0; i < result.Length; i++)
            {
                BitArray one_byte_array = new BitArray(8); 
                for(int j = 0; j < 8; j++)
                {
                    one_byte_array.Set(j, bits[byte_iterator++]);
                }

                result[i] = convertToByte(one_byte_array);
            }

            return result;
        }

    }
}

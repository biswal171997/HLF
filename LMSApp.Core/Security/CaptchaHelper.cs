using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LMSApp.Core
{
    public class CaptchaHelper
    {
        
            ///  <summary>
            /// This method is used to generate random numbers with specified digits
            ///  </summary>
            ///  <param name="VcodeNum"> The parameters are Random number digits </param>
            ///  <returns> returns a random number string </returns>
            private string RndNum(int VcodeNum)
            {
                // The set of characters that can be displayed by the verification code
                string Vchar = "2,3,4,5,6,7,8,9," +
                    "A,B,C,D,E,F,G,H,J,K,L,M ,N,P,P,Q" +
                    ",R,S,T,U,V,W,X,Y,Z";
                string[] VcArray = Vchar.Split(new Char[] { ',' }); // Split into an array
                string code = ""; // The random number generated
                int temp = -1; // Record the last random value, try to avoid producing several identical random numbers

                Random rand = new Random();
                //A simple algorithm is used to ensure that the random number is different
                for (int i = 1; i < VcodeNum + 1; i++)
                {
                    if (temp != -1)
                    {
                        rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks)); // Initialize random class
                    }
                    int t = rand.Next(32); // Get random number
                    if (temp != -1 && temp == t)
                    {
                        return RndNum(VcodeNum); // If the obtained random number is repeated, then recursively call
                    }
                    temp = t; // Record the random number generated this time
                    code += VcArray[t]; //The number of random numbers plus one
                }
                return code;
            }

            ///  <summary>
            /// This method is to write the generated random number into the image file
            ///  < /summary>
            ///  <param name="code"> code is a random number</param>
            ///  <param name="numbers"> Generate digits (default 4 digits) </param>
            public MemoryStream Create(out string code, int numbers = 4)
            {
                code = RndNum(numbers);
                Bitmap Img = null;
                Graphics g = null;
                MemoryStream ms = null;
                Random random = new Random();
                // Captcha color collection
                Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

                // Verification code font collection
                string[] fonts = { " Verdana ", " Microsoft Sans Serif ", " Comic Sans MS ", " Arial " };


                // Define the size of the image and generate an example of the image
                Img = new Bitmap((int)code.Length * 18, 32);

                g = Graphics.FromImage(Img); // Generate a new Graphics object from the Img object

                g.Clear(Color.White); // Set the background to white
           
            g.FillRectangle(new HatchBrush(HatchStyle.BackwardDiagonal, Color.LightGray, Color.Transparent), g.ClipBounds);
           // g.FillRectangle(new HatchBrush(HatchStyle.ForwardDiagonal, Color.FromArgb(255, 0, 0, 0), Color.Transparent), g.ClipBounds);

            // In a random position Draw the background point
            //for (int i = 0; i < 100; i++)
            //{
            //    int x = random.Next(Img.Width);
            //    int y = random.Next(Img.Height);
            //    int x1 = 100;
            //    int y1 = 100;
            //    int x2 = 500;
            //    int y2 = 100;
            //   // g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            //    g.DrawLine(new Pen(Color.LightGray, 1), x1, y1, x2, y2);
            //}
            //The verification code is drawn in g
            for (int i = 0; i < code.Length; i++)
                {
                    int cindex = random.Next(7); // random color index value
                    int findex = random.Next(4); // random font index value
                    Font f = new Font(fonts[findex], 15, FontStyle.Bold); // Font
                    Brush b = new SolidBrush(c[0]); // color
                    int ii = 4;
                    if ((i + 1) % 2 == 0) // control verification code is not at the same height
                    {
                        ii = 2;
                    }
                    g.DrawString(code.Substring(i, 1), f, b, 3 + (i * 12), ii); // Draw a verification character
                }
                ms = new MemoryStream(); // Generate a memory stream object
                Img.Save(ms, ImageFormat.Jpeg); // Save this image to the stream in the format of a Png image file

                // Recycle resources
                g.Dispose();
                Img.Dispose();
                return ms;
            }
        
    }
}

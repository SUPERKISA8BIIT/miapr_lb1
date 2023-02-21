using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miapr_lb1
{
    public struct Pixelcolor
    {

        public byte red;
        public byte green;
        public byte blue;
        public byte alpha;

        public byte[] Core => new byte[] { blue, green, red, alpha };

        public Pixelcolor(byte red, byte green, byte blue, byte alpha)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        public static class PixelColors
        {
            public   static Pixelcolor[] Empty = new Pixelcolor[] {Blue, Black, Green, Gavno, Blyadina, Bulbulbul, Penis, Solsberry, Davalka, Hui, Blyadina, ILovePasha, Bulma, Bulbulbul, Gav, Solsberry, Pink};    
            public static Pixelcolor Black => new Pixelcolor(0, 0, 0, 255);
            public static Pixelcolor Red => new Pixelcolor(255, 0, 0, 255);
            public static Pixelcolor Green => new Pixelcolor(0, 255, 0, 255);
            public static Pixelcolor Blue => new Pixelcolor(0, 0, 255, 255);
            public static Pixelcolor White => new Pixelcolor(255, 255, 255, 255);
            public static Pixelcolor Meow => new Pixelcolor(21, 38, 66, 255);
            public static Pixelcolor Gavno => new Pixelcolor(255, 137, 3, 255);
            public static Pixelcolor Zalupa => new Pixelcolor(72, 79, 89, 255);
            public static Pixelcolor Penis => new Pixelcolor(79, 17, 50, 255);
            public static Pixelcolor Her => new Pixelcolor(227, 113, 174, 255);
            public static Pixelcolor Davalka => new Pixelcolor(46, 28, 37, 255);
            public static Pixelcolor Hui => new Pixelcolor(28, 46, 35, 255);
            public static Pixelcolor Blyadina => new Pixelcolor(3, 255, 226, 255);
            public static Pixelcolor ILovePasha => new Pixelcolor(106, 117, 110, 255);
            public static Pixelcolor Bulma => new Pixelcolor(184, 90, 24, 255);
            public static Pixelcolor Bulbulbul => new Pixelcolor(117, 168, 255, 255);
            public static Pixelcolor Gav => new Pixelcolor(215, 230, 18, 255);
            public static Pixelcolor Solsberry => new Pixelcolor(142, 145, 97, 255);
            public static Pixelcolor Pink => new Pixelcolor(196, 198, 217, 255);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace nModelPartsInfo
{
    class RGBA
    {
        public byte red { get; set; }
        public byte green { get; set; }
        public byte blue { get; set; }
        public byte alpha { get; set; }

        public RGBA(byte r, byte g, byte b, byte a)
        {
            red = r;
            green = g;
            blue = b;
            alpha = a;
        }
    }
    class cColorInfo
    {
        //this is actually just an RGBA color x2
        public RGBA mColorA;
        public RGBA mColorB;

        public cColorInfo(BinaryReader br)
        {
            mColorA = new RGBA(br.ReadByte(), br.ReadByte(), br.ReadByte(), br.ReadByte());
            mColorB = new RGBA(br.ReadByte(), br.ReadByte(), br.ReadByte(), br.ReadByte());
        }

        public cColorInfo()
        {
            mColorA = new RGBA(255, 255, 255, 255);
            mColorB = new RGBA(255, 255, 255, 255);
        }

        public void Export(BinaryWriter bw)
        {
            bw.Write(mColorA.red);
            bw.Write(mColorA.green);
            bw.Write(mColorA.blue);
            bw.Write(mColorA.alpha);

            bw.Write(mColorB.red);
            bw.Write(mColorB.green);
            bw.Write(mColorB.blue);
            bw.Write(mColorB.alpha);
        }
    }
}

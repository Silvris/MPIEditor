using MPIEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;

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
    class ColorBrush : INotifyPropertyChanged
    {
        private Brush _brush;
        public Brush colorBrush
        {
            get
            {
                return _brush;
            }
            set
            {
                _brush = value;
                OnPropertyChanged("colorBrush");
            }
        }
        public ColorBrush(Brush nBrush)
        {
            colorBrush = nBrush;
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    class cColorInfo
    {
        //this is actually just an RGBA color x2
        public RGBA mColorA { get; set; }
        public RGBA mColorB { get; set; }
        public ColorBrush mColorABrush { get; set; }
        public ColorBrush mColorBBrush { get; set; }

        public cColorInfo(BinaryReader br)
        {
            mColorA = new RGBA(br.ReadByte(), br.ReadByte(), br.ReadByte(), br.ReadByte());
            mColorB = new RGBA(br.ReadByte(), br.ReadByte(), br.ReadByte(), br.ReadByte());
            mColorABrush = new ColorBrush(HelperFunctions.GetBrushFromHex("#FFFFFF"));
            mColorBBrush = new ColorBrush(HelperFunctions.GetBrushFromHex("#FFFFFF"));
            UpdateABrush();
            UpdateBBrush();
        }
        public void UpdateABrush()
        {
            byte[] hexArray = { mColorA.red, mColorA.green, mColorA.blue };
            string hexBrush = "#" + BitConverter.ToString(hexArray).Replace("-","");
            mColorABrush.colorBrush = HelperFunctions.GetBrushFromHex(hexBrush);
        }
        public void UpdateBBrush()
        {
            string hexBrush = "#" + mColorB.red.ToString("X2") + mColorB.green.ToString("X2") + mColorB.blue.ToString("X2");
            mColorBBrush.colorBrush = HelperFunctions.GetBrushFromHex(hexBrush);
        }

        public bool CheckColor()
        {
            bool colorPresent = false;
            while (!colorPresent)
            {
                if(mColorA.red != 255)
                {
                    colorPresent = true;
                }
                if (mColorA.green != 255)
                {
                    colorPresent = true;
                }
                if (mColorA.blue != 255)
                {
                    colorPresent = true;
                }
                if (mColorA.alpha != 255)
                {
                    colorPresent = true;
                }
                if (mColorB.red != 255)
                {
                    colorPresent = true;
                }
                if (mColorB.green != 255)
                {
                    colorPresent = true;
                }
                if (mColorB.blue != 255)
                {
                    colorPresent = true;
                }
                if (mColorB.alpha != 255)
                {
                    colorPresent = true;
                }
                break;
            }
            return colorPresent;
        }

        public cColorInfo()
        {
            mColorA = new RGBA(255, 255, 255, 255);
            mColorB = new RGBA(255, 255, 255, 255);
            mColorABrush = new ColorBrush(HelperFunctions.GetBrushFromHex("#FFFFFF"));
            mColorBBrush = new ColorBrush(HelperFunctions.GetBrushFromHex("#FFFFFF"));
            UpdateABrush();
            UpdateBBrush();
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

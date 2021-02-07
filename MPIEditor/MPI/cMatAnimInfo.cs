using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace nModelPartsInfo
{
    class cMatAnimInfo
    {
        uint MatAnimInfo = 825521721;
        int mMatAnimNo { get; set; }
        int mMatID { get; set; }
        int mMatAnimSlotNo { get; set; }
        bool mIsAutoInterpolate { get; set; }
        bool mIsNoResetPlay { get; set; }

        public cMatAnimInfo(BinaryReader br)
        {
            uint hash = br.ReadUInt32();
            if (hash != MatAnimInfo)
            {
                MessageBox.Show("Error when reading files, file will attempt to load but will likely fail.");
            }
            mMatAnimNo = br.ReadInt32();
            mMatID = br.ReadInt32();
            mMatAnimSlotNo = br.ReadInt32();
            mIsAutoInterpolate = br.ReadBoolean();
            mIsNoResetPlay = br.ReadBoolean();
        }

        public cMatAnimInfo()
        {
            mMatAnimNo = 0;
            mMatID = 0;
            mMatAnimSlotNo = 0;
            mIsAutoInterpolate = false;
            mIsNoResetPlay = false;
        }

        public void Export(BinaryWriter bw)
        {
            bw.Write(MatAnimInfo);
            bw.Write(mMatAnimNo);
            bw.Write(mMatID);
            bw.Write(mMatAnimSlotNo);
            bw.Write(mIsAutoInterpolate);
            bw.Write(mIsNoResetPlay);
        }
    }
}

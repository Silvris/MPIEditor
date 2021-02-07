using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace nModelPartsInfo
{
    class cPartsInfo
    {
        uint PartsInfo = 1224644149;
        int mUniqueID { get; set; }
        int mPartsNo { get; set; }//the VisCon number
        int mGroupID { get; set; }
        int mTagID { get; set; }

        //imported constructor
        public cPartsInfo(BinaryReader br)
        {
            uint hash = br.ReadUInt32();
            if (hash != PartsInfo)
            {
                MessageBox.Show("Error when reading files, file will attempt to load but will likely fail.");
            }
            mUniqueID = br.ReadInt32();
            mPartsNo = br.ReadInt32();
            mGroupID = br.ReadInt32();
            mTagID = br.ReadInt32();
        }

        public cPartsInfo()
        {
            mUniqueID = 0;
            mPartsNo = 0;
            mGroupID = 0;
            mTagID = 0;
        }

        public void Export(BinaryWriter bw)
        {
            bw.Write(PartsInfo);
            bw.Write(mUniqueID);
            bw.Write(mPartsNo);
            bw.Write(mGroupID);
            bw.Write(mTagID);
        }
    }
}

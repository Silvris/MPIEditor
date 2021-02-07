using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace nModelPartsInfo
{
    class cTagInfo
    {
        uint TagInfo = 832037210;
        public int cPartsInfoID { get; set; }//This can apparently be a list????
        
        public cTagInfo(BinaryReader br)
        {
            uint hash = br.ReadUInt32();
            if (hash != TagInfo)
            {
                MessageBox.Show("Error when reading files, file will attempt to load but will likely fail.");
            }
            cPartsInfoID = br.ReadInt32();
        }

        public cTagInfo()
        {
            cPartsInfoID = 0;
        }

        public void Export(BinaryWriter bw)
        {
            bw.Write(TagInfo);
            bw.Write(cPartsInfoID);
        }
    }
}

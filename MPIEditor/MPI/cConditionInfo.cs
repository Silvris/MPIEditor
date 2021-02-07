using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace nModelPartsInfo
{
    class cConditionInfo
    {
        //this one is interesting
        uint ConditionInfo = 1883508046;
        public int mConditionID { get; set; }
        public List<uint> cGroupInfoConditions { get; set; }
        public List<uint> cMatAnimInfoConditions { get; set; }
        public List<cColorInfo> cColorInfoList { get; set; }
        //these lists all have set lengths, but working with them is easier for lists because of Selected Index

        public cConditionInfo(BinaryReader br)
        {
            cGroupInfoConditions = new List<uint>();
            cMatAnimInfoConditions = new List<uint>();
            cColorInfoList = new List<cColorInfo>();
            uint hash = br.ReadUInt32();
            if (hash != ConditionInfo)
            {
                MessageBox.Show("Error when reading files, file will attempt to load but will likely fail.");
            }
            for(int i = 0; i < 16; i++)
            {
                cGroupInfoConditions.Add(br.ReadUInt32());
            }
            for (int i = 0; i < 16; i++)
            {
                cMatAnimInfoConditions.Add(br.ReadUInt32());
            }
            for (int i = 0; i < 48; i++)
            {
                uint unknValue = br.ReadUInt32();
                //we just need to skip through here, but it is useful for finding if it ever is used
                //it might just be that it uses that many for MatAnim
                //you can definitely fit 64 different material animations into a timl
                if(unknValue != 0)
                {
                    MessageBox.Show("UnknValue not 0 at i = "+(i+32)+".");
                }
            }
            //now read cColorInfo
            for(int i = 0; i < 16; i++)
            {
                cColorInfoList.Add(new cColorInfo(br));
            }
            //finally condition ID
            mConditionID = br.ReadInt32();
        }

        public cConditionInfo()
        {
            cGroupInfoConditions = new List<uint>(16);
            cMatAnimInfoConditions = new List<uint>(16);
            cColorInfoList = new List<cColorInfo>(16);
            mConditionID = 0;
        }

        public void Export(BinaryWriter bw)
        {
            bw.Write(ConditionInfo);//hash datatype
            for (int i = 0; i < 16; i++)
            {
                bw.Write(cGroupInfoConditions[i]);
            }
            for (int i = 0; i < 16; i++)
            {
                bw.Write(cMatAnimInfoConditions[i]);
            }
            for (int i = 0; i < 16; i++)
            {
                cColorInfoList[i].Export(bw);
            }
            bw.Write(mConditionID);
        }
    }
}

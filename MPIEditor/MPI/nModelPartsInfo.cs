using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace nModelPartsInfo
{
    class nModelPartsInfo
    {
        public List<cPartsInfo> cPartsInfoList { get; set; }
        public List<cGroupInfo> cGroupInfoList { get; set; }
        public List<cConditionInfo> cConditionInfoList { get; set; }
        public List<cMatAnimInfo> cMatAnimInfoList { get; set; }
        bool isUseColorInfo;
        bool isUseMatAnimInfo;


        uint IceborneMark = 403247105;
        uint version = 9;
        byte[] MPIMagic = { 77, 80, 73, 0 };

        public nModelPartsInfo(BinaryReader br)
        {
            cPartsInfoList = new List<cPartsInfo>();
            cGroupInfoList = new List<cGroupInfo>();
            cConditionInfoList = new List<cConditionInfo>();
            cMatAnimInfoList = new List<cMatAnimInfo>();
            uint IBMark = br.ReadUInt32();
            uint ver = br.ReadUInt32();
            if(ver != version)
            {
                MessageBox.Show("File is not of the correct version, or is otherwise incorrect.");
                return;
            }
            uint magic = br.ReadUInt32();//if version is right this should be too
            uint cPartsInfoCount = br.ReadUInt32();
            for (int i = 0; i < cPartsInfoCount; i++)
            {
                cPartsInfo partsInfo = new cPartsInfo(br);
                cPartsInfoList.Add(partsInfo);
            }
            uint cGroupInfoCount = br.ReadUInt32();
            for (int i = 0; i < cGroupInfoCount; i++)
            {
                cGroupInfo groupInfo = new cGroupInfo(br);
                cGroupInfoList.Add(groupInfo);
            }
            uint cConditionInfoCount = br.ReadUInt32();
            for(int i = 0; i < cConditionInfoCount; i++)
            {
                cConditionInfo conditionInfo = new cConditionInfo(br);
                cConditionInfoList.Add(conditionInfo);
            }
            uint cMatAnimInfoCount = br.ReadUInt32();
            for(int i = 0; i < cMatAnimInfoCount; i++)
            {
                cMatAnimInfo matAnimInfo = new cMatAnimInfo(br);
                cMatAnimInfoList.Add(matAnimInfo);
            }
            isUseColorInfo = br.ReadBoolean();
            isUseMatAnimInfo = br.ReadBoolean();
        }

        public nModelPartsInfo()
        {
            cPartsInfoList = new List<cPartsInfo>();
            cGroupInfoList = new List<cGroupInfo>();
            cConditionInfoList = new List<cConditionInfo>();
            cMatAnimInfoList = new List<cMatAnimInfo>();
            isUseColorInfo = false;
            isUseMatAnimInfo = false;
        }
    }
}

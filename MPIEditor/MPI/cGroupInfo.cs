using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace nModelPartsInfo
{
    class cGroupInfo : INotifyPropertyChanged
    {
        public ObservableCollection<cTagInfo> mTagInfoList { get; set; }
        uint GroupInfo = 2023676734;

        public event PropertyChangedEventHandler PropertyChanged;

        public cGroupInfo(BinaryReader br)
        {
            mTagInfoList = new ObservableCollection<cTagInfo>();
            uint hash = br.ReadUInt32();
            if (hash != GroupInfo)
            {
                MessageBox.Show("Error when reading files, file will attempt to load but will likely fail.");
            }
            uint TagInfoCount = br.ReadUInt32();
            for(int i = 0; i < TagInfoCount; i++)
            {
                cTagInfo Tag = new cTagInfo(br);
                mTagInfoList.Add(Tag);
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void AddNotify(cTagInfo tag)
        {
            mTagInfoList.Add(tag);
            OnPropertyChanged("mTagInfoList");
        }
        public cGroupInfo()
        {
            mTagInfoList = new ObservableCollection<cTagInfo>();
            cTagInfo Tag = new cTagInfo();
            mTagInfoList.Add(Tag);
            //add one so user doesn't have to on creation of file
        }

        public void Export(BinaryWriter bw)
        {
            bw.Write(GroupInfo);
            bw.Write(mTagInfoList.Count);
            for (int i = 0; i < mTagInfoList.Count; i++)
            {
                mTagInfoList[i].Export(bw);
            }
        }
    }
}

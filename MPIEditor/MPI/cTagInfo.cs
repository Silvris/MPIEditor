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
    class cTagInfo : INotifyPropertyChanged
    {
        uint TagInfo = 832037210;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<UIntObject> cPartsInfoID { get; set; }//This can apparently be a list???? Old me managed to not notice it in the first file I open
        
        public cTagInfo(BinaryReader br)
        {
            cPartsInfoID = new ObservableCollection<UIntObject>();
            uint hash = br.ReadUInt32();
            if (hash != TagInfo)
            {
                MessageBox.Show("Error when reading files, file will attempt to load but will likely fail.");
            }
            int cPartsCount = br.ReadInt32();
            for(int i = 0; i< cPartsCount; i++)
            {
                cPartsInfoID.Add(new UIntObject(br.ReadUInt32()));
            }
            
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void AddNotify(UIntObject obj)
        {
            cPartsInfoID.Add(obj);
            OnPropertyChanged("cPartsInfoID");
        }

        public cTagInfo()
        {
            cPartsInfoID = new ObservableCollection<UIntObject>();
            cPartsInfoID.Add(new UIntObject(0));
        }

        public void Export(BinaryWriter bw)
        {
            bw.Write(TagInfo);
            bw.Write(cPartsInfoID.Count);
            for(int i = 0; i < cPartsInfoID.Count; i++)
            {
                bw.Write(cPartsInfoID[i].value);
            }
        }
    }
}

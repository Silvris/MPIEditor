using Microsoft.Win32;
using MPIEditor.MPI;
using nModelPartsInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MPIEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModelPartsInfo modelPartsInfo;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PopulateTreeView()
        {
            treeView1.Items.Clear();
            for (int i = 0; i < modelPartsInfo.cPartsInfoList.Count; i++)
            {
                TreeViewItem partsItem = new TreeViewItem();
                partsItem.Header = "cPartsInfo " + (i + 1);
                partsItem.Foreground = HelperFunctions.GetBrushFromHex("#AAAAAA");
                List<int> tag = new List<int>();
                tag.Add((int)classEnums.cPartsInfo);
                tag.Add(i);
                partsItem.Tag = tag;
                treeView1.Items.Add(partsItem);
            }
            for (int i = 0; i < modelPartsInfo.cGroupInfoList.Count; i++)
            {
                TreeViewItem groupItem = new TreeViewItem();
                groupItem.Header = "cGroupInfo " + (i + 1);
                groupItem.Foreground = HelperFunctions.GetBrushFromHex("#AAAAAA");
                List<int> tag = new List<int>();
                tag.Add((int)classEnums.cGroupInfo);
                tag.Add(i);
                groupItem.Tag = tag;
                treeView1.Items.Add(groupItem);
            }
            for (int i = 0; i < modelPartsInfo.cConditionInfoList.Count; i++)
            {
                TreeViewItem conditionItem = new TreeViewItem();
                conditionItem.Header = "cConditionInfo " + (i + 1);
                conditionItem.Foreground = HelperFunctions.GetBrushFromHex("#AAAAAA");
                List<int> tag = new List<int>();
                tag.Add((int)classEnums.cConditionInfo);
                tag.Add(i);
                conditionItem.Tag = tag;
                treeView1.Items.Add(conditionItem);
            }
            for (int i = 0; i < modelPartsInfo.cMatAnimInfoList.Count; i++)
            {
                TreeViewItem matAnimItem = new TreeViewItem();
                matAnimItem.Header = "cMatAnimInfo " + (i + 1);
                matAnimItem.Foreground = HelperFunctions.GetBrushFromHex("#AAAAAA");
                List<int> tag = new List<int>();
                tag.Add((int)classEnums.cMatAnimInfo);
                tag.Add(i);
                matAnimItem.Tag = tag;
                treeView1.Items.Add(matAnimItem);
            }
        }

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem ti = (TreeViewItem)treeView1.SelectedItem;
            if (ti != null)
            {
                List<int> tag = (List<int>)ti.Tag;
                if (tag[0] == (int)classEnums.cPartsInfo)
                {
                    ContentController.Content = modelPartsInfo.cPartsInfoList[tag[1]];
                }
                else if (tag[0] == (int)classEnums.cConditionInfo)
                {
                    ContentController.Content = modelPartsInfo.cConditionInfoList[tag[1]];
                }
                else if (tag[0] == (int)classEnums.cMatAnimInfo)
                {
                    ContentController.Content = modelPartsInfo.cMatAnimInfoList[tag[1]];
                }
                else if (tag[0] == (int)classEnums.cGroupInfo)
                {
                    ContentController.Content = modelPartsInfo.cGroupInfoList[tag[1]];
                }
                else
                {
                    ContentController.Content = null;
                }
            }
            else
            {
                ContentController.Content = null;
            }
        }
        private void ImportFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Monster Hunter World ModelPartsInfo (*.mpi) | *.mpi";
            openFile.Title = "Select your MPI file:";
            if (openFile.ShowDialog() == true)
            {
                BinaryReader br = new BinaryReader(new FileStream(openFile.FileName, FileMode.Open));
                modelPartsInfo = new ModelPartsInfo(br);
                br.Close();
                PopulateTreeView();
            }
        }

        private void ExportFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Monster Hunter World ModelPartsInfo (*.mpi) | *.mpi";
            saveFile.Title = "Export your file:";
            if (saveFile.ShowDialog() == true)
            {
                BinaryWriter bw = new BinaryWriter(new FileStream(saveFile.FileName, FileMode.OpenOrCreate));
                modelPartsInfo.Export(bw);
                bw.Close();
            }
        }

        private void ChangeMColorA(object sender, TextChangedEventArgs e)
        {
            for(int i = 0; i < modelPartsInfo.cConditionInfoList.Count; i++)
            {
                for(int j =0;j< modelPartsInfo.cConditionInfoList[i].cColorInfoList.Count; j++)
                {
                    modelPartsInfo.cConditionInfoList[i].cColorInfoList[j].UpdateABrush();
                }
            }
        }
        private void ChangeMColorB(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < modelPartsInfo.cConditionInfoList.Count; i++)
            {
                for (int j = 0; j < modelPartsInfo.cConditionInfoList[i].cColorInfoList.Count; j++)
                {
                    modelPartsInfo.cConditionInfoList[i].cColorInfoList[j].UpdateBBrush();
                }
            }
        }

        private void AddTagInfo(object sender, RoutedEventArgs e)
        {
            TreeViewItem ti = (TreeViewItem)treeView1.SelectedItem;
            List<int> tag = (List<int>)ti.Tag;
            //check to make sure we're looking at a group info
            if(tag[0] == (int)classEnums.cGroupInfo)
            {
                modelPartsInfo.cGroupInfoList[tag[1]].AddNotify(new cTagInfo());
            }
        }
        private void AddTag(object sender, RoutedEventArgs e)
        {
            FrameworkElement ele = (FrameworkElement)e.OriginalSource;
            if(ele != null)
            {
                cTagInfo tagInfo = ele.DataContext as cTagInfo;
                if(tagInfo != null)
                {
                    tagInfo.AddNotify(new UIntObject(0));
                }
            }
        }
        private void AddClass(object sender, RoutedEventArgs e)
        {
            InputDialog classChoice = new InputDialog();
            classChoice.Input.SelectedIndex = 0;
            classChoice.LabelA.Content = "Select which type to add one of:";
            if (classChoice.ShowDialog() == true)
            {
                switch (classChoice.Input.SelectedIndex)
                {
                    case 0:
                        modelPartsInfo.cPartsInfoList.Add(new cPartsInfo());
                        break;
                    case 1:
                        modelPartsInfo.cGroupInfoList.Add(new cGroupInfo());
                        break;
                    case 2:
                        modelPartsInfo.cConditionInfoList.Add(new cConditionInfo());
                        break;
                    case 3:
                        modelPartsInfo.cMatAnimInfoList.Add(new cMatAnimInfo());
                        break;
                    default:
                        break;
                }
            }
            PopulateTreeView();
        }
        private void RemoveSelected(object sender, RoutedEventArgs e)
        {
            TreeViewItem ti = (TreeViewItem)treeView1.SelectedItem;
            List<int> tag = (List<int>)ti.Tag;
            switch (tag[0])
            {
                case (int)classEnums.cPartsInfo:
                    modelPartsInfo.cPartsInfoList.RemoveAt(tag[1]);
                    break;
                case (int)classEnums.cGroupInfo:
                    modelPartsInfo.cGroupInfoList.RemoveAt(tag[1]);
                    break;
                case (int)classEnums.cConditionInfo:
                    modelPartsInfo.cConditionInfoList.RemoveAt(tag[1]);
                    break;
                case (int)classEnums.cMatAnimInfo:
                    modelPartsInfo.cMatAnimInfoList.RemoveAt(tag[1]);
                    break;
            }
            PopulateTreeView();
        }
    }
    public class TypeSelector : DataTemplateSelector
    {
        public DataTemplate ColorInfo { get; set; }
        public DataTemplate ConditionInfo { get; set; }
        public DataTemplate GroupInfo { get; set; }
        public DataTemplate MatAnimInfo { get; set; }
        public DataTemplate PartsInfo { get; set; }
        public DataTemplate TagInfo { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                if (item is cColorInfo)
                {
                    return ColorInfo;
                }
                else if (item is cConditionInfo)
                {
                    return ConditionInfo;
                }
                else if (item is cGroupInfo)
                {
                    return GroupInfo;
                }
                else if (item is cMatAnimInfo)
                {
                    return MatAnimInfo;
                }
                else if (item is cPartsInfo)
                {
                    return PartsInfo;
                }
                else if (item is cTagInfo)
                {
                    return TagInfo;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}

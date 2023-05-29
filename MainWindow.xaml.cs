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

namespace TextEditor
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()//這邊可以丟一些預設的東西
        {
            InitializeComponent();
            rtbText.Document.Blocks.Clear();// 清除rtbText(RichTextBox)的內容
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);// 設定字型下拉選單的選單內容，存取你的電腦裡面的字型庫，將你安裝的字型清單都放進去
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };// 設定字體大小下拉選單的選單內容，設定8`72的數字，這要用來設定字體大小
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "RTF文件 (*.rtf)|*.rtf|All files (*.*)|*.*"; // 跟記事本範例程式類似，不過要改成過濾為RTF檔案格式
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbText.Document.ContentStart, rtbText.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);// DataFormats 檔案格式也要設定為RTF檔案格式
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "RTF文件 (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbText.Document.ContentStart, rtbText.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)//SelectionChanged是更改的事件    Selection是選取的東西
        {

            if (cmbFontFamily.SelectedItem != null)// 判斷式：必須要有選擇項目，才會做文字格式改變
            {
                rtbText.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);// 將rtbText豐富文字框所選的項目，套用所設定的字型
            }
        }

        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontSize.SelectedItem != null)
            {
                rtbText.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.SelectedItem);// 將rtbText豐富文字框所選的項目，套用所設定的字體大小
            }
        }

        private void btnBold_Click(object sender, RoutedEventArgs e)
        {
            object temp = rtbText.Selection.GetPropertyValue(Inline.FontWeightProperty); // 取得你目前選取的文字，取得文字的字體粗細

            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold)))
            {
                rtbText.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);// 判斷：文字要有設定格式、設定為粗體，改變文字成為原來的粗細程度
            }
            else
            {
                rtbText.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);// 如果文字不是粗體，則改為粗體
            }
        }

        private void btnItalic_Click(object sender, RoutedEventArgs e)
        {
            object temp = rtbText.Selection.GetPropertyValue(Inline.FontStyleProperty); // 取得你目前選取的文字，取得文字的字體樣式（斜體或非斜體）

            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic)))
            {
                rtbText.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal); // 判斷：文字要有設定格式、設定為斜體，改變文字成為原來的正體
            }
            else
            {
                rtbText.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic); // 如果文字為正體，則改為斜體
            }
        }

        private void btnUnderline_Click(object sender, RoutedEventArgs e)
        {
            object temp = rtbText.Selection.GetPropertyValue(Inline.TextDecorationsProperty);// 取得你目前選取的文字，取得文字的字體樣式（字體裝飾）
            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline)))
            {
                rtbText.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);// 判斷：文字要有設定格式、設定為底線，將文字移除底線
            }
            else
            {
                rtbText.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline); // 如果文字沒有底線，則增加底線
            }
        }
    }
    }

   


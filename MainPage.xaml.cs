using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HtmlHandlerLib;
using System.Collections;
using HtmlAgilityPack;
using System.Threading;
using Windows.ApplicationModel.Background;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CoronaCases
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ArrayList tbList = null;
        public MainPage()
        {
            this.InitializeComponent();
           this.Loaded += new Windows.UI.Xaml.RoutedEventHandler(Load_info);
            refreshBtn.Click += new Windows.UI.Xaml.RoutedEventHandler(Load_info);
            loadTbs();
        }

        private async void Load_info(object sender,RoutedEventArgs e)
        {
            try
            {
                HtmlScraper newScraper = new HtmlScraper("https://www.geo.tv/");
               await System.Threading.Tasks.Task.Run(() => newScraper.getHtml());


                var doc= newScraper.HtmlDocLoader();
                
                updateUI(doc);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private void loadTbs()
        {
            tbList = new ArrayList();
            tbList.Add(confirmedCasesBox);
            tbList.Add(deathsBox);
            tbList.Add(recoveredCasesBox);
            tbList.Add(sindhBox);
            tbList.Add(punjabBox);
            tbList.Add(balochistanBox);
            tbList.Add(kpBox);
            tbList.Add(islamabadBox);
            tbList.Add(gbBox);
            tbList.Add(ajkBox);
        }

        private void updateUI(HtmlDocument doc)
        {
            string[] classList = { "vb_right_text", "vb_right_number" };
            var counts = doc.DocumentNode.SelectNodes($"//span[@class='{classList[0]}']");
            var labels = doc.DocumentNode.SelectNodes($"//span[@class='{classList[1]}']");
            for (int i = 0; i < tbList.Count; i++)
            {
                TextBlock textBlock = (TextBlock)tbList[i];
                textBlock.Text = $"{labels[i].InnerHtml} : {counts[i].InnerHtml}";

            }
        }
       
    }
}

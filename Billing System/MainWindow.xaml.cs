using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Billing_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Items itm = new Items();
        UserInfo info = new UserInfo();
        PrintPage page = new PrintPage();
        public static MainWindow inst;
        Random r = new Random();
        



        public MainWindow()
        {
            InitializeComponent();
            inst = this;
          
          
        }

        private void Date_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            date.Text = DateTime.Now.ToString();
            bill_no.Text = r.Next(0, 1000).ToString();
            name.Text = "اسم العميل";
            quant.Text = "1";
            price.Text = " ";
           name.SelectAll();
            name.Focus();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {



            Handling();

            ///////////////////////////
            
            RetrivingInfo();

            /////////////////////////

            SendingDataToInvoice();

            ////////////////////

            total.Content = (Convert.ToUInt32(total.Content) + itm.intialtot);


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


           PrintPage.inst.totalprice.Content = total.Content;
           PrintPage.inst.datagrid.Items.Add(itm);
            page.Show();
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
             
        }

        private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Quant_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        public void RetrivingInfo()
        {
            info.username = name.Text;
            info.address = address.Text;
            info.code_bill = Convert.ToInt32(bill_no.Text);
            info.time = Convert.ToDateTime(date.Text);
            info.finalprice = Convert.ToDouble(total.Content);
        }

        public void SendingDataToInvoice()
        {
            PrintPage.inst.buyer_name.Content = info.username.ToString();
            PrintPage.inst.invoice_code.Content = info.code_bill.ToString();
            PrintPage.inst.date.Content = info.time.ToString();
            PrintPage.inst.address.Content = info.address.ToString();
        }

        public void Handling()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name.Text))
                {
                    MessageBox.Show("برجاء كتابة اسم العميل", "خطأ");
                }
                else if (string.IsNullOrWhiteSpace(price.Text))
                {
                    MessageBox.Show("برجاء كتابة سعر المنتج", "خطأ");
                }
                else if (string.IsNullOrWhiteSpace(address.Text))
                {
                    MessageBox.Show("برجاء كتابة العنوان", "خطأ");
                }
                else if (string.IsNullOrWhiteSpace(quant.Text))
                {
                    MessageBox.Show("برجاء كتابة الكمية", "خطأ");
                }
                else if (string.IsNullOrWhiteSpace(item_name.Text))
                {
                    MessageBox.Show("برجاء كتابة اسم المنتج", "خطأ");
                }
                else
                {
                    itm.itemname = item_name.Text;
                    itm.itemprice = Convert.ToDouble(price.Text);
                    itm.quant = Convert.ToInt32(quant.Text);
                    itm.intialtot = Convert.ToInt32(quant.Text) * Convert.ToDouble(price.Text);

                    datagrid.Items.Add(itm);
                    PrintPage.inst.datagrid.Items.Add(itm);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



    }

    

    class Items
    {
        public string itemname { get; set; }
        public double itemprice  { get; set; }
        public int quant { get; set; }
        public double intialtot { get; set; }
        

    }

    class UserInfo
    {

        public string username { get; set; }
        public string address { get; set; }
        public int code_bill   { get; set; }
        public double finalprice { get; set; }
        public DateTime time { get; set; }


    }


}

﻿using System;
using System.Collections.Generic;
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

namespace prototype2
{
    /// <summary>
    /// Interaction logic for ucPurchaseOrder.xaml
    /// </summary>
    public partial class ucPurchaseOrder : UserControl
    {
        public ucPurchaseOrder()
        {
            InitializeComponent();
        }

        MainViewModel MainVM = Application.Current.Resources["MainVM"] as MainViewModel;

        public event EventHandler SelectCustomer;
        protected virtual void OnSelectCustomerClicked(RoutedEventArgs e)
        {
            MainVM.isNewPurchaseOrder = true;
            var handler = SelectCustomer;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler SelectSalesQuote;
        protected virtual void OnSelectSalesQuote(RoutedEventArgs e)
        {
            MainVM.isNewPurchaseOrder = true;
            var handler = SelectSalesQuote;
            if (handler != null)
                handler(this, e);
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsLoaded && this.IsVisible)
            {
                foreach (UIElement obj in containerGrid.Children)
                {
                    if (containerGrid.Children.IndexOf(obj) != 0)
                    {
                        obj.Visibility = Visibility.Collapsed;
                    }
                    else
                        obj.Visibility = Visibility.Visible;
                }
            }

        }

        private void selectSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            OnSelectCustomerClicked(e);
        }


        private void selectItemsBtn_Click(object sender, RoutedEventArgs e)
        {
            OnSelectSalesQuote(e);
        }

        

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement obj in containerGrid.Children)
            {
                if (containerGrid.Children.IndexOf(obj) == 0)
                {
                    nextBtn.Content = "Save";
                }
                else if (containerGrid.Children.IndexOf(obj) == 1)
                {
                    saveDataToDb();
                }
            }
        }

        private void saveDataToDb()
        {
            var dbCon = DBConnection.Instance();
            bool noError = true;
            string poNumChar = "TRANS" + DateTime.Now.ToString("yyyy-MM-dd");
            int termsDay = 30;
            decimal termsDp = 50;
            if ((bool)paymentDefaultRd.IsChecked)
            {
                termsDay = 30;
                termsDp = 50;
            }
            if (dbCon.IsConnect())
            {
                if (String.IsNullOrWhiteSpace(selectedDateRequiredTb.Text))
                {
                    MessageBox.Show("Purchase order seems empty.");
                }
                else
                {
                    string query = "INSERT purchase_order_t (`PONumChar`,`suppID`,`shipTo`, `POdueDate`,`asapDueDate`,`shipVia`, `requisitioner`, `incoterms`, `POstatus`, `currency`, `importantNotes`, `preparedBy`, `approveBy`, `refNo`, `termsDays`, `termsDP`) VALUES " +
                        "('" + poNumChar + "'," +
                        MainVM.SelectedCustomerSupplier.CompanyID + "," +
                        "'','" +
                        selectedDateRequiredTb.SelectedDate.Value.ToString("yyyy-MM-dd") + "'," +
                        (bool)asapCb.IsChecked + ",'" +
                        shipViaCb.SelectedValue.ToString() + "','" +
                        "','" + //requisitionaer
                        "','" + //incoterms
                        "PENDING" + "', '" +
                        currencyCb.SelectedValue.ToString() + "','" +
                        "','" + //importantNotes
                        "','" + //preparedBy
                        "','" + //approveBy
                        "'," + //refNo
                        termsDay + "," +
                        termsDp +
                        ");";
                    if (dbCon.insertQuery(query, dbCon.Connection))
                    {
                        if (dbCon.IsConnect())
                        {
                            foreach (RequestedItem ri in MainVM.RequestedItems)
                            {
                                query = "UPDATE `items_availed_t` SET poNumChar = '" + poNumChar + "' WHERE sqNoChar = '" + MainVM.SelectedSalesQuote.sqNoChar_ + "' AND id = '" + ri.availedItemID + "'";
                                dbCon.insertQuery(query, dbCon.Connection);
                            }
                            MessageBox.Show("Successfully added.");
                        }

                    }
                }
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach(UIElement obj in containerGrid.Children)
            {
                if (containerGrid.Children.IndexOf(obj) == 0)
                {
                    
                }
                else if(containerGrid.Children.IndexOf(obj) == 1)
                {

                }
            }
        }

        private void paymentCustomRb_Checked(object sender, RoutedEventArgs e)
        {
            downpaymentPercentTb.IsEnabled = true;
        }

        private void paymentCustomRb_Unchecked(object sender, RoutedEventArgs e)
        {
            downpaymentPercentTb.IsEnabled = false;
        }
    }
}

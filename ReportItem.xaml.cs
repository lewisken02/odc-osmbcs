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
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using System.Data;

namespace prototype2
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
            DisplayReport();
        }
        private void DisplayReport()
        {
            ReportItem.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", GetItem()));
            ReportItem.LocalReport.ReportEmbeddedResource = "prototype2.Report1.rdlc";
            ReportItem.RefreshReport();
        }

        private DataTable GetItem()
        {
            var dbCon = DBConnection.Instance();
            dbCon.IsConnect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = dbCon.Connection;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select i.itemCode, i.itemName, i.costPrice, po.orderDate from item_t i JOIN po_line_t p ON i.itemCode = p.itemNo JOIN purchase_order_t po ON p.PONumChar = po.PONumChar";

            DataSet1.DataTable2DataTable dSItem = new DataSet1.DataTable2DataTable();

            MySqlDataAdapter mySqlDa = new MySqlDataAdapter(cmd);
            mySqlDa.Fill(dSItem);

            return dSItem;

        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ReportItem.RefreshReport();
        }

        private void ComboBoxItemFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object SELECTEDINDEX = ComboBoxItemFilter.SelectedIndex;
            if (SELECTEDINDEX.Equals(0))
            {

                ComboBoxItemYear.Visibility = Visibility.Hidden;
                ComboBoxItemMonth.Visibility = Visibility.Hidden;
                MonthItem.Visibility = Visibility.Hidden;
                YearItem.Visibility = Visibility.Hidden;
                DatePickerItemStart.Visibility = Visibility.Hidden;
                DatePickerItemEnd.Visibility = Visibility.Hidden;
                ItemStart.Visibility = Visibility.Hidden;
                ItemEnd.Visibility = Visibility.Hidden;



            }
            if (SELECTEDINDEX.Equals(1))
            {
                ComboBoxItemYear.Visibility = Visibility.Hidden;
                ComboBoxItemMonth.Visibility = Visibility.Hidden;
                MonthItem.Visibility = Visibility.Hidden;
                YearItem.Visibility = Visibility.Hidden;
                DatePickerItemStart.Visibility = Visibility.Hidden;
                DatePickerItemEnd.Visibility = Visibility.Hidden;
                ItemStart.Visibility = Visibility.Hidden;
                ItemEnd.Visibility = Visibility.Hidden;


            }
            if (SELECTEDINDEX.Equals(2))
            {

                ComboBoxItemYear.Visibility = Visibility.Hidden;
                ComboBoxItemMonth.Visibility = Visibility.Visible;
                MonthItem.Visibility = Visibility.Visible;
                YearItem.Visibility = Visibility.Hidden;
                DatePickerItemStart.Visibility = Visibility.Hidden;
                DatePickerItemEnd.Visibility = Visibility.Hidden;
                ItemStart.Visibility = Visibility.Hidden;
                ItemEnd.Visibility = Visibility.Hidden;

            }
            if (SELECTEDINDEX.Equals(3))
            {
                ComboBoxItemYear.Visibility = Visibility.Visible;
                ComboBoxItemMonth.Visibility = Visibility.Hidden;
                MonthItem.Visibility = Visibility.Hidden;
                YearItem.Visibility = Visibility.Visible;
                DatePickerItemStart.Visibility = Visibility.Hidden;
                DatePickerItemEnd.Visibility = Visibility.Hidden;
                ItemStart.Visibility = Visibility.Hidden;
                ItemEnd.Visibility = Visibility.Hidden;


            }
            if (SELECTEDINDEX.Equals(4))
            {
                ComboBoxItemYear.Visibility = Visibility.Hidden;
                ComboBoxItemMonth.Visibility = Visibility.Hidden;
                MonthItem.Visibility = Visibility.Hidden;
                YearItem.Visibility = Visibility.Hidden;
                DatePickerItemStart.Visibility = Visibility.Visible;
                DatePickerItemEnd.Visibility = Visibility.Visible;
                ItemStart.Visibility = Visibility.Visible;
                ItemEnd.Visibility = Visibility.Visible;

            }
        }
    }
}
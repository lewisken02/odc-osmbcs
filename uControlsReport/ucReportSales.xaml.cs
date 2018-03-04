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
    public partial class ucReportItem : UserControl
    {
        public ucReportItem()
        {
            InitializeComponent();
           
        }
        private void DisplayReport()
        {
            ReportItem.Reset();
            var rNames = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("prototype2.rdlcfiles.SalesReport.rdlc");
            ReportItem.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource("odc_dbDataSetSales", GetItem()));
            ReportItem.LoadReport(rNames);
            ReportItem.ProcessingMode = Syncfusion.Windows.Reports.Viewer.ProcessingMode.Local;
            ReportItem.RefreshReport();
        }

        private DataTable GetItem()
        {
            var dbCon = DBConnection.Instance();
            dbCon.IsConnect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = dbCon.Connection;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT        i.itemName, s.serviceName, ia.totalCost, sa.totalCost AS Expr1, si.dateOfIssue, SUM(ia.totalCost) AS Expr2, MONTHNAME(si.dateOfIssue) AS Expr3, YEAR(si.dateOfIssue) AS Expr4, SUM(sa.totalCost)  AS Expr5 FROM item_t i INNER JOIN items_availed_t ia ON i.ID = ia.itemID INNER JOIN   cust_supp_t cs ON i.supplierID = cs.companyID INNER JOIN sales_invoice_t si ON cs.companyID = si.custID INNER JOIN service_sched_t ss ON si.invoiceNo = ss.invoiceNo INNER JOIN services_availed_t sa ON ss.serviceAvailedID = sa.id INNER JOIN services_t s ON sa.serviceID = s.serviceID";

            odc_dbDataSetSales.DataTable1DataTable dSItem = new odc_dbDataSetSales.DataTable1DataTable();

            MySqlDataAdapter mySqlDa = new MySqlDataAdapter(cmd);
            mySqlDa.Fill(dSItem);

            return dSItem;

        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ReportItem.RefreshReport();
        }
        private void DisplayReportItemDay()
        {
            ReportItem.Reset();
            var rNames = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("prototype2.rdlcfiles.SalesReport.rdlc");
            ReportItem.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource("odc_dbDataSetSales", GetItemDay()));
            ReportItem.LoadReport(rNames);
            ReportItem.ProcessingMode = Syncfusion.Windows.Reports.Viewer.ProcessingMode.Local;
            ReportItem.RefreshReport();
        }

        private DataTable GetItemDay()
        {
            var dbCon = DBConnection.Instance();
            dbCon.IsConnect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = dbCon.Connection;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT    i.itemName, s.serviceName, ia.totalCost, sa.totalCost AS Expr1, si.dateOfIssue, SUM(ia.totalCost) AS Expr2, MONTHNAME(si.dateOfIssue) AS Expr3, YEAR(si.dateOfIssue) AS Expr4, SUM(sa.totalCost) AS Expr5 FROM item_t i INNER JOIN items_availed_t ia ON i.ID = ia.itemID INNER JOIN cust_supp_t cs ON i.supplierID = cs.companyID INNER JOIN sales_invoice_t si ON cs.companyID = si.custID INNER JOIN service_sched_t ss ON si.invoiceNo = ss.invoiceNo INNER JOIN services_availed_t sa ON ss.serviceAvailedID = sa.id INNER JOIN  services_t s ON sa.serviceID = s.serviceID WHERE(CURDATE() = si.dateOfIssue)";

            odc_dbDataSetSales.DataTable1DataTable dSItem = new odc_dbDataSetSales.DataTable1DataTable();

            MySqlDataAdapter mySqlDa = new MySqlDataAdapter(cmd);
            mySqlDa.Fill(dSItem);

            return dSItem;

        }
        private void DisplayReportItemWeek()
        {
            ReportItem.Reset();
            var rNames = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("prototype2.rdlcfiles.SalesReport.rdlc");
            ReportItem.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource("odc_dbDataSetSales", GetItemWeek()));
            ReportItem.LoadReport(rNames);
            ReportItem.ProcessingMode = Syncfusion.Windows.Reports.Viewer.ProcessingMode.Local;
            ReportItem.RefreshReport();
        }

        private DataTable GetItemWeek()
        {
            var dbCon = DBConnection.Instance();
            dbCon.IsConnect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = dbCon.Connection;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT        i.itemName, s.serviceName, ia.totalCost, sa.totalCost AS Expr1, si.dateOfIssue, SUM(ia.totalCost) AS Expr2, MONTHNAME(si.dateOfIssue) AS Expr3, YEAR(si.dateOfIssue) AS Expr4, SUM(sa.totalCost)  AS Expr5 FROM item_t i INNER JOIN items_availed_t ia ON i.ID = ia.itemID INNER JOIN  cust_supp_t cs ON i.supplierID = cs.companyID INNER JOIN  sales_invoice_t si ON cs.companyID = si.custID INNER JOIN service_sched_t ss ON si.invoiceNo = ss.invoiceNo INNER JOIN services_availed_t sa ON ss.serviceAvailedID = sa.id INNER JOIN services_t s ON sa.serviceID = s.serviceID WHERE(WEEK(si.dateOfIssue) = '" + DatePickerItemWeek.SelectedDate.ToString() + "')";

            odc_dbDataSetSales.DataTable1DataTable dSItem = new odc_dbDataSetSales.DataTable1DataTable();

            MySqlDataAdapter mySqlDa = new MySqlDataAdapter(cmd);
            mySqlDa.Fill(dSItem);

            return dSItem;

        }

        private void DisplayReportItemMonth()
        {
            ReportItem.Reset();
            var rNames = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("prototype2.rdlcfiles.SalesReport.rdlc");
            ReportItem.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource("odc_dbDataSetSales", GetItemMonth()));
            ReportItem.LoadReport(rNames);
            ReportItem.ProcessingMode = Syncfusion.Windows.Reports.Viewer.ProcessingMode.Local;
            ReportItem.RefreshReport();
        }

        private DataTable GetItemMonth()
        {
            var dbCon = DBConnection.Instance();
            dbCon.IsConnect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = dbCon.Connection;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT        i.itemName, s.serviceName, ia.totalCost, sa.totalCost AS Expr1, si.dateOfIssue, SUM(ia.totalCost) AS Expr2, MONTHNAME(si.dateOfIssue) AS Expr3, YEAR(si.dateOfIssue) AS Expr4, SUM(sa.totalCost)  AS Expr5 FROM item_t i INNER JOIN items_availed_t ia ON i.ID = ia.itemID INNER JOIN cust_supp_t cs ON i.supplierID = cs.companyID INNER JOIN sales_invoice_t si ON cs.companyID = si.custID INNER JOIN  service_sched_t ss ON si.invoiceNo = ss.invoiceNo INNER JOIN services_availed_t sa ON ss.serviceAvailedID = sa.id INNER JOIN services_t s ON sa.serviceID = s.serviceID WHERE(MONTHNAME(si.dateOfIssue) = '"+ ComboBoxItemMonth.SelectedItem.ToString()+ "')";

            odc_dbDataSetSales.DataTable1DataTable dSItem = new odc_dbDataSetSales.DataTable1DataTable();

            MySqlDataAdapter mySqlDa = new MySqlDataAdapter(cmd);
            mySqlDa.Fill(dSItem);

            return dSItem;

        }
        private void DisplayReportItemYear()
        {

            ReportItem.Reset();
            var rNames = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("prototype2.rdlcfiles.SalesReport.rdlc");
            ReportItem.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource("odc_dbDataSetSales", GetItemYear()));
            ReportItem.LoadReport(rNames);
            ReportItem.ProcessingMode = Syncfusion.Windows.Reports.Viewer.ProcessingMode.Local;
            ReportItem.RefreshReport();
        }

        private DataTable GetItemYear()
        {
            var dbCon = DBConnection.Instance();
            dbCon.IsConnect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = dbCon.Connection;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT        i.itemName, s.serviceName, ia.totalCost, sa.totalCost AS Expr1, si.dateOfIssue, SUM(ia.totalCost) AS Expr2, MONTHNAME(si.dateOfIssue) AS Expr3, YEAR(si.dateOfIssue) AS Expr4, SUM(sa.totalCost)  AS Expr5 FROM item_t i INNER JOIN items_availed_t ia ON i.ID = ia.itemID INNER JOIN cust_supp_t cs ON i.supplierID = cs.companyID INNER JOIN  sales_invoice_t si ON cs.companyID = si.custID INNER JOIN   service_sched_t ss ON si.invoiceNo = ss.invoiceNo INNER JOIN services_availed_t sa ON ss.serviceAvailedID = sa.id INNER JOIN services_t s ON sa.serviceID = s.serviceID WHERE(YEAR(si.dateOfIssue)  = '"+ ComboBoxItemYear .SelectedItem.ToString()+ "')";

            odc_dbDataSetSales.DataTable1DataTable dSItem = new odc_dbDataSetSales.DataTable1DataTable();

            MySqlDataAdapter mySqlDa = new MySqlDataAdapter(cmd);
            mySqlDa.Fill(dSItem);

            return dSItem;

        }
        private void DisplayReportItemRange()
        {
            ReportItem.Reset();
            var rNames = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("prototype2.rdlcfiles.SalesReport.rdlc");
            ReportItem.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource("odc_dbDataSetSales", GetItemRange()));
            ReportItem.LoadReport(rNames);
            ReportItem.ProcessingMode = Syncfusion.Windows.Reports.Viewer.ProcessingMode.Local;
            ReportItem.RefreshReport();
        }

        private DataTable GetItemRange()
        {
            var dbCon = DBConnection.Instance();
            dbCon.IsConnect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = dbCon.Connection;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT        i.itemName, s.serviceName, ia.totalCost, sa.totalCost AS Expr1, si.dateOfIssue, SUM(ia.totalCost) AS Expr2, MONTHNAME(si.dateOfIssue) AS Expr3, YEAR(si.dateOfIssue) AS Expr4, SUM(sa.totalCost)  AS Expr5 FROM item_t i INNER JOIN  items_availed_t ia ON i.ID = ia.itemID INNER JOIN cust_supp_t cs ON i.supplierID = cs.companyID INNER JOIN sales_invoice_t si ON cs.companyID = si.custID INNER JOIN service_sched_t ss ON si.invoiceNo = ss.invoiceNo INNER JOIN services_availed_t sa ON ss.serviceAvailedID = sa.id INNER JOIN  services_t s ON sa.serviceID = s.serviceID WHERE(si.dateOfIssue BETWEEN '"+DatePickerItemStart.SelectedDate.ToString()+"' AND '"+DatePickerItemEnd.SelectedDate.ToString()+"')";

            odc_dbDataSetSales.DataTable1DataTable dSItem = new odc_dbDataSetSales.DataTable1DataTable();

            MySqlDataAdapter mySqlDa = new MySqlDataAdapter(cmd);
            mySqlDa.Fill(dSItem);

            return dSItem;

        }
        private void ComboBoxItemFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object SELECTEDINDEX = ComboBoxItemFilter.SelectedIndex;
            if (SELECTEDINDEX.Equals(0))
            {
                DisplayReportItemDay();
                ItemWeek.Visibility = Visibility.Hidden;
                ComboBoxItemYear.Visibility = Visibility.Hidden;
                ComboBoxItemMonth.Visibility = Visibility.Hidden;
                MonthItem.Visibility = Visibility.Hidden;
                YearItem.Visibility = Visibility.Hidden;
                DatePickerItemStart.Visibility = Visibility.Hidden;
                DatePickerItemEnd.Visibility = Visibility.Hidden;
                ItemStart.Visibility = Visibility.Hidden;
                ItemEnd.Visibility = Visibility.Hidden;
                DatePickerItemWeek.Visibility = Visibility.Hidden;


            }
            if (SELECTEDINDEX.Equals(1))
            {
                DisplayReportItemWeek();
                ComboBoxItemYear.Visibility = Visibility.Hidden;
                ComboBoxItemMonth.Visibility = Visibility.Hidden;
                MonthItem.Visibility = Visibility.Hidden;
                YearItem.Visibility = Visibility.Hidden;
                DatePickerItemStart.Visibility = Visibility.Hidden;
                DatePickerItemEnd.Visibility = Visibility.Hidden;
                ItemStart.Visibility = Visibility.Hidden;
                ItemEnd.Visibility = Visibility.Hidden;
                DatePickerItemWeek.Visibility = Visibility.Visible;
                ItemWeek.Visibility = Visibility.Visible;

            }
            if (SELECTEDINDEX.Equals(2))
            {
                ItemWeek.Visibility = Visibility.Hidden;
                DatePickerItemWeek.Visibility = Visibility.Hidden;
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
                ItemWeek.Visibility = Visibility.Hidden;
                DatePickerItemWeek.Visibility = Visibility.Hidden;
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
                ItemWeek.Visibility = Visibility.Hidden;
                DatePickerItemWeek.Visibility = Visibility.Hidden;
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

        private void ComboBoxItemMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayReportItemMonth();
        }

        private void ComboBoxItemYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayReportItemYear();
        }

        private void DatePickerItemStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayReportItemRange();
        }

        private void DatePickerItemEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayReportItemRange();
        }

        private void DatePickerItemWeek_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayReportItemWeek();
        }
    }
}

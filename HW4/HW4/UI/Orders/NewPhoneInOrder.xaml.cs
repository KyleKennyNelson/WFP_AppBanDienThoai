﻿using HW4.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace HW4
{
    /// <summary>
    /// Interaction logic for NewPhoneInOrder.xaml
    /// </summary>
    public partial class NewPhoneInOrder : Window
    {
        BindingList<PHONE> phone_list = new BindingList<PHONE>();
        int OrderID;
        SqlConnection connection;
        private void LoadPhone()
        {
            string sql = """
                SELECT PHONE.ID, PHONE.NAME, PHONE.PRICE, PHONE.THUMBNAIL, MANUFACTURER.NAME as MANUFACTURER_NAME
                FROM PHONE JOIN MANUFACTURER ON PHONE.MANUFACTURER_ID = MANUFACTURER.ID
                """;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            using var command = new SqlCommand(sql, connection);
            var read = command.ExecuteReader();
            while (read.Read())
            {
                phone_list.Add(new PHONE()
                {
                    ID = (int)read["ID"],
                    PhoneName = (string)read["NAME"],
                    Price = (float)read["PRICE"],
                    Thumbnail = (string)read["THUMBNAIL"],
                    Manufacturer = (string)read["MANUFACTURER_NAME"]
                });
            }

            PhoneChooseView.ItemsSource = phone_list;
        }
        public NewPhoneInOrder(SqlConnection conn, int OrderID)
        {
            this.OrderID = OrderID;
            InitializeComponent();
            LoadPhone();
        }

        private void AddNewPhone(object sender, RoutedEventArgs e)
        {
            try
            {
                int quantity = int.Parse(Quantity.Text);
                PHONE selected = (PHONE)PhoneChooseView.SelectedItem;
                ORDEREDPHONE oRDEREDPHONE = new ORDEREDPHONE() 
                {
                    PhoneID = selected.ID,
                    PhoneName = selected.PhoneName,
                    Price = (float)selected.Price,
                    quantity = quantity
                };
                var choice = MessageBox.Show("Do you want to add phone?", "Add Phone?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (choice == MessageBoxResult.Yes)
                {
                    bool AddPhone = BUS_Order.EditPhoneInOrder(connection, oRDEREDPHONE, OrderID, "Add New Phone");
                    if (AddPhone == true) 
                    {
                        MessageBox.Show("Added New Phone Successfully!", "Edit Successfully", MessageBoxButton.OK);
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Failed to Added New Phone!", "Edit Failed", MessageBoxButton.OK);
                        DialogResult = false;
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("You must input number!");
            }
        }
    }
}
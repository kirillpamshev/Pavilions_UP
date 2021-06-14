using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace Pavilions_program
{
    public partial class AddRenterWindow : Window
    {
        SqlConnection connection;
        Window parent;

        public AddRenterWindow(SqlConnection connection, Window parent)
        {
            InitializeComponent();
            this.Show();
            this.parent = parent;
            this.connection = connection;
            parent.Visibility = Visibility.Hidden;


        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

        private void accept_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = name_text.Text;
                string city = city_text.Text;
                string phone = phone_text.Text;
                string street = street_text.Text;
                string id_rent = DateTime.Now.ToString("yyyyddss");
                string sqlexpression = "INSERT INTO RENTORS VALUES(@idrentvalue, @namevalue, @phonevalue, @cityvalue, @streetvalue, 'Активен') ";
                SqlCommand command = new SqlCommand(sqlexpression, connection);
                SqlParameter par = new SqlParameter("@namevalue", name_text.Text);
                command.Parameters.Add(par);
                SqlParameter par1 = new SqlParameter("@idrentvalue", id_rent);
                command.Parameters.Add(par1);
                SqlParameter par2 = new SqlParameter("@phonevalue", phone_text.Text);
                command.Parameters.Add(par2);
                SqlParameter par3 = new SqlParameter("@cityvalue", city_text.Text);
                command.Parameters.Add(par3);
                SqlParameter par4 = new SqlParameter("@streetvalue", street_text.Text);
                command.Parameters.Add(par4);
                command.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена!");
                ((RentorsAdmin)parent).function_show();
            }
            catch (SqlException er)
            {
                MessageBox.Show(er.Number + "." + er.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace Pavilions_program
{


    public class class_stat
    {
        public string shop_senter_id { get; set; }
        public string city { get; set; }
        public string max_pav { get; set; }
        public string took_pav { get; set; }
        public string free_pav { get; set; }
        public string public_sum { get; set; }
        public string avg_sum { get; set; }
    }

    public partial class ManagerA : Window
    {
        SqlConnection connection;
        Window parent;
        public ManagerA(SqlConnection connection, Window parent)
        {
            InitializeComponent();
            this.Show();
            this.parent = parent;
            this.connection = connection;
            parent.Visibility = Visibility.Hidden;
            string sqlExpression = "SELECT shop_center_id, shop_center_name FROM dbo.SHOPING_CENTERS";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();


            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    string local_name = reader.GetValue(1).ToString();
                    shop_text.Items.Add(local_name);
                }
                reader.Close();

            }
            else
            {
                reader.Close();
            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            

            List<class_stat> stat_list = new List<class_stat>();
            try
            {
                string sqlExpression = "EXEC SHOW_STAT_SHOPS @shop_center";
                SqlParameter par = new SqlParameter("@shop_center", shop_text.SelectedItem);
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(par);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        class_stat st_rec = new class_stat();

                        st_rec.shop_senter_id = reader.GetValue(0).ToString();
                        st_rec.city = reader.GetValue(1).ToString();
                        st_rec.max_pav = reader.GetValue(2).ToString();
                        st_rec.took_pav = reader.GetValue(3).ToString();
                        st_rec.free_pav = reader.GetValue(4).ToString();
                        st_rec.public_sum = reader.GetValue(5).ToString();
                        st_rec.avg_sum = reader.GetValue(6).ToString();

                        stat_list.Add(st_rec);
                    }

                    reader.Close();
                    grid.ItemsSource = stat_list;

                    grid.Columns[0].Visibility = Visibility.Hidden;
                    grid.Columns[1].Header = "Город";
                    grid.Columns[2].Header = "Макс.";
                    grid.Columns[3].Header = "Занято";
                    grid.Columns[4].Header = "Свободно";
                    grid.Columns[5].Header = "Площадь";
                    grid.Columns[6].Header = "Сред. стоимость";

                }

                else
                {
                    reader.Close();
                    grid.ItemsSource = null;
                }
            }

            catch (SqlException er)
            {
                MessageBox.Show("Произошла ошибка: " + er.Number + "." + er.Message);
                this.Close();
            }
        }
    }
}

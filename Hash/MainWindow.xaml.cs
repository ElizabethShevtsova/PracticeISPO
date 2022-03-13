using System;
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

namespace Hash
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HashEntities db = new HashEntities())
            {
                var login = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@login",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 50,
                    Value = Login.Text
                };
                var password = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@password",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 50,
                    Value = Password.Text
                };
                var result = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = 250
                };
                db.Database.ExecuteSqlCommand("AddAccount @login, @password, @result OUTPUT", login, password, result);

                db.SaveChanges();
                MessageBox.Show("You are added");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (HashEntities db = new HashEntities())
            {
                var login = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@login",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 50,
                    Value = Login.Text
                };
                var password = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@password",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 50,
                    Value = Password.Text
                };
                var result = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = 250
                };
                db.Database.ExecuteSqlCommand("UserLogin @login, @password, @result OUTPUT", login, password, result);

                MessageBox.Show((string)result.Value);
            }
        }
    }
}

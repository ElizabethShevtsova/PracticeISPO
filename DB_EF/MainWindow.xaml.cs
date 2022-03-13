using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace DB_EF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PeopleContext db;
        public MainWindow()
        {
            InitializeComponent();
            db = new PeopleContext();
            db.Persons.Load();
            dgPeople.ItemsSource = db.Persons.Local.ToBindingList();
            this.Closing += Window_Closing;

        }
        
        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgPeople.SelectedItems.Count > 0)
            {
                for(int i = 0; i < dgPeople.SelectedItems.Count; i++)
                {
                    Person p = dgPeople.SelectedItems[i] as Person;
                    if (p != null)
                    {
                        db.Persons.Remove(p);
                    }
                }
            }
            db.SaveChanges();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        
    }
}

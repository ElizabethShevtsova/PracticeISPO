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



namespace db_First
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

        private void Btn_AddRoom_Click(object sender, RoutedEventArgs e)
        {
            using (HotelEntities db = new HotelEntities())
            {
                Rooms r = new Rooms();
                r.NumberOfPersons = Convert.ToInt32(numberOfperson.Text);
                r.NumberOfRooms = Convert.ToInt32(numberOfRooms.Text);
                r.Price = Convert.ToDecimal(Price.Text);
                db.Rooms.Add(r);
                db.SaveChanges();
            }
        }

        private void Btn_AddPerson_Click(object sender, RoutedEventArgs e)
        {
            using (HotelEntities db = new HotelEntities())
            {
                Customers c = new Customers();
                c.Age = Convert.ToInt32(AgeC.Text);
                c.PassportID= Convert.ToInt32(PassportId.Text);
                c.FirstName = FirstName.Text;
                c.LastName = LastName.Text;
                c.Email = Email.Text;
                c.Phone = Phone.Text;
                db.Customers.Add(c);
                db.SaveChanges();

            }
        }

        private void Btn_AddBooking_Click(object sender, RoutedEventArgs e)
        {
            using (HotelEntities db = new HotelEntities())
            {
                Booking b = new Booking();
                b.ArrivalDate = Convert.ToDateTime(ArrDate.Text);
                b.ArrivalTime = TimeSpan.Parse(ArrTime.Text);
                b.DepartureDate = Convert.ToDateTime(DepDate.Text);
                b.DepartureTime = TimeSpan.Parse(DepTime.Text);
                b.CustomerId = db.Customers.Where(cust => cust.LastName == LastName.Text).FirstOrDefault().Id;
                b.RoomId = db.Rooms.FirstOrDefault().Id;
                db.Booking.Add(b);
                db.SaveChanges();
                MessageBox.Show("Все данные заполнены");
                

            }
        }

        

        private void Btn_data_Click(object sender, RoutedEventArgs e)
        {
            tbOutPut.Clear();
            using(HotelEntities db = new HotelEntities())
            {
                var bookings = db.Booking.Join(db.Customers, booking => booking.CustomerId, cust => cust.Id, (booking, cust) => new
                {
                    LastName = cust.LastName,
                    FirstName = cust.FirstName,
                    Email = cust.Email,
                    Phone = cust.Phone,
                    ArrDate = booking.ArrivalDate,
                    DepDate = booking.DepartureDate,
                });
                foreach(var bk in bookings)
                {
                    tbOutPut.Text += string.Format("{0} {1} Email: {2} Phone:{3} \n ArrivalDate:{4} DepartureDate:{5}\n", bk.LastName,
                        bk.FirstName, bk.Email, bk.Phone, bk.ArrDate, bk.DepDate);
                }

            }
        }

        private void Btn_rooms_Click(object sender, RoutedEventArgs e)
        {
            tbOutPut.Clear();
            using (HotelEntities db = new HotelEntities())
            {
                var rooms = db.Rooms;
                foreach (var r in rooms)
                {
                    tbOutPut.Text += string.Format("{0} NumberOfPerson: {1} NumberOfRooms: {2} Price: {3}\n", r.Id,r.NumberOfPersons,r.NumberOfRooms,r.Price);
                }
            }
        }
        private void Btn_cust_Click(object sender, RoutedEventArgs e)
        {
            tbOutPut.Clear();
            using (HotelEntities db = new HotelEntities())
            {
                var cust = db.Customers;
                foreach (var c in cust)
                {
                    tbOutPut.Text += string.Format("{0} {1} Age: {2} Email: {3} Phone:{4}\n", c.LastName,c.FirstName,c.Age,c.Email,c.Phone);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(HotelEntities db = new HotelEntities())
            {
                Customers c = db.Customers.Where(cust => cust.LastName == Deletecust.Text).FirstOrDefault();
                if (c != null)
                {
                    db.Customers.Remove(c);
                    db.SaveChanges();
                    MessageBox.Show("User deleted");
                }
                else
                {
                    MessageBox.Show("the user does not exist");

                }
            }
        }
    }
}

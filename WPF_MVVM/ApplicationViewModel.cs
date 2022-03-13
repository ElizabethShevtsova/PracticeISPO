using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        private Contact selectedContact;
        public ObservableCollection<Contact> Contacts { get; set; }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        Contact contact = new Contact();
                        Contacts.Insert(0, contact);
                        SelectedContact = contact;
                    }));
            }
        }
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        Contact phone = obj as Contact;
                        if (phone != null)
                        {
                            Contacts.Remove(phone);
                        }
                    },
                    (obj) => Contacts.Count > 0));
            }
        }
        public Contact SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                OnPropertyChanged("SelectedContact");
            }
            
        }
        public ApplicationViewModel()
        {
            Contacts = new ObservableCollection<Contact>
            {
                new Contact{Name="Ann",Surname="Ivanova",Phone="89546734212"},
                new Contact{Name="Elizabet",Surname="Shevtsova",Phone="89435231289"},
                new Contact{Name="Egor",Surname="Ivanov",Phone="897654231299"}

            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}

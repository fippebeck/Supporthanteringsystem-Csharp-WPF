using Support_Janssons_fastigheter.Models;
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
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Support___Janssons_fastigheter.Properties;
using System.Runtime.Remoting.Contexts;
using Support_Janssons_fastigheter;
using Support___Janssons_fastigheter;
using System.Data;
using System.Data.Entity.Migrations;
using System.Windows.Media.Animation;

namespace Support_Janssons_fastigheter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly MyContext context;
        public MainWindow()
        {


            InitializeComponent();
            string[] itemss = { "Inte påbörjad", "Påbörjad", "Avklarad" };
            comboBox.ItemsSource = itemss;


            var column = dataGrid.Columns[0];
            // Sorterar ärendena så att senaste skapat ärende visas högst upp listan.
            dataGrid.Items.SortDescriptions.Clear();
            dataGrid.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(column.SortMemberPath, System.ComponentModel.ListSortDirection.Descending));

            context = new MyContext();
            //inititierar databaskontexten


            //inititierar databaskontexten för att länka customer id:et med rätt namn i customer tabellen. Samt ordnar upp de i TicketViewModel 
            //Laddar också in datan med korrekt customer id till customer name från ticketviewmodel till dataGrid.
            using (var context = new MyContext())
            {
                var tickets = context.Tickets.Include(t => t.Customer).ToList();
                var ticketViewModels = tickets.Select(t => new TicketViewModel
                {
                    TicketId = t.TicketId,
                    CustomerName = t.Customer.CustomerName,
                    CategoryName = t.CategoryName,
                    Description = t.Description,
                    Status = t.Status
                }).ToList();

                dataGrid.ItemsSource = ticketViewModels;
            }
        }


        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 100;
            animation.Duration = TimeSpan.FromSeconds(0.17);
            progressBar.BeginAnimation(ProgressBar.ValueProperty, animation);



            // Definerar ticket id och status
            var val1 = dataGrid.SelectedItem as TicketViewModel;
            if (val1 != null)
            {
                int value = val1.TicketId;
                var status = comboBox.SelectedValue.ToString();
                //här använder vi metoden FirstOrDefault för att hämta Ticket-objektet från databasen med ett matchande TicketId-värde
                // Samt uppdaterar databasen med vårat nya värde
                var nTicket = context.Tickets.FirstOrDefault(t => t.TicketId == value);
                if (nTicket != null)
                {

                    nTicket.Status = status;
                    context.SaveChanges();
                }
            }

            //När knappen klickas inititierar databaskontexten för att länka customer id:et med rätt namn i customer tabellen. Samt ordnar upp de i TicketViewModel 
            //Laddar också in datan med korrekt customer id till customer name från ticketviewmodel till dataGrid.
            using (var context = new MyContext())
            {
                var tickets = context.Tickets.Include(t => t.Customer).ToList();
                var ticketViewModels = tickets.Select(t => new TicketViewModel
                {
                    TicketId = t.TicketId,
                    CustomerName = t.Customer.CustomerName,
                    CategoryName = t.CategoryName,
                    Description = t.Description,
                    Status = t.Status
                }).ToList();

                dataGrid.ItemsSource = ticketViewModels;
            }
            var column = dataGrid.Columns[0];
            // Sorterar ärendena så att senaste skapat ärende visas högst upp listan.
            dataGrid.Items.SortDescriptions.Clear();
            dataGrid.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(column.SortMemberPath, System.ComponentModel.ListSortDirection.Descending));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Skapa Skapa = new Skapa();
            Skapa.Show();
        }


        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBox.IsEnabled = true;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button_Copy1.IsEnabled = true;
        }
    }
}

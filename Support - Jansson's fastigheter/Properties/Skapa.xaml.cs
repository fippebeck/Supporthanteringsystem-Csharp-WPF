using Support_Janssons_fastigheter;
using Support_Janssons_fastigheter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace Support___Janssons_fastigheter.Properties
{
    /// <summary>
    /// Interaction logic for Skapa.xaml
    /// </summary>
    public partial class Skapa : Window
    {
        private readonly MyContext context;

        public Skapa()
        {
            InitializeComponent();


            //inititera databasen
            context = new MyContext();
            //listar namnen som bor i huset
            var names = listBox.ItemsSource = context.Customers.Select(t => t.CustomerName).ToList();
            listBox.ItemsSource = names;



        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var customerName = listBox.SelectedValue.ToString();
            var categoryName = textBox.Text;
            var description = textBox_Copy.Text;


            // Matchar ihop tillhörande id med namn via metoden firstordefault
            var customerId = context.Customers
            .Where(c => c.CustomerName == customerName)
            .Select(c => c.CustomerId)
            .FirstOrDefault();




            // Skapar ärendet
            var newTicket = new Ticket

            {
                CustomerId = customerId,
                CategoryName = categoryName,
                Description = description,
                Status = "Inte påbörjad"
            };

            //Lägger till ärendet i databasen o sparar
            context.Tickets.Add(newTicket);
            context.SaveChanges();
            Close();
            MessageBox.Show("Ärendet inskickat");

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void textBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

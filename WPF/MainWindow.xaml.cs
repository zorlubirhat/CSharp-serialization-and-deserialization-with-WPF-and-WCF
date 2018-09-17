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

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BusinessLayer.Business business = new BusinessLayer.Business();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void window_load(object sender, RoutedEventArgs e)
        {
            mainpage();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            startfunc();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fileslist.SelectedIndex >= 0)
            {
                string item = fileslist.SelectedItem.ToString();
                fileslistSelectionItem(item);
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedindex = booklist.SelectedIndex;
            string file = fileslist.SelectedItem.ToString();
            booklistSelectionItem(file, selectedindex);
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            int selectedindex = fileslist.SelectedIndex;
            string filename = business.returnFiles(nameoffile.Text)[selectedindex].ToString();
            createButtonFunc(filename);
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            int selectedindex = booklist.SelectedIndex;
            int fileindex = fileslist.SelectedIndex;
            string filename = business.returnFiles(nameoffile.Text)[fileindex].ToString();
            editButtonFunc(filename, selectedindex);
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void mainpage()
        {
            refresh.Visibility = Visibility.Hidden;
            id.Visibility = Visibility.Hidden;
            lblid.Visibility = Visibility.Hidden;
            create.Visibility = Visibility.Hidden;
            edit.Visibility = Visibility.Hidden;
            id.IsReadOnly = true;
            author.IsReadOnly = true;
            title.IsReadOnly = true;
            genre.IsReadOnly = true;
            price.IsReadOnly = true;
            publishdate.IsReadOnly = true;
        }

        public void startfunc()
        {
            foreach (string path in business.returnFiles(nameoffile.Text))
            {
                if (path.Equals("No found file!"))
                {
                    nameoffile.Text = "No Found File!";
                }
                else if (path.Equals("No permission for this file!"))
                {
                    nameoffile.Text = "No Permission For This File!";
                }
                else
                {
                    nameoffile.Visibility = Visibility.Hidden;
                    fileslist.Items.Add(path);
                }
            }
        }

        public void fileslistSelectionItem(string file)
        {
            refresh.Visibility = Visibility.Visible;
            create.Visibility = Visibility.Visible;
            booklist.Items.Clear();

            foreach (ItemCatalog.Items item in business.returnItems(file))
            {
                booklist.Items.Add(item.id);
            }
            author.Text = "None";
            title.Text = "None";
            genre.Text = "None";
            price.Text = "None";
            publishdate.Text = "None";
        }

        public void booklistSelectionItem(string file, int index)
        {
            edit.Visibility = Visibility.Visible;

            if (index >= 0)
            {
                ItemCatalog.Items item = business.returnSelectedItem(file, index);
                author.Text = item.author;
                title.Text = item.title;
                genre.Text = item.genre;
                price.Text = item.price.ToString();
                publishdate.Text = item.publish_date;
                index = -1;
            }
        }

        public void createButtonFunc(string filename)
        {
            edit.Visibility = Visibility.Hidden;
            if (create.Content.Equals("Create a New Book"))
            {
                create.Content = "Create";

                id.Visibility = Visibility.Visible;
                lblid.Visibility = Visibility.Visible;

                id.Text = "";
                author.Text = "";
                title.Text = "";
                genre.Text = "";
                price.Text = "";
                publishdate.Text = "";

                id.IsReadOnly = false;
                author.IsReadOnly = false;
                title.IsReadOnly = false;
                genre.IsReadOnly = false;
                price.IsReadOnly = false;
                publishdate.IsReadOnly = false;
            }

            else if (create.Content.Equals("Create"))
            {
                if (id.Text.Length == 0 || author.Text.Length == 0 || title.Text.Length == 0 || genre.Text.Length == 0 || price.Text.Length == 0 || publishdate.Text.Length == 0)
                {
                    MessageBox.Show("Please fill in the gaps!");
                }
                else
                {
                    create.Content = "Create a New Book";

                    id.Visibility = Visibility.Hidden;
                    lblid.Visibility = Visibility.Hidden;

                    booklist.Items.Add(business.createNewItem(id.Text, author.Text, title.Text, genre.Text, double.Parse(price.Text), publishdate.Text, filename).id);

                    author.IsReadOnly = true;
                    title.IsReadOnly = true;
                    genre.IsReadOnly = true;
                    price.IsReadOnly = true;
                    publishdate.IsReadOnly = true;
                    author.Text = "None";
                    title.Text = "None";
                    genre.Text = "None";
                    price.Text = "None";
                    publishdate.Text = "None";
                }
            }
        }

        public void editButtonFunc(string filename, int index)
        {
            ItemCatalog.ItemCatalog catalog = business.returnCatalog(filename);
            ItemCatalog.Items item = business.returnItems(filename)[index];

            create.Visibility = Visibility.Hidden;
            if (edit.Content.Equals("Edit Book"))
            {
                edit.Content = "Save Book";
                author.Text = item.author;
                title.Text = item.title;
                genre.Text = item.genre;
                price.Text = item.price.ToString();
                publishdate.Text = item.publish_date;
                author.IsReadOnly = false;
                title.IsReadOnly = false;
                genre.IsReadOnly = false;
                price.IsReadOnly = false;
                publishdate.IsReadOnly = false;
            }

            else if (edit.Content.Equals("Save Book"))
            {
                create.Visibility = Visibility.Visible;
                if (author.Text.Length == 0 || title.Text.Length == 0 || genre.Text.Length == 0 || price.Text.Length == 0 || publishdate.Text.Length == 0)
                {
                    MessageBox.Show("Please fill in the gaps!");
                }
                else
                {
                    edit.Content = "Edit Book";
                    item.author = author.Text;
                    item.title = title.Text;
                    item.genre = genre.Text;
                    item.price = double.Parse(price.Text);
                    item.publish_date = publishdate.Text;

                    business.editItem(filename, catalog, item, index);

                    author.IsReadOnly = true;
                    title.IsReadOnly = true;
                    genre.IsReadOnly = true;
                    price.IsReadOnly = true;
                    publishdate.IsReadOnly = true;
                }
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            edit.Visibility = Visibility.Hidden;
            nameoffile.Visibility = Visibility.Visible;
            create.Visibility = Visibility.Hidden;
            booklist.Items.Clear();
            fileslist.Items.Clear();
            author.Text = "None";
            title.Text = "None";
            genre.Text = "None";
            price.Text = "None";
            publishdate.Text = "None";
        }
    }
}
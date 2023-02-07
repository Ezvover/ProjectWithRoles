using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections;

namespace Lab
{
    /// <summary>
    /// класс которйы работает со всеми хмл доками
    /// </summary>
    public class User
    {
        public User(Role role, string name, string password, int id)
        {
            Role = role;
            Name = name;
            Password = password;
            Id = id;
        }

        public Role Role
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        /*public void Login()
        {
        🤣🤣🤣🤣🤣🤣🤣
            

            Console.WriteLine("Введите логин: ");
            string loginXML = Console.ReadLine();
            Console.WriteLine("Введите пароль: ");
            string passwordXML = Console.ReadLine();
            var allAlbums = root.Elements("user");
            foreach (var item in allAlbums)
            {
                if (item.Attribute("userName").Value == loginXML && item.Element("userPassword").Value == passwordXML)
                {
                    Console.WriteLine("ok");
                }
            }
        }*/


    }

    public class Role
    {
        public string Name
        {
            get;
            set;
        }

        public Role(string name) => Name = name;
    }

    public class Item
        // item - vse, product - v magazine 
    {
        public Item (string name, string desc, int rate, int id)
        {
            Name = name;
            Desc = desc;
            Rate = rate;
            Id = id;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string desc;
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        private int rate;
        public int Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        
    }

    public class Product : Item
    {
        public Product(string name, string desc, int rate, int id, int price, int amount, int shopId) : base(name, desc, rate, id)
        {
           this.price = price;
           this.amount = amount;
           this.shopId = shopId;
        }

        private int price;
        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private int shopId;
        public int ShopId
        {
            get { return shopId; }
            set { shopId = value; }
        }


    }


    public class Shop
    {
        // amoutn price
        List<Product> productList = new List<Product>();


        public void AddProduct()
        {

        }

        public void DeleteProduct()
        {

        }

        public void HideProduct()
        {

        }
    }


    internal class Program
    {
        public static User CreateUserObject()
        {
            Random rand = new Random();
            Role role;
            if ((rand.Next() % 2) == 0)
            {
                role = new Role("admin");
            }
            else
            {
                role = new Role("user");
            }
            int strlenght = rand.Next(4, 10);
            string str = "";
            for (int i = 0; i < strlenght; i++)
            {
                str = str + Convert.ToChar(rand.Next(0, 26) + 65);
            }
            string passstr = "";
            for (int i = 0; i < 8; i++)
            {
                passstr = passstr + Convert.ToChar(rand.Next(0, 26) + 65);
            }
            return new User(role, str, passstr, rand.Next());
        }

        public static void UserToFile(List<User> list)
        {
            XDocument UserList = new XDocument();
            XElement root = new XElement("Users");
            XElement user;
            XElement userPassword;
            XElement userRole;
            XElement userID;
            XAttribute userName;

            foreach (User s in list)
            {
                user = new XElement("user");
                userPassword = new XElement("userPassword");
                userRole = new XElement("userRole");
                userID = new XElement("userID");
                userPassword.Value = s.Password;
                userRole.Value = s.Role.Name;
                userID.Value = s.Id.ToString();
                userName = new XAttribute("userName", s.Name);
                user.Add(userPassword);
                user.Add(userRole);
                user.Add(userID);
                user.Add(userName);
                root.Add(user);
            }
            UserList.Add(root);
            UserList.Save("userlist.xml");
        }

        public static void UserFromFile(List<User> list)
        {
            XDocument xDoc = XDocument.Load("userlist.xml");
            XElement? users = xDoc.Element("Users");
            if (users != null)
            {
                foreach (XElement user in users.Elements("user"))
                {
                    XAttribute? userName = user.Attribute("userName");
                    XElement? userPassword = user.Element("userPassword");
                    XElement? userRole = user.Element("userRole");
                    XElement? userID = user.Element("userID");
                    list.Add(new User(new Role(userRole.Value), userName.Value, userPassword.Value, int.Parse(userID.Value)));
                }
            }
        }

        public static void ItemToFile(List<Item> list)
        {
            // атрибутом будет ид а не нейм
            XDocument ItemList = new XDocument();
            XElement root = new XElement("Items");
            XElement item;
            XElement itemDesc;
            XElement itemRate;
            XElement itemId;
            XAttribute itemName;
            foreach (Item s in list)
            {
                item = new XElement("item");
                itemDesc = new XElement("itemDesc");
                itemRate = new XElement("itemRate");
                itemId = new XElement("itemId");
                itemDesc.Value = s.Desc;
                itemRate.Value = s.Rate.ToString();
                itemId.Value = s.Id.ToString();
                itemName = new XAttribute("itemName", s.Name);
                item.Add(itemDesc);
                item.Add(itemRate);
                item.Add(itemId);
                item.Add(itemName);
                root.Add(item);
            }
            ItemList.Add(root);
            ItemList.Save("itemlist.xml");
        }

        public static void ItemFromFile(List<Item> list)
        {
            XDocument xDoc = XDocument.Load("itemlist.xml");
            XElement? items = xDoc.Element("Items");
            if (items != null)
            {
                foreach (XElement item in items.Elements("item"))
                {
                    XAttribute? itemName = item.Attribute("itemName");
                    XElement? itemDesc = item.Element("itemDesc");
                    XElement? itemRate = item.Element("itemRate");
                    XElement? itemId = item.Element("itemId");
                    list.Add(new Item(itemName.Value, itemDesc.Value, int.Parse(itemRate.Value), int.Parse(itemId.Value)));
                }
            }
        }

        public static void ProductToFile(List<Product> list)
        {
            
            XDocument ProductList = new XDocument();
            XElement root = new XElement("Products");
            XElement price;
            XElement amount;
            XAttribute itemId;
            foreach (Item s in list)
            {
                product = new XElement("product");
                itemDesc = new XElement("itemDesc");
                itemRate = new XElement("itemRate");
                itemId = new XElement("itemId");
                itemDesc.Value = s.Desc;
                itemRate.Value = s.Rate.ToString();
                itemId.Value = s.Id.ToString();
                itemName = new XAttribute("itemName", s.Name);
                item.Add(itemDesc);
                item.Add(itemRate);
                item.Add(itemId);
                item.Add(itemName);
                root.Add(item);
            }
            ItemList.Add(root);
            ItemList.Save("itemlist.xml");

            /// по очереди вызываю шопы и кажжый шоп.продуктЛист записывается в конец файла. 
        }

        public static void ProductFromFile()
        {
            /// иду по файлу и чекаю шопИД. По шопИД нахожу нужный шоп и в шоп.продуктЛИст добавляю нужный продукт и так должен пройти по всему продукту. за n * k найти все нужные элементы и раскидывать их по списку 
        }
       

        static void Main(string[] args)
        {
            /*List<User> list = new List<User>();
            FromFile(list);
            for (int i = 0; i < 1000; i++)
            {
                list.Add(CreateObject());
            }
            foreach (User s in list)
            {
                Console.WriteLine($"{s.Role.Name}, {s.Name}, {s.Password}, {s.Id}\n");
            }
            ToFile(list);*/
       // public Item (string name, string desc, int rate, int id)
            List<Item> list = new List<Item>();
            list.Add(new Item("name", "desc", 33, 44));
            ItemToFile(list);
            ItemFromFile(list);
            //user.Login();
        }
    }
}

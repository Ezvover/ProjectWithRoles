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



    /*public class Products
    {
        private int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public void AddProduct()
        {
            XDocument UserList = new XDocument();
            XElement root = new XElement("Products");
            XElement product;
            XElement productAmount;
            XElement productNumber;
            XElement productID;
            XAttribute productName;

            product = new XElement("product");
            productAmount = new XElement("productAmount");
            productNumber = new XElement("productNumber");
            productID = new XElement("productID");

            productAmount.Value = "55";
            productNumber.Value = "5451451";
            productID.Value = "2";
            productName = new XAttribute("productName", "Apple");
            product.Add(productAmount);
            product.Add(productNumber);
            product.Add(productID);
            product.Add(productName);
            root.Add(product);
            UserList.Add(root);
            UserList.Save("productlist.xml");
        }
    }*/



    internal class Program
    {
        public static User CreateObject()
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

        public static void ToFile(List<User> list)
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

        public static void FromFile(List<User> list)
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
        static void Main(string[] args)
        {
            List<User> list = new List<User>();
            FromFile(list);
            for (int i = 0; i < 1000; i++)
            {
                list.Add(CreateObject());
            }
            foreach (User s in list)
            {
                Console.WriteLine($"{s.Role.Name}, {s.Name}, {s.Password}, {s.Id}\n");
            }
            ToFile(list);
            //user.Login();
        }
    }
}

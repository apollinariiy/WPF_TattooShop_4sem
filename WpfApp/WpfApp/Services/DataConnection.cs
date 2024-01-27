using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class DataConnection : DbContext
    {
        #region Connection

        private static readonly string connectionString;

        static DataConnection()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataConnection>());
        }
        #endregion Connection
        #region Singleton

        private static DataConnection _instance;

        public static DataConnection GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataConnection();
                if (_instance.Accounts.Count() == 0)
                {
                    _instance.Accounts.Add(new Admin("Admin", Manager.HashPassword("123"), "admin@gmail.com", "+375291112233", "Admin", "Admin", "Admin"));
                    _instance.SaveChanges();
                }
            }
            return _instance;
        }

        public static DataConnection GetInstance(string connectionString)
        {
            if (_instance == null)
                _instance = new DataConnection(connectionString);
            return _instance;
        }

        private DataConnection() : base(connectionString)
        {
        }

        private DataConnection(string connectionString) : base(connectionString)
        {
        }

        #endregion Singleton
        #region Tables

        public DbSet<Acc> Accounts { get; set; }
        public DbSet<Sketch> Sketch { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Favourite> Favourites { get; set; }


        #endregion Tables
        public static List<User> GetUsers() => GetInstance().Accounts
        .Where(account => account.AccountType == AccountType.User)
        .Select(account => account as User).ToList();
        public static List<Sketch> GetSketches() => GetInstance().Sketch.ToList();
        public static List<Master> GetMasters() => GetInstance().Masters.ToList();
        public static List<Reservation> GetReservations() => GetInstance().Reservations.ToList();
        public static List<Feedback> GetFeedbacks() => GetInstance().Feedbacks.ToList();
        public static List<Favourite> GetFavourites() => GetInstance().Favourites.ToList();
        

        public static List<User> SearchUsers(string query)
        {
            query = query?.ToLower();

            if (!string.IsNullOrEmpty(query))
            {
                var tags = query.Split(' ');

                return GetUsers().Where(user =>
                        tags.All(tag => user.ForSearch().Contains(tag))).ToList();
            }
            else
                return GetUsers();
        }

        public static List<Admin> SearchAdmins(string query)
        {
            query = query?.ToLower();

            if (!string.IsNullOrEmpty(query))
            {
                var tags = query.Split(' ');

                return GetInstance().Accounts.ToList().Where(account => account.AccountType == AccountType.Admin &&
                        tags.All(tag => account.ForSearch().Contains(tag))).Cast<Admin>().ToList();
            }
            else
                return GetInstance().Accounts.ToList().Where(account => account.AccountType == AccountType.Admin).Cast<Admin>().ToList();
        }
        public static List<Master> SearchMasters(string query)
        {
            query = query?.ToLower();

            if (!string.IsNullOrEmpty(query))
            {
                var tags = query.Split(' ');

                return GetMasters().Where(master =>
                    tags.All(tag => master.ForSearch().Contains(tag))).ToList();
            }
            else
            {
                return GetMasters();
            }
        }

    }
}

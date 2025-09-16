using BlogDataLibrary.Data;
using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace BlogTestUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlData db = GetConnection();

            Console.WriteLine("1. Register new user");
            Console.WriteLine("2. Login");
            Console.Write("Choose option: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Register(db);
            }
            else if (choice == "2")
            {
                Authenticate(db);
                AddPost(db);
                ListPosts(db);
                ShowPostDetails(db);
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
            
        }

        // Step 6: GetConnection
        static SqlData GetConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = builder.Build();

            ISqlDataAccess dbAccess = new SqlDataAccess(config);
            SqlData db = new SqlData(dbAccess);

            return db;
        }

        // Step 7: GetCurrentUser
        static UserModel? GetCurrentUser(SqlData db)
        {
            Console.Write("Username: ");
            string? username = Console.ReadLine();

            Console.Write("Password: ");
            string? password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            return db.Authenticate(username, password);
        }

        // Step 8: Authenticate
        public static void Authenticate(SqlData db)
        {
            UserModel? user = GetCurrentUser(db);

            if (user == null)
            {
                Console.WriteLine("Invalid credentials.");
            }
            else
            {
                Console.WriteLine($"Welcome, {user.Username}");
            }
        }

        // Register functionality
        public static void Register(SqlData db)
        {
            Console.Write("Enter new username: ");
            var username = Console.ReadLine();

            Console.Write("Enter new password: ");
            var password = Console.ReadLine();

            Console.Write("Enter first name: ");
            var firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            var lastName = Console.ReadLine();

            db.Register(username, firstName, lastName, password);

            Console.WriteLine("Registration successful!");
        }

        // Posts Functionality
        public static void AddPost(SqlData db)
        {
            UserModel user = GetCurrentUser(db);

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Write body: ");
            string body = Console.ReadLine();

            PostModel post = new PostModel
            {
                Title = title,
                Body = body,
                DateCreated = DateTime.Now,
                UserId = user.Id
            };

            db.AddPost(post);
        }

        public static void ListPosts(SqlData db)
        {
            List<ListPostModel> posts = db.ListPosts();

            foreach (ListPostModel post in posts)
            {
                string bodyStr = post.Body as string ?? string.Empty;
                Console.WriteLine($"{post.Id}. Title: {post.Title} by {post.UserName} [{post.DateCreated:yyyy-MM-dd}]");
                Console.WriteLine($"{bodyStr.Substring(0, Math.Min(20, bodyStr.Length))}...");
                Console.WriteLine();
            }
        }

        public static void ShowPostDetails(SqlData db)
        {
            Console.Write("Enter a post ID: ");
            int id = Int32.Parse(Console.ReadLine());

            ListPostModel post = db.ShowPostDetails(id);
            Console.WriteLine(post.Title);
            Console.WriteLine($"by {post.FirstName} {post.LastName} [{post.UserName}]");

            Console.WriteLine();

            Console.WriteLine(post.Body);
            Console.WriteLine(post.DateCreated.ToString("MMM d yyyy"));
        }

    }
}

﻿namespace BlogDataLibrary.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;
        public object Username { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

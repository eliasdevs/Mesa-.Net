namespace Mesa.Users
{
    /// <summary>
    /// Model de User
    /// </summary>
    public class User
    {
        public User(string name, string email, string password, string userName, string userId, string cellPhone)
        {
            Name = name;
            Email = email;
            Password = password;
            UserName = userName;
            UserId = userId;
            CellPhone = cellPhone;
        }

        /// <summary>
        /// nombre del users
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// contraseña en hash
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// userbname para login
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// identificativo unico en db es un Guid
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// celular
        /// </summary>
        public string CellPhone{ get; set; }
    }
}
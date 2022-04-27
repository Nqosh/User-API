using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using UserAPI.Model;

namespace UserAPI.Services
{
    /// <summary>
    /// UserService Class
    /// </summary>
    public class UserService : IUser
    {
        /// <summary>
        /// Delete User 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            XDocument userdoc = LoadXmlDocument();
            var deleteQuery = userdoc.Descendants("User")
                .Where(r => Convert.ToInt32(r.Element("Id").Value) == id);

            deleteQuery.ElementAt(0).Remove();
            try
            {
                userdoc.Save("Data/Users.xml");
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>User</returns>
        public async Task<IEnumerable<Model.User>> GetAll()
        {
            XDocument userdoc = LoadXmlDocument();
            var users = userdoc.Descendants("User").ToList();
            var userList = new List<Model.User>();

            foreach (var user in users)
            {
                var id = user.Element("Id").Value;
                var name = user.Element("Name").Value;
                var surname = user.Element("Surname").Value;
                var cellPhone = user.Element("CellPhone").Value;
                userList.Add(
                    new Model.User
                    {
                        Id = Convert.ToInt32(id),
                        Name = name,
                        Surname = surname,
                        CellPhone = cellPhone
                    });
            }
            return userList;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> Create(User user)
        {
            XDocument userdoc = LoadXmlDocument();

            if(user.Name == null || user.Surname == null && user.CellPhone == null)
            {
                return false;
            }

            var count = userdoc.Root.Descendants("User").Count();
            count++;
            userdoc.Element("Users").Add(

                new XElement("User",
                new XElement("Id", count),
                new XElement("Name", user.Name),
                new XElement("Surname", user.Surname),
                new XElement("CellPhone", user.CellPhone)
                ));

            try
            {
                userdoc.Save("Data/Users.xml");    
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
            return true;
        }

        public Task<bool> Update(User User)
        {
            throw new NotImplementedException();
        }

        private XDocument LoadXmlDocument()
        {
            XDocument userdoc = XDocument.Load("Data/Users. ");
            return userdoc;
        }
    }
}

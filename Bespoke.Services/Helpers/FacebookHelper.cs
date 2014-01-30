using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models;
using Facebook;
using Microsoft.CSharp.RuntimeBinder;

namespace Bespoke.Services.Helpers
{
    public class FacebookHelper
    {
        private readonly string _accessToken;

        public FacebookHelper(string accessToken)
        {
            _accessToken = accessToken;
        }

        public User GetFacebookUser()
        {
            var user = default(User);
            var client = new FacebookClient(_accessToken);

            dynamic facebookUser = client.Get("me");

            try
            {
                user = new User()
                {
                    Email = facebookUser.email,
                    FirstName = facebookUser.first_name,
                    LastName = facebookUser.last_name,
                    FacebookUserId = Convert.ToInt64(facebookUser.id),
                    UserRegistrationMethod = UserRegistrationMethods.Facebook
                };
            }
            catch (RuntimeBinderException ex)
            {
                // properties don't exist, failed to retrieve data from FB
            }

            return user;
        }

        public List<Connection> GetFacebookFriends()
        {
            var friendList = new List<Connection>();
            var client = new FacebookClient(_accessToken);

            dynamic friends = client.Get("me/friends?limit=5000");

            try
            {
                foreach (var friend in friends.data)
                {
                    friendList.Add(new Connection()
                        {
                            Id = Convert.ToInt64(friend.id),
                            Name = friend.name
                        });
                }
            }
            catch (RuntimeBinderException ex)
            {
                // properties don't exist, failed to retrieve data from FB
            }

            return friendList;
        }
    }
}

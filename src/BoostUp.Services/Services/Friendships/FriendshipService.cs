namespace BoostUp.Services.Friendships
{
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FriendshipService : IFriendshipService
    {
        private readonly BoostUpDbContext data;

        public FriendshipService(BoostUpDbContext data) 
            => this.data = data;

        public FriendshipServiceModel GetFriendship(string fromId, string toId)
             => this.data
             .Friendships
             .Where(fs => fs.RequesterId == fromId && fs.ResponderId == toId)
             .Select(fs => new FriendshipServiceModel
             {
                 RequesterId = fromId,
                 ResponderId = toId,
             })
             .FirstOrDefault();

        public int AddFriendship(string fromId, string toId)
        {
            var friendShipToAdd = new Friendship
            {
                RequesterId = fromId,
                ResponderId = toId,
            };

            this.data.Friendships.Add(friendShipToAdd);

            this.data.SaveChanges();

            return friendShipToAdd.Id;
        }
    }
}

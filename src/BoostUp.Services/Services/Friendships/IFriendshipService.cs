namespace BoostUp.Services.Friendships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IFriendshipService
    {
        FriendshipServiceModel GetFriendship(string fromId, string toId);

        int AddFriendship(string fromId, string toId);
    }
}

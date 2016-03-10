namespace TfsReports.SelfHost
{
    using System.Collections.Generic;

    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.Framework.Client;
    using Microsoft.TeamFoundation.Framework.Common;

    public class IdentityMapper
    {
        private readonly IIdentityManagementService identityService;
        private readonly Dictionary<string, string> identityCache;

        public IdentityMapper(TfsTeamProjectCollection teamProjectCollection)
        {
            identityService = teamProjectCollection.GetService<IIdentityManagementService>();
            identityCache = new Dictionary<string, string>();
        }

        public string GetUserDisplayName(string userId)
        {
            if (!identityCache.ContainsKey(userId))
            {
                identityCache.Add(userId, identityService.ReadIdentity(IdentitySearchFactor.AccountName, userId, MembershipQuery.Direct, ReadIdentityOptions.None).DisplayName);
            }

            return identityCache[userId];
        }
    }
}
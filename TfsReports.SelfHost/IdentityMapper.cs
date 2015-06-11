namespace TfsReports.SelfHost
{
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.Framework.Client;
    using Microsoft.TeamFoundation.Framework.Common;

    public class IdentityMapper
    {
        private readonly IIdentityManagementService identityService;

        public IdentityMapper(TfsTeamProjectCollection teamProjectCollection)
        {
            identityService = teamProjectCollection.GetService<IIdentityManagementService>();
        }

        public string GetUserDisplayName(string userId)
        {
            return identityService.ReadIdentity(IdentitySearchFactor.AccountName, userId, MembershipQuery.Direct, ReadIdentityOptions.None).DisplayName;
        }
    }
}
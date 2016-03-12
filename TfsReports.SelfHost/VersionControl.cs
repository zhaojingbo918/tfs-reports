namespace TfsReports.SelfHost
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.TeamFoundation.VersionControl.Client;

    public class VersionControl
    {
        private readonly TeamProject teamProject;
        private readonly VersionControlServer versionControlService;
        private readonly IdentityMapper identityMapper;

        public VersionControl(TfsProjectSelector projectSelector)
        {
            versionControlService = projectSelector.SelectedProjectCollection.GetService<VersionControlServer>();
            teamProject = versionControlService.GetTeamProject(projectSelector.SelectedProject.Name);
            identityMapper = new IdentityMapper(projectSelector.SelectedProjectCollection);
        }

        public string ProjectName
        {
            get
            {
                return teamProject.Name;
            }
        }

        public IEnumerable<Commit> GetAllChangesets()
        {
            return versionControlService.QueryHistory(teamProject.ServerItem, VersionSpec.Latest, 0, RecursionType.Full, null, new ChangesetVersionSpec(1), VersionSpec.Latest, int.MaxValue, false, true, false, true).Cast<Changeset>()
                .Select(x =>
                new Commit(x.ChangesetId, x.CreationDate, identityMapper.GetUserDisplayName(x.Owner), x.Comment));
        }
    }
}
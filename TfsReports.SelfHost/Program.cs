namespace TfsReports.SelfHost
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.Framework.Client;
    using Microsoft.TeamFoundation.Framework.Common;
    using Microsoft.TeamFoundation.VersionControl.Client;

    public class Program
    {
        public static void Main()
        {
            var teamProjectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
            teamProjectPicker.ShowDialog();

            var identityManagementService = teamProjectPicker.SelectedTeamProjectCollection.GetService<IIdentityManagementService>();

            var versionControlServer = teamProjectPicker.SelectedTeamProjectCollection.GetService<VersionControlServer>();
            var projectName = teamProjectPicker.SelectedProjects.First().Name;
            var teamProject = versionControlServer.GetTeamProject(projectName);

            Trace.WriteLine("Retrieving change set history in project {0}".With(teamProject.Name));

            var history = versionControlServer.QueryHistory(teamProject.ServerItem, VersionSpec.Latest, 0, RecursionType.Full, null, new ChangesetVersionSpec(1), VersionSpec.Latest, int.MaxValue, false, true, false, true);

            var fileName = "Changeset history for {0}.csv".With(projectName);

            Trace.WriteLine("Writing to file '{0}'".With(fileName));

            using (var streamWriter = File.CreateText(fileName))
            {
                streamWriter.WriteLine("Changeset Id,Creation Date,User,Comment");

                foreach (Changeset changeset in history)
                {
                    var userId = identityManagementService.ReadIdentity(IdentitySearchFactor.AccountName, changeset.Owner, MembershipQuery.Direct, ReadIdentityOptions.None);
                    streamWriter.WriteLine(
                        "{0},{1},{2},{3}",
                        changeset.ChangesetId,
                        changeset.CreationDate,
                        @userId.DisplayName.EncloseInDoubleQuotes(),
                        changeset.Comment.ReplaceCrLfWithSpace().EncloseInDoubleQuotes());
                }
            }

            Trace.WriteLine("Completed.");
        }
    }
}

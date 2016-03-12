namespace TfsReports.SelfHost
{
    using System.Linq;

    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.Server;

    public class TfsProjectSelector
    {
        public TfsProjectSelector()
        {
            var teamProjectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
            teamProjectPicker.ShowDialog();
            SelectedProjectCollection = teamProjectPicker.SelectedTeamProjectCollection;
            SelectedProject = teamProjectPicker.SelectedProjects.Single();
        }

        public TfsTeamProjectCollection SelectedProjectCollection { get; private set; }

        public ProjectInfo SelectedProject { get; private set; }
    }
}
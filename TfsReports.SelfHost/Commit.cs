namespace TfsReports.SelfHost
{
    using System;

    public class Commit
    {
        public Commit(int id, DateTime creationDate, string user, string comment)
        {
            Id = id;
            CreationDate = creationDate;
            User = user;
            Comment = comment;
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; }

        public string User { get; private set; }

        public string Comment { get; private set; }

        public override string ToString()
        {
            return "{0},{1},{2},{3}".With(
                Id,
                CreationDate,
                User.EncloseInDoubleQuotes(),
                Comment.ReplaceCrLfWithSpace().EncloseInDoubleQuotes());
        }
    }
}
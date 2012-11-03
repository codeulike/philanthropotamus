namespace Wwwtw.Domain
{
    using SharpArch.Domain.DomainModel;
    using System.ComponentModel.DataAnnotations;

    public class CharityInfo : Entity
    {
        public virtual int CharityNumber { get; set; }

        public virtual string CharityName { get; set; }

        public virtual string Activities { get; set; }

        public virtual string Website { get; set; }

        public virtual string TwitterAccount { get; set; }

        public virtual int GameScore { get; set; }



    }
}
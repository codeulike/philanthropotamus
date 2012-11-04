﻿namespace Wwwtw.Domain
{
    using SharpArch.Domain.DomainModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class CharityInfo : Entity
    {
        [DomainSignature]
        public virtual int CharityNumber { get; set; }

        public virtual string CharityName { get; set; }

        public virtual string Activities { get; set; }

        public virtual string Website { get; set; }

        public virtual string TwitterAccount { get; set; }

        public virtual int GameScore { get; set; }

        public virtual string ActivitiesSentenceCase
        {
            get
            {
                // from http://stackoverflow.com/a/3141467/22194
                // start by converting entire string to lower case
                var lowerCase = this.Activities.ToLower();
                // matches the first sentence of a string, as well as subsequent sentences
                var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
                // MatchEvaluator delegate defines replacement of setence starts to uppercase
                return r.Replace(lowerCase, s => s.Value.ToUpper());
            }

        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Wwwtw.Domain;
using SharpArch.Testing;
using SharpArch.Testing.NUnit;


namespace Wwwtw.Tests
{
    [TestFixture]
    public class CharityInfoTests
    {
        [Test]
        public void CanCompareCharityInfos()
        {
            CharityInfo instance = new CharityInfo();
            instance.CharityNumber = 35325; ;

            CharityInfo instanceToCompareTo = new CharityInfo();
            instanceToCompareTo.CharityNumber = 35325;

            instance.ShouldEqual(instanceToCompareTo);
        }
    }
}

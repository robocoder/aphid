﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Aphid.Tests.Integration
{
    [TestFixture(Category = "AphidQuery")]
    public class QueryTests : AphidTests
    {
        protected override bool LoadStd
        {
            get { return true;  }
        }

        protected override string PrefixScript(string script)
        {
            return base.PrefixScript("#'Query'; l = [ 1, 1, 2, 3, ]; l2 = [ 4, ];") + script;
        }

        [Test]
        public void WhereTest()
        {
            AssertTrue(@"
                x = [ 1, 2, 3, 4] |> @aq.where(@(x) x % 2 == 0);
                ret x.count() == 2 && x[0] == 2 && x[1] == 4;
            ");
        }

        [Test]
        public void AnyTest()
        {
            AssertFalse("ret l |> @aq.any(@(x) x == 5);");
        }

        [Test]
        public void AnyTest2()
        {
            AssertTrue("ret l |> @aq.any(@(x) x == 2);");
        }

        [Test]
        public void SelectManyTest()
        {
            AssertTrue(@"
                x = [ 1, 2 ] |> @aq.selectMany(@(x) [ x, x * x ]); 
                ret x.count() == 4 && x[0] == 1 && x[1] == 1 && x[2] == 2 && x[3] == 4;
            ");
        }

        [Test]
        public void DistinctTest()
        {
            AssertTrue(@"
                x = [ 1, 1, 2 ] |> aq.distinct; 
                ret x.count() == 2 && x[0] == 1 && x[1] == 2;
            ");
        }

        [Test]
        public void CountTest()
        {
            Assert9("ret range(0, 9) |> aq.count;");
        }

        [Test]
        public void AllTest()
        {
            AssertTrue("ret l |> @aq.all(@(x) x > 0);");
        }

        [Test]
        public void AllTest2()
        {
            AssertFalse("ret l |> @aq.all(@(x) x < 3);");
        }

        [Test]
        public void ConcatTest()
        {
            AssertTrue(@"
                x = [ 1, 2 ] |> @aq.concat([ 3, 4 ]);
                ret x.count() == 4 && x[0] == 1 && x[1] == 2 && x[2] == 3 && x[3] == 4;
            ");
        }

        [Test]
        public void SkipTest()
        {
            AssertTrue(@"
                x = [ 1, 2 ] |> @aq.concat([ 3, 4 ]);
                ret x.count() == 4 && x[0] == 1 && x[1] == 2 && x[2] == 3 && x[3] == 4;
            ");
        }

        [Test]
        public void TakeTest()
        {
            AssertTrue(@"
                x = l |> @aq.take(3); 
                ret x.count() == 3 && x[0] == 1 && x[1] == 1 && x[2] == 2;
            ");
        }

        [Test]
        public void AggrTest()
        {
            Assert9("ret [ 0, 2, 3, 4 ] |> @aq.aggr(@(x, y) x + y);");
        }
    }
}

﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Aphid.Tests.Integration
{
    [TestFixture(Category = "AphidOperator")]
    public class OperatorTests : AphidTests
    {
        [Test]
        public void AssignmentTest()
        {
            AssertFoo("x='foo'; ret x;");
        }

        [Test]
        public void AssignmentTest2()
        {
            AssertFoo("x='bar'; x='foo'; ret x;");
        }

        [Test]
        public void StringConcatTest()
        {
            AssertFoo("ret 'fo'+'o';");
        }

        [Test]
        public void AdditionTest()
        {
            Assert9("ret 2+7;");
        }

        [Test]
        public void SubtractionTest()
        {
            Assert9("ret 11-2;");
        }

        [Test]
        public void MultiplicationTest()
        {
            AssertEquals(20m, "ret 10*2;");
        }

        [Test]
        public void AdditionAndAssignmentTest()
        {
            Assert9("x = 2; x += 7; ret x;");
        }

        [Test]
        public void SubtractionAndAssignmentTest()
        {
            Assert9("x = 11; x -= 2; ret x;");
        }

        [Test]
        public void MultiplicationAndAssignmentTest()
        {
            Assert9("x = 4.5; x *= 2; ret x;");
        }

        [Test]
        public void DivisionAndAssignmentTest()
        {
            Assert9("x = 18; x /= 2; ret x;");
        }

        [Test]
        public void ConditionalTest()
        {
            Assert9("ret true ? 9 : 10;");
        }

        [Test]
        public void ConditionalTest2()
        {
            Assert9("ret false ? 10 : 9;");
        }

        [Test]
        public void ConditionalTest3()
        {
            Assert9("ret false ? 10 : true ? 9 : 8;");
        }

        [Test]
        public void ConditionalTest4()
        {
            Assert9("ret false ? 10 : false ? 8 : 9;");
        }

        [Test]
        public void RangeTest()
        {
            Assert9("#'std'; ret (0 .. 9).count();");
        }

        [Test]
        public void RangeTest2()
        {
            AssertEquals(0m, "ret (0 .. 9)[0];");
        }

        [Test]
        public void RangeTest3()
        {
            Assert9("ret (0 .. 10)[9];");
        }
    }
}

using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestThatCarHasCorrectLocation()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();

            using (mocks.Record())
            {
                mockDatabase.getCarLocation(7);
                LastCall.Return("Test Location");
            }
            targetCar.Database = mockDatabase;
            String testString = targetCar.getCarLocation(7);
            Assert.AreEqual(testString, "Test Location");
        }

        [Test()]
        public void TestThatCarHasCorrectMileage()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            Int32 Miles = 15;
            mockDatabase.Miles = Miles;
            var target = new Car(10);
            target.Database = mockDatabase;
            int TestMileage = target.Mileage;
            Assert.AreEqual(Miles, TestMileage);

        }
        [Test()]
        public void ObjectMotherTestCarName()
        {
            var target = ObjectMother.BMW();
            Assert.AreEqual(target.Name, "BMW Something");
        }

	}
}

using NUnit.Framework;
using System;
using Flea.Models;

namespace Flea.Tests
{
    public class ModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EventTests()
        {
            Event hello = new Event("hello", DateTime.Now);
            Assert.True(13 == hello.Clusters.Count, "There should always be 13 clusters");

            Assert.AreEqual(108, hello.ComputeRemainingTables);


        }

        [Test]
        public void CustomerTests()
        {
            const string name = "Ole";
            const string number = "88888888";
            Customer Ole = new Customer(name, number);
            Assert.True(Ole.Name == name || Ole.PhoneNumber == number);
        }

        [Test]
        public void ReservationTests()
        {
            const int priority = 1;
            const int tableCount = 4;
            const bool paid = false;
            const string comment = "Ole er meget gammel og grim";
            const string name = "Ole";
            const string number = "88888888";
            Customer Ole = new Customer(name, number);
            Reservation OleReservation = new Reservation(priority, tableCount, paid, comment, Ole, null!);
            OleReservation.Tables.Add(new Table());
            Assert.AreEqual(OleReservation.TableCount, tableCount, "reservation does not have the right tablecount based on input");
        }

        [Test]
        public void TableTests()
        {
            Table m1 = new Table();
            Assert.AreEqual(m1.Type, Table.TableType.Table);
        }

        [Test]
        public void ClusterTests()
        {
            const int tableAmount = 8;
            const int priority = 1;
            const int reservedTableCount = 4;
            const bool paid = false;
            const string comment = "Ole er meget gammel og grim";
            const string name = "Ole";
            const string number = "88888888";
            Customer Ole = new Customer(name, number);
            Reservation OleReservation = new Reservation(priority, reservedTableCount, paid, comment, Ole, null!);
            Cluster m = new Cluster("m", 4, tableAmount);
            Assert.AreEqual(m.CustomerCount, 4);

            m.Tables[tableAmount - 1].Reservation = OleReservation;
            Assert.AreEqual(m.Tables[tableAmount-1].Reservation.TableCount, 4, "trouble with assesing reservation info though table");

            /*checks if tables not placed gives back one lower than table amount since 1 table is reserved*/
            Assert.AreEqual(tableAmount - 1, m.TablesNotPlaced, "TablesNotPlaced does not give back the amount of unreserved tables");

            /*checks if reservation knows how many reservation there are*/
            Assert.IsFalse(1 != m.ReservationCount, "reservation count doesnt have the correct amount of reservations");

        }
    }
}
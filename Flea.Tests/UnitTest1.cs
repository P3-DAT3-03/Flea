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
            Assert.True(13 == hello.Clusters.Length, "There should always be 13 clusters");

            Assert.AreEqual(108, hello.ComputeRemainingTables);


        }

        [Test]
        public void CustomerTests()
        {
            string name = "Ole";
            int number = 88888888;
            Customer Ole = new Customer(name, number);
            Assert.True(Ole.Name == name || Ole.PhoneNumber == number);
        }

        [Test]
        public void ReservationTests()
        {
            int priority = 1;
            int tableCount = 4;
            bool paid = false;
            string comment = "Ole er meget gammel og grim";
            Reservation OleReservation = new Reservation(priority, tableCount, paid, comment);
            OleReservation.Tables.Add(new Table());
            Assert.AreEqual(OleReservation.TableCount, tableCount, "reservation does not have the right tablecount based on input");
        }

        [Test]
        public void TableTests()
        {
            Table m1 = new Table();
            Assert.AreEqual(m1.Type, Table.TableType.table);
        }

        [Test]
        public void ClusterTests()
        {
            int tableAmount = 8;
            int priority = 1;
            int reservedTableCount = 4;
            bool paid = false;
            string comment = "Ole er meget gammel og grim";
            Reservation OleReservation = new Reservation(priority, reservedTableCount, paid, comment);
            Cluster m = new Cluster("m", 4, tableAmount);
            Assert.AreEqual(m.CustomerCount, 4);

            m.Tables[tableAmount - 1].TableReservation = OleReservation;
            Assert.AreEqual(m.Tables[tableAmount-1].TableReservation.TableCount, 4, "trouble with assesing reservation info though table");

            /*checks if tables not placed gives back one lower than table amount since 1 table is reserved*/
            Assert.AreEqual(tableAmount - 1, m.TablesNotPlaced, "TablesNotPlaced does not give back the amount of unreserved tables");

            /*checks if reservation knows how many reservation there are*/
            Assert.IsFalse(1 != m.ReservationCount, "reservation count doesnt have the correct amount of reservations");

        }




    }
}
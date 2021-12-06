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
            Event @event = new Event(DateTime.Now);
            // real world is fucked so there is more than 13
            Assert.True(23 == @event.Clusters.Count, "There should always be 23 clusters");
            const string name = "Ole";
            const string number = "88888888";
            const int priority = 1;
            const int tableCount = 8;
            const PaymentStatus status = PaymentStatus.NotPaid;
            const string comment = "Ole er meget gammel og grim";
            Customer customer = new Customer(name, number);
            Reservation reservation = new Reservation(priority, tableCount, status, comment, customer, @event);
            reservation.Arrived = true;
            @event.Reservations.Add(reservation);
            @event.AssignReservation(@event.Clusters[5], new []{0}, reservation);
            for (var i = 0; i < 4; i++)
            {
                @event.Clusters[5].Tables[i].Type = TableType.Empty;
            }
            Assert.AreEqual(122, @event.ComputeRemainingTables);
            Assert.AreEqual(1, @event.ComputeReservationCount);
            Assert.AreEqual(1, @event.ComputeArrived);
            Assert.AreEqual(1, @event.ComputeMissingPayments);
            Assert.AreEqual(4, @event.ComputeEmptyTableCount);
            Assert.AreEqual(130, @event.ComputeTableCount);
        }

        [Test]
        public void CustomerTests()
        {
            const string name = "Ole";
            const string number = "88888888";
            Customer customer = new Customer(name, number);
            Assert.AreEqual(customer.Name, name);
            Assert.AreEqual(customer.PhoneNumber, number);
            Customer customerCopy = customer.Clone();
            Assert.AreEqual(customer.Name, customerCopy.Name);
            Assert.AreEqual(customer.PhoneNumber, customerCopy.PhoneNumber);
        }

        [Test]
        public void ReservationTests()
        {
            const int priority = 1;
            const int tableCount = 4;
            const PaymentStatus status = PaymentStatus.NotPaid;
            const string comment = "Ole er meget gammel og grim";
            const string name = "Ole";
            const string number = "88888888";
            Customer customer = new Customer(name, number);
            Reservation reservation = new Reservation(priority, tableCount, status, comment, customer, null!);
            
            Assert.AreEqual(reservation.TableCount, tableCount, "reservation does not have the right table count based on input");
            Assert.AreEqual(reservation.Comments, comment);
            Assert.AreEqual(reservation.Priority, priority);
            Assert.AreEqual(reservation.PaymentStatus, status);
            Assert.AreEqual(reservation.ReservationOwner.Name, name);
            Assert.AreEqual(reservation.ReservationOwner.PhoneNumber, number);
            
            reservation = new Reservation(priority, tableCount, status, comment);
            Assert.AreEqual(reservation.TableCount, tableCount, "reservation does not have the right table count based on input");
            Assert.AreEqual(reservation.Comments, comment);
            Assert.AreEqual(reservation.Priority, priority);
            Assert.AreEqual(reservation.PaymentStatus, status);

            Reservation reservationCopy = reservation.Clone();
            Assert.AreEqual(reservation.TableCount, reservationCopy.TableCount, "reservation does not have the right table count based on input");
            Assert.AreEqual(reservation.Comments, reservationCopy.Comments);
            Assert.AreEqual(reservation.Priority, reservationCopy.Priority);
            Assert.AreEqual(reservation.PaymentStatus, reservationCopy.PaymentStatus);
        }

        [Test]
        public void TableTests()
        {
            Table m1 = new Table();
            Assert.AreEqual(m1.Type, TableType.Table);
        }

        [Test]
        public void ClusterTests()
        {
            const int tableAmount = 8;
            const int priority = 1;
            const int reservedTableCount = 4;
            const PaymentStatus status = PaymentStatus.NotPaid;
            const string comment = "Ole er meget gammel og grim";
            const string name = "Ole";
            const string number = "88888888";
            Customer customer = new Customer(name, number);
            Reservation reservation = new Reservation(priority, reservedTableCount, status, comment, customer, null!);
            Cluster m = new Cluster("m", 4, tableAmount, ClusterType.Vertical);
            Assert.AreEqual(m.CustomerCount, 4);

            m.Tables[tableAmount - 1].Reservation = reservation;
            Assert.AreEqual(m.Tables[tableAmount-1].Reservation.TableCount, 4, "trouble with assesing reservation info though table");

            /*checks if tables not placed gives back one lower than table amount since 1 table is reserved*/
            Assert.AreEqual(tableAmount - 1, m.TablesNotPlaced, "TablesNotPlaced does not give back the amount of unreserved tables");

            /*checks if reservation knows how many reservation there are*/
            Assert.IsFalse(1 != m.ReservationCount, "reservation count doesnt have the correct amount of reservations");
        }
    }
}
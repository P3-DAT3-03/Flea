﻿using System;
using System.Threading.Tasks;
using Flea.Models;
using Flea.Tests.DbIntegration;
using Flea.Utility;
using NUnit.Framework;

namespace Flea.Tests
{
	public class QueryBuilderTests : DbTest
	{
		/*
		 * Throughout this class we make use of the term principal entity
		 * to mean the entity in a foreign key relation that contains the key
		 * that is referred to by the foreign key. The entity containing the
		 * foreign key is referred to as the dependent entity.
		 * 
		 * The term inverse relation or inverse property is used to refer to
		 * a property that refers to one or more dependant entities in a foreign key relation.
		 *
		 * It should also be mentioned that while tests are not technically dependant,
		 * they may perform operations that were tested in a previous test. For this
		 * reason the test are strictly ordered such that a test dependent on another
		 * must come after the other. This does not necessarily mean it will be executed first
		 * and the order of execution will have no impact on the result.
		 */

		/// <summary>
		/// Creates a new entity and saves it.
		/// We verify that the operation was a success by observing a change
		/// in customer id which is generated by the database.
		/// </summary>
		[Test]
		public async Task AddEntity()
		{
			var customer = new Customer("Test customer", "70154265");
			Assert.AreEqual(customer.Id, 0);
			await Factory.Update()
				.New(customer)
				.Save();
			Assert.AreEqual(customer.Id, 1);
		}

		/// <summary>
		/// Fetches an entity from the database.
		/// The operation is verified by matching the values
		/// retrieved from the database with the ones uploaded.
		/// </summary>
		[Test]
		public async Task GetEntity()
		{
			const string customerName = "Test customer";
			const string customerPhone = "12345678";

			await Factory.Update()
				.New(new Customer(customerName, customerPhone))
				.Save();

			var customer = await Factory.Get<BingoContext, Customer>().First();

			Assert.NotNull(customer);
			Assert.AreEqual(customer.Id, 1);
			Assert.AreEqual(customer.Name, customerName);
			Assert.AreEqual(customer.PhoneNumber, customerPhone);
		}


		/// <summary>
		/// Updates a entity.
		/// Verifies the update by fetching the entity and checking
		/// the value.
		/// </summary>
		[Test]
		public async Task UpdateEntity()
		{
			const string updatedName = "Test customer 2";
			// Create initial customer
			var customer = new Customer("Test customer", "12345678");
			await Factory.Update()
				.New(customer)
				.Save();

			// Change name value and save update
			customer.Name = updatedName;
			await Factory.Update()
				.Update(customer)
				.Save();

			// Fetch customer from database and verify changes
			customer = await Factory.Get<BingoContext, Customer>().First();
			Assert.NotNull(customer);
			Assert.AreEqual(customer.Name, updatedName);
		}

		/// <summary>
		/// Fetches the principal entity in a relation in such a manner
		/// that the dependants are included through an inverse property.
		/// The presence of all expected entities is checked in order to
		/// verify the operation.
		/// </summary>
		[Test]
		public async Task GetEntityWithInverseProperty()
		{
			// Initialise entities
			var @event = new Event("Test event", DateTime.Today);
			var customerA = new Customer("Test customer A", "12345678");
			var customerB = new Customer("Test customer B", "12345678");
			var reservationA = new Reservation(1, 2, false, "comment", customerA, @event);
			var reservationB = new Reservation(1, 2, true, "comment2", customerB, @event);

			await Factory.Update()
				.New(@event)
				.New(customerA, customerB)
				.New(reservationA, reservationB)
				.Save();

			var retrievedEvent = await Factory.Get<BingoContext, Event>()
				.Include(e => e.Reservations)
				.ThenInclude((Reservation r) => r.ReservationOwner)
				.First(e => e.Id == @event.Id);

			// Check that the event itself was retrieved
			Assert.NotNull(retrievedEvent);
			Assert.AreEqual(@event.Id, retrievedEvent.Id);

			// Check that the reservations are retrieved
			Assert.NotNull(retrievedEvent.Reservations);
			Assert.AreEqual(2, retrievedEvent.Reservations.Count);
			// We assume that they are retrieved and inserted in order of id
			Assert.AreEqual(1, retrievedEvent.Reservations[0].Id);
			Assert.AreEqual(2, retrievedEvent.Reservations[1].Id);
			
			// Check that the reservation owners are retrieved
			Assert.AreEqual(customerA.Id, retrievedEvent.Reservations[0].ReservationOwner.Id);
			Assert.AreEqual(customerB.Id, retrievedEvent.Reservations[1].ReservationOwner.Id);
		}

		/// <summary>
		/// Fetches the principal entity in a relation in such a manner
		/// that the dependants are included through an inverse property.
		/// The presence of all expected entities is checked in order to
		/// verify the operation.
		///
		/// This is the same test as <see cref="GetEntityWithInverseProperty"/>
		/// except the string version of the include command is used instead of
		/// the lambda version.
		/// </summary>
		[Test]
		public async Task GetEntityWithInversePropertyTextInclude()
		{
			// Initialise entities
			var @event = new Event("Test event", DateTime.Today);
			var customerA = new Customer("Test customer A", "12345678");
			var customerB = new Customer("Test customer B", "12345678");
			var reservationA = new Reservation(1, 2, false, "comment", customerA, @event);
			var reservationB = new Reservation(1, 2, true, "comment2", customerB, @event);

			await Factory.Update()
				.New(@event)
				.New(customerA, customerB)
				.New(reservationA, reservationB)
				.Save();

			var retrievedEvent = await Factory.Get<BingoContext, Event>()
				.Include("Reservations.ReservationOwner")
				.First(e => e.Id == @event.Id);

			// Check that the event itself was retrieved
			Assert.NotNull(retrievedEvent);
			Assert.AreEqual(@event.Id, retrievedEvent.Id);

			// Check that the reservations are retrieved
			Assert.NotNull(retrievedEvent.Reservations);
			Assert.AreEqual(2, retrievedEvent.Reservations.Count);
			// We assume that they are retrieved and inserted in order of id
			Assert.AreEqual(1, retrievedEvent.Reservations[0].Id);
			Assert.AreEqual(2, retrievedEvent.Reservations[1].Id);
			
			// Check that the reservation owners are retrieved
			Assert.AreEqual(customerA.Id, retrievedEvent.Reservations[0].ReservationOwner.Id);
			Assert.AreEqual(customerB.Id, retrievedEvent.Reservations[1].ReservationOwner.Id);
		}

		/// <summary>
		/// We test that an exception is thrown when a method is called
		/// after a finalizing operation has been called.
		/// </summary>
		[Test]
		public async Task UseAfterDisposeShouldThrow()
		{
			var query = Factory.Update().New(new Customer("Test user", "12345678"));
			await query.Save(); // This causes a dispose call.
			Assert.Throws<Exception>(() =>
			{
				query.New(new Customer("Test user", "12345678"));
			});
		}
	}
}
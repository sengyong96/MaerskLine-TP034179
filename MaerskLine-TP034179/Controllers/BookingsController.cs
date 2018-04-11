﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaerskLine_TP034179.Models;
using MaerskLine_TP034179.ViewModels;

namespace MaerskLine_TP034179.Controllers
{
    public class BookingsController : Controller
    {
        private ApplicationDbContext _context;

        public BookingsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Bookings
        public ActionResult Details()
        {
            return View();
        }

        //Select schedule in booking view
        public ActionResult SelectSchedule()
        {
            var schedule = _context.Schedules.ToList();
            return View(schedule);
        }

        //Select Ship in booking view
        public ActionResult SelectShip(int Scheduleid)
        {
            var schedule = _context.Schedules.SingleOrDefault(s => s.ScheduleID == Scheduleid);
            var shipList = _context.Ships.ToList();

            var viewModel = new ScheduleShipCustomerViewModel
            {
                Schedule = schedule,
                Ships = shipList
            };

            return View(viewModel);
        }

        //Select Customer in booking view
        public ActionResult SelectCustomer(int Scheduleid, int Shipid)
        {
            var schedule = _context.Schedules.SingleOrDefault(s => s.ScheduleID == Scheduleid);
            var ship = _context.Ships.SingleOrDefault(s => s.ShipID == Shipid);
            var CustomerList = _context.Customers.ToList();

            var viewModel = new ScheduleShipCustomerViewModel
            {
                Schedule = schedule,
                Ship = ship,
                Customers = CustomerList

            };

            return View(viewModel);
        }


        //Select Container in booking view
        public ActionResult SelectContainer(int Shipid, int Scheduleid, int Customerid)
        {
            var schedule = _context.Schedules.SingleOrDefault(s => s.ScheduleID == Scheduleid);
            var Ship = _context.Ships.SingleOrDefault(s => s.ShipID == Shipid);
            var customer = _context.Customers.SingleOrDefault(c => c.CustomerID == Customerid);
            //var ContainerList = _context.Containers.ToList();

            var viewModel = new ScheduleShipCustomerViewModel
            {
                Schedule = schedule,
                Ship = Ship,
                Customer = customer
                // Containers = ContainerList

            };

            return View(viewModel);
        }

        public ActionResult CreateBooking(ScheduleShipCustomerViewModel sscvm)
        {
            var tempShipID = sscvm.Ship.ShipID;
            var newContainerSpace = sscvm.Container.ContainerWeight;

            var tempContainerSpace = _context.Ships.Single(s => s.ShipID == tempShipID).ShipContainerSpace;

            if (tempContainerSpace - newContainerSpace < 0)
            {
                ViewBag.Error = "The container space is exceeded the ship's container space.";

                var oldSchedule = _context.Schedules.SingleOrDefault(s => s.ScheduleID == sscvm.Schedule.ScheduleID);
                var oldShip = _context.Ships.SingleOrDefault(s => s.ShipID == sscvm.Ship.ShipID);
                var oldCustomer = _context.Customers.SingleOrDefault(c => c.CustomerID == sscvm.Customer.CustomerID);

                var viewModel = new ScheduleShipCustomerViewModel
                {
                    Schedule = oldSchedule,
                    Ship = oldShip,
                    Customer = oldCustomer
                };

                return View("SelectContainer", viewModel);
            }

            var ship = _context.Ships.Single(s => s.ShipID == sscvm.Ship.ShipID);
            ship.ShipContainerSpace = Convert.ToInt32(tempContainerSpace - newContainerSpace);

            var booking = new Booking()
            {
                ScheduleID = sscvm.Schedule.ScheduleID,
                ShipID = sscvm.Ship.ShipID,
                CustomerID = sscvm.Customer.CustomerID,
                BookingAgent = User.Identity.Name
            };

            //_context.Bookings.Add(booking);
            //_context.SaveChanges();

            //var test = _context.Bookings.SingleOrDefault(b => b.CustomerID == sscvm.Customer.CustomerID);

            var container = new Container()
            {
                ContainerID = sscvm.Container.ContainerID,
                ContainerType = sscvm.Container.ContainerType,
                ContainerWeight = sscvm.Container.ContainerWeight,

                BookingID = sscvm.Booking.BookingID
            };



            _context.Bookings.Add(booking);
            _context.Containers.Add(container);
            _context.SaveChanges();

            //var orderList = _context.Containers.Include(o => o.Booking).ToList();


            var orderList = _context.Containers
                .Include(o => o.Booking.Schedule)
                .Include(o => o.Booking.Customer)
                .Include(o => o.Booking.Ship)
                .Include(o => o.Booking)
                .ToList();


            //var containerList = _context.Containers
            //    .Include(c => c.Booking)
            //    .Include(c => c.ContainerID)
            //    .Include(c => c.ContainerType)
            //    .Include(c => c.ContainerWeight)
            //    .ToList();

            //return View(orderList);
            return View("ViewBooking", orderList);
        }

        public ActionResult ViewBooking()
        {
            //var schedule = _context.Schedules.SingleOrDefault(s => s.ScheduleID == Scheduleid);
            //var ship = _context.Ships.SingleOrDefault(s => s.ShipID == Shipid);
            //var customer = _context.Customers.SingleOrDefault(c => c.CustomerID == Customerid);
            var container = _context.Containers
                .Include(c => c.Booking)
                .Include(c => c.Booking.Schedule)
                .Include(c => c.Booking.Customer)
                .Include(c => c.Booking.Ship).ToList();
            //.SingleOrDefault(c => c.ContainerID == Containerid);

            //var viewModel = new ScheduleShipCustomerViewModel()
            //{
            //    Schedule = schedule,
            //    Ship = ship,
            //    Customer = customer,
            //    Container = container
            //};   
            //var orderList = _context.Bookings
            //    .Include(o => o.Schedule)
            //    .Include(o => o.Customer)
            //    .Include(o => o.Ship)
            //    .ToList();


            //var book = _context.Containers.Include(b => b.)

            return View(container);
        }
    }
}
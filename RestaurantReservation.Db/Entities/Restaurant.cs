﻿using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string OpeningHours { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Table> Tables { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}

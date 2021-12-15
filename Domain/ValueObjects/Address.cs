﻿using Flunt.Validations;
using Shared.ValueObjects;

namespace Domain.ValueObjects
{
    public class Address  : ValueObject
    {
        public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(new Contract<Address>().Requires()
                .IsGreaterOrEqualsThan(Street, 3, "Street", "A rua deve conter pelo menos 3 caracteres.")
                .IsGreaterOrEqualsThan(State, 3, "State", "O estado deve conter pelo menos 3 caracteres."));
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}

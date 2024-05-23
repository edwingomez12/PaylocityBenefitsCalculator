using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Api.Models;
using Api.Dtos.Employee;
using Api.Dtos.Dependent;
using System.Transactions;

namespace Api.DAL
{
    public  class EmployeeInitializer 
    {
        public  static List<Employee> Seed()
        {

            var employeesdto = GetData();
            var employees = employeesdto.Select(e => new Employee
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                DateOfBirth = e.DateOfBirth,
                Salary = e.Salary
            }).ToList();
            return employees;
        }

        public static List<Dependent> SeedDependents()
        {
            var employeesdto = GetData();
            List<Dependent> dependentDtos = new List<Dependent>();
            foreach(var e in employeesdto)
            {
                foreach(var d in e.Dependents)
                {
                    var dependent = new Dependent 
                    {
                        Id = d.Id,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        EmployeeId = e.Id,
                        DateOfBirth = d.DateOfBirth,
                        Relationship = d.Relationship
                    };
                    dependentDtos.Add(dependent);
                }
            }
            return dependentDtos;
        }

        private static List<GetEmployeeDto> GetData()
        {
                return new List<GetEmployeeDto>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };
        }
    }
}
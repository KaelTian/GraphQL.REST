using Microsoft.EntityFrameworkCore.Internal;
using RealEstateManager.Database.Models;
using System;
using System.Collections.Generic;

namespace RealEstateManager.Database
{
    public static class RealEstateSeedData
    {
        public static void EnsureSeedData(this RealEstateContext db)
        {
            if (!db.Properties.Any() || !db.Payments.Any())
            {
                var properties = new List<Property> {
                new Property
                {
                    City="New York",
                    Family="Smith",
                    Name="Big House",
                    Street="Broadway",
                    Value=100000,
                    Payments=new List<Payment>
                    {
                        new Payment
                        {
                            DateCreated=new DateTime(2018,03,06),
                            DateOverdue=new DateTime(2019,08,15),
                            Paid=true,
                            Value=1500,
                        },
                        new Payment
                        {
                            DateCreated=new DateTime(2020,06,06),
                            DateOverdue=new DateTime(2020,09,15),
                            Paid=false,
                            Value=2800,
                        },
                        new Payment
                        {
                            DateCreated=new DateTime(2017,02,02),
                            DateOverdue=new DateTime(2020,10,01),
                            Paid=true,
                            Value=5000,
                        },
                    }
                },
                new Property
                {
                    City="London",
                    Family="Binley",
                    Name="White House",
                    Street="Orange",
                    Value=300500,
                    Payments=new List<Payment>
                    {
                        new Payment
                        {
                            DateCreated=new DateTime(2017,02,05),
                            DateOverdue=new DateTime(2018,12,31),
                            Paid=true,
                            Value=3300,
                        },
                        new Payment
                        {
                            DateCreated=new DateTime(2019,11,11),
                            DateOverdue=new DateTime(2020,06,13),
                            Paid=true,
                            Value=6987,
                        },
                        new Payment
                        {
                            DateCreated=new DateTime(2016,09,28),
                            DateOverdue=new DateTime(2020,10,01),
                            Paid=false,
                            Value=1600,
                        },
                    }
                },
                new Property
                {
                    City="San Francisco",
                    Family="Michael",
                    Name="Museum",
                    Street="yellow",
                    Value=555500,
                    Payments=new List<Payment>
                    {
                        new Payment
                        {
                            DateCreated=new DateTime(2017,02,05),
                            DateOverdue=new DateTime(2018,12,31),
                            Paid=true,
                            Value=3600,
                        },
                        new Payment
                        {
                            DateCreated=new DateTime(2019,11,11),
                            DateOverdue=new DateTime(2020,06,13),
                            Paid=true,
                            Value=3600,
                        },
                        new Payment
                        {
                            DateCreated=new DateTime(2016,09,28),
                            DateOverdue=new DateTime(2020,10,01),
                            Paid=true,
                            Value=9527,
                        },
                    }
                },
                };
                db.Properties.AddRange(properties);
                db.SaveChanges();
            }
        }
    }
}

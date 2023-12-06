using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD
{
    public class GenerateId
    {
        public static int GenerateID()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());

        }
    }
}
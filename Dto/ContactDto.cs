﻿using DascoPlasticRecyclingApp.Models;

namespace DascoPlasticRecyclingApp.Dto
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int UserId { get; set; }
        public int ContactTypeId { get; set; }
    }
}

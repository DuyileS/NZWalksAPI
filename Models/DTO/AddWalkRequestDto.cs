﻿using NZwalks.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
       [ Range(0,50)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
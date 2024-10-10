﻿namespace Matchfinder.DTO
{
    public class UserDTO
    {
        public required string Username { get; set; }
        public string? KnownAs { get; set; }
        public required string Token { get; set; }
        public required string Gender { get; set; }
        public string? PhotoUrl { get; set; }
    }
}

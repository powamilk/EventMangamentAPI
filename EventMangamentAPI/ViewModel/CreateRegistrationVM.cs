﻿namespace EventMangamentAPI.ViewModel
{
    public class CreateRegistrationVM
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Status { get; set; }
    }
}

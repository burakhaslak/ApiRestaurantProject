namespace ApiProject.WebApi.Dtos.GroupReservationDtos
{
    public class CreateGroupReservationDto
    {
        public string ContactPersonName { get; set; }
        public string GroupTitle { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public string Details { get; set; }
        public string ReservationStatus { get; set; }
        public int? ParticipantCount { get; set; }
        public string? Email { get; set; }
    }
}

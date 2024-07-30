namespace NZWALKS.DTO
{
    public class AddWalkClass
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DeficultyId { get; set; }

        public Guid RegionId { get; set; }

       
    }
}

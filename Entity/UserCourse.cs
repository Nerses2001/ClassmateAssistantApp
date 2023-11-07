namespace Entity
{
    public class UserCourse
    {
        public string? UserId { get; set; }
        public ApplicationUser ? User { get; set; }

        public int CourseId { get; set; }
        public Cource ? Course { get; set; }

    }
}
